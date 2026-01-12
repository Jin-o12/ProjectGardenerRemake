using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    /* Component */
    private InputSettings inputSettings;
    private Camera mainCamera;
    
    /* 차후 자동으로 총알 값이 연결 되도록 수정 */
    public GameObject nowBullet;
    public SpriteRenderer sprRender;
    
    // 아래는 고정 변수, 수정하지 않아도 됨
    private Transform playerTf;     // 플레이어 transform

    void Start()
    {
        inputSettings = PlayerManager.Instance.InputSettings;
        playerTf = transform;
        mainCamera = Camera.main;
        sprRender = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        LookMousePos();
        FireAction();
        BuiltAction();
    }

    /* 무기 발사 액션에 대한 확인 */
    private void FireAction()
    {
        if (Input.GetKeyDown(inputSettings.Firing))
        {
            FireBullet();
        }
    }

    /* 건물 건설 & 상호작용 액션에 대한 확인 */
    private void BuiltAction()
    {
        if (Input.GetKeyDown(inputSettings.Interact))
        {
            Debug.Log("Do someting here");
        }
    }

    /* 발사 액션 실행 */
    private void FireBullet()
    {
        // 1. 현재 플레이어 회전값을 가져옴
        Quaternion currentRot = playerTf.rotation;

        // 2. 추가하고 싶은 회전값 설정 (마우스의 방향을 더하여 총구 회전)
        Quaternion addRot = Quaternion.Euler(0, 0, 0);

        // 3. 벡터 곱으로 추가 회전
        Quaternion finalRot = currentRot * addRot;

        // 4. 최종 발사 지점 Transform 값
        Instantiate(nowBullet, playerTf.position, finalRot);
    }

    /* 플레이어 화면 기준 마우스가 어느 위치에 있는지 찾는 함수 */
    private void LookMousePos()
    {
        /// 카메라의 평면 화면에 대한 플레이어의 위치를 구한 뒤, 
        /// 마우스의 위치와 거리 및 방향 연산을 해 플레이어의 시점을 y축을 축으로 회전시킨다
        /// 2D 각도 계산 결과는 반시계 방향이 양수이기 때문에 최종 각도에 음수 계산을 함
        /// 또한 유니티의 정면(z축)은 2D 죄표계의 정면(x축)과 다르기 때문에 +90도 하여 보정
        
        // 1. 플레이어의 월드 좌표를 2D 화면 좌표로 변환
        Vector3 playerScreenPos = mainCamera.WorldToScreenPoint(transform.position);

        // 2. 마우스의 화면상 죄표
        Vector3 mouseScreenPos = Input.mousePosition;
        
        // 3. 방향 벡터 계산
        Vector3 lookDir = mouseScreenPos-playerScreenPos;

        // 4. 라디안 -> 도 변환 계산 (아크탄젠트, Atan2: x축을 기준으로 한 각도 반환)
        float lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, -lookAngle+90.0f, 0);

        // 바라보는 방향에 따라 플레이어 스프라이트 회전(반전)
        // 플레이어 기본 방향: 오른쪽
        // 마우스가 왼쪽에 있다면 왼쪽(스프라이트 반전)을 처다본다
        if(mouseScreenPos.x < playerScreenPos.x)
        {
            sprRender.flipX = true;
        }
        else
        {
            sprRender.flipX = false;
        }
    }
}
