using UnityEngine;
using UnityEngine.EventSystems; // UI 위에 마우스가 있을 때 Raycast를 무시하기 위해 추가

public class GridHoverSelector : MonoBehaviour
{
    public float cellSize = 1.0f;
    public LayerMask gridPlaneLayer;
    public GameObject highlightQuadPrefab; // 호버 시 표시할 하이라이트 오브젝트

    private GameObject currentHighlightQuad;
    private Vector2Int lastHoveredIndex = new Vector2Int(-1, -1); // 직전에 호버했던 셀 인덱스 (초기값: 없는 위치)

    void Start()
    {
        if (highlightQuadPrefab != null)
        {
            currentHighlightQuad = Instantiate(highlightQuadPrefab);
            currentHighlightQuad.transform.localScale = new Vector3(cellSize, cellSize, 1); // 2D Quad일 경우
            currentHighlightQuad.SetActive(false);
        }
    }

    void Update()
    {
        // UI 요소 위에 마우스가 있다면 3D 월드 호버 판정을 무시
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // 이전에 있던 하이라이트를 끈다
            if (currentHighlightQuad != null && currentHighlightQuad.activeSelf)
            {
                currentHighlightQuad.SetActive(false);
                lastHoveredIndex = new Vector2Int(-1, -1); // 상태 초기화
            }
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, gridPlaneLayer))
        {
            // 현재 마우스 아래에 있는 셀의 인덱스를 계산
            int xIndex = Mathf.FloorToInt(hit.point.x / cellSize);
            int zIndex = Mathf.FloorToInt(hit.point.z / cellSize);
            Vector2Int currentIndex = new Vector2Int(xIndex, zIndex);

            // 이전에 호버했던 셀과 다른 셀 위에 마우스가 올라왔다면
            if (currentIndex != lastHoveredIndex)
            {
                // ✨ OnHoverEnter 로직 실행
                Debug.Log($"마우스가 ({currentIndex.x}, {currentIndex.y}) 셀에 들어왔습니다.");

                // 하이라이트 위치 업데이트
                if (currentHighlightQuad != null)
                {
                    Vector3 highlightPosition = new Vector3(
                        xIndex * cellSize + cellSize / 2,
                        hit.point.y + 0.01f, // 살짝 띄우기
                        zIndex * cellSize + cellSize / 2
                    );
                    currentHighlightQuad.transform.position = highlightPosition;
                    currentHighlightQuad.SetActive(true);
                }

                // 현재 위치를 "마지막 위치"로 기록
                lastHoveredIndex = currentIndex;
            }
        }
        else // Ray가 아무것에도 맞지 않았다면 (마우스가 GridPlane을 벗어남)
        {
            // 이전에 하이라이트가 켜져 있었다면
            if (lastHoveredIndex.x != -1)
            {
                // ✨ OnHoverExit 로직 실행
                Debug.Log($"마우스가 그리드를 벗어났습니다.");

                if (currentHighlightQuad != null)
                {
                    currentHighlightQuad.SetActive(false);
                }

                // 마지막 위치를 초기값으로 리셋
                lastHoveredIndex = new Vector2Int(-1, -1);
            }
        }
    }
}