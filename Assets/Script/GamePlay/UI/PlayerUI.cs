/// <summary>
/// PlayerUI.cs
/// Player UI에 관련한 필드 값을 관리하는 스크립트
/// 할당이 필요한 UI 오브젝트는 이 스크립트에 할당
/// </summary>
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance { get; private set; }

    [Header("QuickSlot")]
    public Sprite defaultSlotSprite;
    public Sprite selectSlotSprite;
    public const int slotNumber = 5;
    private QuickSlot[] quickSlot;
    private int selectedSlotIndex = 0;

    void Awake()
    {
        // 싱글턴 초기화
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 씬 전환 시 UI가 새로 만들어지면 재바인딩
        if (SceneLoader.Instance != null)
            SceneLoader.Instance.SceneLoaded += OnSceneLoaded;
        else
            SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void Start()
    {
        // 씬 내에서 QuickSlot 컴포넌트들을 찾아 할당
        BindQuickSlots();
    }

    void OnDestroy()
    {
        // 구독 해제 (SceneLoader가 있는 경우와 없는 경우 둘 다 처리)
        if (SceneLoader.Instance != null)
            SceneLoader.Instance.SceneLoaded -= OnSceneLoaded;
        else
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 전환 시 QuickSlot 재바인딩
        BindQuickSlots();
    }

    private void BindQuickSlots()
    {
        // 선택없음 0번을 포함하여 슬롯 배열 초기화
        quickSlot = new QuickSlot[slotNumber + 1];

        // 자식 오브젝트로 존재하는 모든 QuickSlot 컴포넌트 검색
        var slotComp = GetComponentsInChildren<QuickSlot>(includeInactive: true);
        if (slotComp == null || slotComp.Length == 0)
        {
            Debug.LogWarning("QuickSlots not found in children.");
            return;
        }

        // 슬롯 컴포넌트들을 슬롯 배열에 할당 (1..slotNumber)
        foreach (var slot in slotComp)
        {
            int index = slot.transform.GetSiblingIndex() + 1; // 0-based to 1-based
            if (index >= 1 && index <= slotNumber)
            {
                quickSlot[index] = slot;
            }
        }

        // 모든 슬롯(1..slotNumber)을 Unselect하고, 현재 selectedSlotIndex를 다시 적용
        for (int i = 1; i <= slotNumber; i++)
        {
            if (quickSlot[i] != null)
                quickSlot[i].UnselectSlot();
        }

        selectedSlotIndex = Mathf.Clamp(selectedSlotIndex, 0, slotNumber);

        if (selectedSlotIndex != 0)
        {
            if (quickSlot[selectedSlotIndex] != null)
                quickSlot[selectedSlotIndex].SelectSlot();
            else
                Debug.LogWarning($"PlayerUI: selectedSlotIndex {selectedSlotIndex} has no QuickSlot assigned.");
        }
    }

    public void UpdateSelectSlot(int index)
    {
        // 입력 유효성 검사
        if (index < 0 || index > slotNumber)
        {
            Debug.LogWarning($"PlayerUI.UpdateSelectSlot: index {index} out of range 0..{slotNumber}");
            return;
        }

        // 이전 선택 해제(0이면 해제할 것 없음)
        if (selectedSlotIndex != 0 && quickSlot != null && selectedSlotIndex <= slotNumber && quickSlot[selectedSlotIndex] != null)
            quickSlot[selectedSlotIndex].UnselectSlot();

        selectedSlotIndex = index;

        // 새 선택 적용 (0이면 아무것도 선택하지 않음)
        if (selectedSlotIndex != 0 && quickSlot[selectedSlotIndex] != null)
            quickSlot[selectedSlotIndex].SelectSlot();
    }
}
