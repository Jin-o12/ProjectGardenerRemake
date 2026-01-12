using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    /* Component */
    private InputSettings inputSettings;
    
    /* 총알 발사 테스트 관련 변수들. 이후 자동으로 총알 값이 연결 되도록 수정 */
    // 연결 후 private로 돌려야 할 변수들
    public GameObject nowBullet;
    
    // 아래는 고정 변수, 수정하지 않아도 됨
    private Transform playerTf;     // 플레이어 transform

    void Start()
    {
        inputSettings = PlayerManager.Instance.InputSettings;
        playerTf = gameObject.transform;
    }

    void Update()
    {
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
        // 1. 현재 내 회전값을 가져옴
        Quaternion currentRot = transform.rotation;
        
        // 2. 추가하고 싶은 회전값을 만듦 (Y축 90도)
        Quaternion addRot = Quaternion.Euler(0, 90, 0);

        // 3. 두 회전을 곱함 (쿼터니언에서 A * B는 A 상태에서 B만큼 더 회전하라는 뜻)
        Quaternion finalRot = currentRot * addRot;

        // 최종 발사 지점 Transform 값
        Instantiate(nowBullet, gameObject.transform.position, finalRot);
    }
}
