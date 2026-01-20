/// <summary>
/// 타일 초기화 및 배치
/// 게임 진행 중 타일과의 상호작용 기능 제공
/// </summary>
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldManager : MonoBehaviour
{
    private InputSettings inputSettings;

    public GameObject tilePrefab;           // 게임 오브젝트 프리팹
    public GameObject tileParentsObj;       // 모든 타일의 부모 오브젝트
    //public Sprite tileLineImage;            // 타일 경계선 이미지
    //public Sprite hoverImage;               // 마우스 호버시의 이미지

    public int mapWidth;                    // 전체 맵 가로 길이
    public int mapHight;                    // 전체 맵 세로 길이
    public int tileSize;                    // 타일 길이 (가로 세로 동일)
    public float tileDist;                  // 타일 간격

    private GridSell[,] gridArray;          // 모든 격자를 담는 2차원 배열
    private GridSell nowHoverTile;          // 현재 마우스가 올라간 타일

    public LayerMask whatIsTile;            // raycast가 인식 할 타일의 레이어

    void Start()
    {
        // 스크립트 내 변수들 초기화
        inputSettings = PlayerManager.Instance.InputSettings;
        tileDist = 0.1f;                    // 오차를 고려해 약간의 간격을 둠
        nowHoverTile = null;                // 초기에는 지정된 타일이 없음

        // 임시 지정 값들
        mapWidth = 100;
        mapHight = 100;
        tileSize = 1;
        
        InitializFieldGrid();               // 타일 초기화
    }
    
    private void InitializFieldGrid()
    {
        gridArray = new GridSell[mapWidth, mapHight];

        // 0. 셀 간격과 크기를 감안하여 셀간의 배치 간격 계산
        float interval = tileSize + tileDist;

        // 1. 그리드의 전체 크기 계산
        float gridWidth = mapWidth * interval;
        float gridHeight = mapHight * interval;

        // 2. 중심을 맞추기 위한 보정값 (Offset) 계산
        // (전체 크기의 절반)을 빼주고, (타일 크기의 절반)을 더하기
        float startX = -(gridWidth / 2) + (interval / 2);
        float startZ = -(gridHeight / 2) + (interval / 2);

        for(int x=0; x<mapWidth ; x++)
        {
            for(int z=0; z<mapHight ; z++)
            {
                // 3-1. 보정값을 적용하여 위치 계산
                float posX = (x * interval) + startX;
                float posZ = (z * interval) + startZ;

                // 3-2. 위치 계산 후 타일을 배치한다. 보정된 좌표를 바탕으로 왼쪽 아래 하단에서부터 2차원 평면으로 배치해 나감.
                
                // 지역좌표 -> 월드좌표 보정을 위한 부모 오브젝트의 현재 월드 좌표를 최종 좌표에 더해줌
                Vector3 centerPos = tileParentsObj.transform.position;
                Vector3 pos = new Vector3(posX, 0, posZ) + centerPos;

                GameObject newTileObj = Instantiate(tilePrefab, pos, Quaternion.identity);

                // 지정된 크기로 셀 크기를 늘려줌
                newTileObj.transform.localScale = new Vector3(tileSize, 1, tileSize);

                // 3-3. 지정된 부모로 타일 상속 계층을 정리하고, 구분을 위해 이름에 좌표 기입
                newTileObj.transform.parent = tileParentsObj.transform;
                newTileObj.name = $"Sell_({x},{z})";

                // 3-4. 타일 초기화 및 배열에 저장
                GridSell tile = newTileObj.GetComponent<GridSell>();
                tile.InitGridSell(x, z);
                gridArray[x, z] = tile;
            }
        }
    }

    void Update()
    {
        HoverMouseTile();
    }

    private void HoverMouseTile()
    {
        // raycast 생성
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        // ray에 히트되는 타일에 대해 아래 코드 실행. 단, 특정 레이어에 대해서만 인식
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, whatIsTile))
        {
            GridSell hitTile = hit.collider.GetComponent<GridSell>();
            // 아무 타일도 지정하지 않거나, 지정된 오브젝트가 타일이 아닌 경우
            if (hitTile == null)
                return;

            // UI 요소 위에 마우스가 있다면 3D 월드 호버 판정을 무시
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // 이전에 있던 하이라이트를 끈다
                if (nowHoverTile != null)
                    nowHoverTile.IsMouseHover(false);
                return;
            }

            // 새로운 타일에 호버시
            if(nowHoverTile!=hitTile)
            {   
                // 이전 호버 타일이 존재 한다면 해당 타일의 하이라이트를 끄고
                if(nowHoverTile!=null)
                    nowHoverTile.IsMouseHover(false);

                // 새로 호버된 타일의 하이라이트를 킴
                hitTile.IsMouseHover(true);
                nowHoverTile = hitTile;
            }

            if(Input.GetKeyDown(inputSettings.Interact))
            {
                if(nowHoverTile != null)
                    nowHoverTile.PlaceBuilding();
            }
        }
        else if (nowHoverTile != null)
        {
            // 마우스가 맵 밖으로 나가면 하이라이트 해제
            nowHoverTile.GetComponent<GridSell>().IsMouseHover(false);
            nowHoverTile = null;
        }
    }

}
