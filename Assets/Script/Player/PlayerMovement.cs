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

    private Animator playerAnim;                  // 플레이어 애니메이션

    void Start()
    {
        InputSettings = PlayerManager.Instance.InputSettings;
        moveSpeed = PlayerManager.Instance.moveSpeed;
        dodgeSpeed = PlayerManager.Instance.dodgeSpeed;
        dodgeDuration = PlayerManager.Instance.dodgeDuration;
        dodgeCooldown = PlayerManager.Instance.dodgeCooldown;
        cameraTf = PlayerManager.Instance.cameraTf;
        playerAnim = PlayerManager.Instance.playerAnim;
        

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
        /* 0. 구르고 있을 시 리턴시켜 구르기 외 이동하지 않게 함 */
        if (isDodging) return;

        /* 1. 키 입력에 따른 방향값 계산 */

        // 앞, 뒤에 대한 방향값을 임시로 만들고 (이후 실수값과 연산될 것이기 때문에 float로 선언)
        float h = 0f;
        float v = 0f;

        // 사방향에 대해 입력을 받으면 해당 방향에 대해 1~-1 사이 값으로 판정한다 (0: 두 방향을 동시에 눌러 정지)
        if (Input.GetKey(InputSettings.MoveUp)) v += 1f; 
        if (Input.GetKey(InputSettings.MoveDown)) v -= 1f;
        if (Input.GetKey(InputSettings.MoveRight)) h += 1f;
        if (Input.GetKey(InputSettings.MoveLeft)) h -= 1f;

        /* 2. 카메라(플레이어 시점)을 기준으로 최종 이동 방향을 결정하는 코드 */

        // 카메라 앞 벡터 구하고 Y축 값 제거 (수평 값은 고정이므로 계산에서 제외)
        Vector3 camRight = cameraTf.right;
        camRight.y = 0;
        camRight.Normalize();
        Vector3 camForward = Vector3.Cross(camRight, Vector3.up);

        // 카메라 방향 * 이동 방향 = 최종 정규화
        moveDir = (camForward*v + camRight*h).normalized;

        /* 4. 이동 거리 저장하고 FicedUpdata에서 최종 이동 처리 */
    }

    /* 이동 처리 */
    private void Move()
    {
        // 구를 경우와 아닐 때의 이동을 나눔
        if(isDodging)
        {
            thisRigidbody.linearVelocity = dodgeDir * dodgeSpeed;
            playerAnim.SetBool("Move", false);
        }
        else
        {
            // 입력이 없으면 멈추고 아니면 해당 위치(Rigidbady)로 이동
            if(moveDir == Vector3.zero)
            {
                thisRigidbody.linearVelocity = new Vector3(0, thisRigidbody.linearVelocity.y, 0);
                playerAnim.SetBool("Move", false);
                return;
            }
            else
            {
                thisRigidbody.linearVelocity = new Vector3(moveDir.x*moveSpeed,         // X
                                                        thisRigidbody.linearVelocity.y, // Y
                                                        moveDir.z*moveSpeed);           // Z

                // 이동 애니메이션
                playerAnim.SetBool("Move", true);
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
