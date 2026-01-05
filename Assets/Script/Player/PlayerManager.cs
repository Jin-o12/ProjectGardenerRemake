using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [Header("Component")]
    public InputSettings InputSettings;         // 입력 설정 컴포넌트
    public Transform cameraTf;                  // 카메라 트랜스폼

    [Header("Player Info: Health")]
    public int maxHealth = 100;

    [Header("Player Info: move")]
    public float moveSpeed;                     // 이동 속도
    public float dodgeSpeed;                    // 구르기 속도
    public float dodgeDuration;                 // 구르기 지속 시간
    public float dodgeCooldown;                 // 구르기 쿨타임

    [Header ("Player Inventory")]
    const int eqipmentNum = 2;
    public int inventorySize;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
    }
#endif

    void Start()
    {
    }
}


