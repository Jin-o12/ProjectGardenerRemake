/// <summay>
/// 필드 위의 격자 하나의 프리팹이 가지는 클래스.
/// 격자 하나의 정보를 담는다.
/// </summay>
using UnityEngine;
using UnityEngine.UI;

public enum TileObjectType
{
    Turret,     // 터렛
    Wall,       // 벽
    Building,   // 기타 건축물
    Obstacle,   // 장애물
    None        // 아무것도 없는 경우
}

public class GridSell : MonoBehaviour
{
    [Header ("구역 정보")]
    private int x;                                          // 전체 그리드 상의 좌표 x
    private int z;                                          // 전체 그리드 상의 좌표 z
    private bool isQccupied                                 // 무언가에 의해 막혀있는 상태인지 확인
        { get; set; }
    public TileObjectType objType                           // 타일 위 존재하는 오브젝트
        { get; set; }

    [Header ("스프라이트 이미지")]
    [SerializeField] public SpriteRenderer tileSprRender;   // 현재 타일 스프라이트 렌더러
    [SerializeField] public Sprite defaultTileImage;        // 기본 타일 스프라이트 이미지
    [SerializeField] public Sprite hoverTileImage;          // 호버시 타일 스프라이트 이미지
    
    

    /* 셀 초기화 */
    public void InitGridSell(int _x, int _z)
    {
        this.x = _x;
        this.z = _z;
        isQccupied = false;
        objType = TileObjectType.None;
        tileSprRender.sprite = defaultTileImage;
    }

    void Update()
    {
        
    }

    /* 초기화 작업 과정에서 해당 셀에 특정 오브젝트를 설치하기 위한 함수 */
    public void SetObjectType()
    {
        
    }

    /* 스프라이트를 지정한 이미지로 변화 */
    public void IsMouseHover(bool hover)
    {
        tileSprRender.sprite = hover ? hoverTileImage:defaultTileImage;
    }
    
    /* 현재 셀에 건물을 지음 */
    public void PlaceBuilding()
    {
        /// 임시 코드, 플레이어에게 입력을 받는 로직 추가할 것 ///
        TileObjectType _objType = TileObjectType.Wall;
        Debug.Log($"{x},{z} 좌표에 건축 실행");
        objType = _objType;
    }
}
