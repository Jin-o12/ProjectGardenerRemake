using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /* Component */
    private InputSettings InputSettings;
    private Rigidbody thisRigidbody;

    /* Move Settings */
    private float moveSpeed;                // 이동 속도
    private float dodgeSpeed;               // 도주 속도
    private float dodgeDuration;            // 도주 지속 시간
    private float dodgeCooldown;            // 도주 쿨타임
    private float dodgeCooldownTimer;       // 도주 쿨타임 타이머
    private float dodgeDurationTimer;       // 도주 지속 시간 타이머
    private bool isDodging;                 // 도주 중인지 여부
    private Transform cameraTf;              // 카메라 트랜스폼

    private Vector3 moveDir;                // 현재 이동 방향 벡터
    private Vector3 moveInput;              // 이동 입력 벡터
    private Vector3 dodgeDir;               // 도주 방향

    void Start()
    {
        InputSettings = PalyerInfomation.Instance.InputSettings;
        moveSpeed = PalyerInfomation.Instance.moveSpeed;
        dodgeSpeed = PalyerInfomation.Instance.dodgeSpeed;
        dodgeDuration = PalyerInfomation.Instance.dodgeDuration;
        dodgeCooldown = PalyerInfomation.Instance.dodgeCooldown;
        cameraTf = PalyerInfomation.Instance.cameraTf;
        

        dodgeCooldownTimer = 0f;
        isDodging = false;

        thisRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MoveInput();
        Dodge();
    }

    void FixedUpdate()
    {
        Move();
    }

    /* 이동 입력 확인 */
    private void MoveInput()
    {
        if (isDodging) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        

        // 카메라 시야각 기준 이동 방향 교정

        // 카메라 앞, 오른쪽 벡터 구하고 Y축 값 제거
        Vector3 camFoward = cameraTf.forward;
        Vector3 camRight = cameraTf.right;
        camFoward.y = 0;
        camRight.y = 0;

        // 벡터 정규화 후 카메라 방향과 이동 방향을 곱해 최종 정규화
        camFoward.Normalize();
        camRight.Normalize();
        moveDir = (camFoward*v + camRight*h).normalized;

        // 이동 거리 저장하고 FicedUpdata에서 최종 이동 처리
    }

    /* 이동 처리 */
    private void Move()
    {
        // 구를 경우와 아닐 때의 이동을 나눔
        if(isDodging)
        {
            thisRigidbody.linearVelocity = dodgeDir * dodgeSpeed;
        }
        else
        {
            // 입력이 없으면 멈추고 아니면 해당 위치(Rigidbady)로 이동
            if(moveDir == Vector3.zero)
            {
                thisRigidbody.linearVelocity = new Vector3(0, thisRigidbody.linearVelocity.y, 0);
                return;
            }
            else
            {
                thisRigidbody.linearVelocity = new Vector3(moveDir.x*moveSpeed,         // X
                                                        thisRigidbody.linearVelocity.y, // Y
                                                        moveDir.z*moveSpeed);           // Z
            }
            
        }
        
    }

    /* 구르기 처리 */
    private void Dodge()
    {
        if (isDodging && dodgeDurationTimer > 0f)
        {
            dodgeDurationTimer -= Time.deltaTime;
            return;
        }

        if (!isDodging && dodgeCooldownTimer > 0f)
        {
            dodgeCooldownTimer -= Time.deltaTime;
            return;
        }

        if (Input.GetKeyDown(InputSettings.Dodge) && !isDodging)
        {
            Debug.Log("Dodge!");
            dodgeDurationTimer = dodgeDuration;
            isDodging = true;
            StartCoroutine(dodgeCoroutine());
        }
    }

    /* 구르기 코루틴 */
    private IEnumerator dodgeCoroutine()
    {
        isDodging = true;

        // 현재 이동 방향으로 구르기 위한 방향 설정 (단, 움직이지 않는 경우는 제외)
        dodgeDir = moveDir == Vector3.zero ? transform.forward : moveDir;

        // 무적 판정을 위한 레이어 이동 처리
        gameObject.layer = LayerMask.NameToLayer("PlayerInvincible");

        // 구르는 동안 대기
        yield return new WaitForSeconds(dodgeDuration);

        // 구르기 종료 후
        gameObject.layer = LayerMask.NameToLayer("Player"); // 원래 레이어로 복귀
        isDodging = false;
        thisRigidbody.linearVelocity = Vector3.zero;        // 속도 초기화 (미끄러짐 방지)
        dodgeCooldownTimer = dodgeCooldown;                 // 쿨타임 시작
    }
}
