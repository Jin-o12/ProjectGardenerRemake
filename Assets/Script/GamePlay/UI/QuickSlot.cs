using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    /* 슬롯 박스 */
    private Image slotImage;

    /* 슬롯 아이템 */
    private GameObject itemInfoPanel;
    private Item item;
    private int number;

    /* Get Function */
    public Item GetItem() { return item; }
    public int GetNumber() { return number; }

    /* Set Function */
    public void SetItem(Item item) { this.item = item; }
    public void SetNumber(int number) { this.number = number; }

    void Awake()
    {
        slotImage = GetComponent<Image>();
        if (slotImage == null)
            Debug.LogError($"QuickSlot '{name}' requires an Image component.", this);

        item = null;
        itemInfoPanel = (transform.childCount > 0) ? transform.GetChild(0).gameObject : null;
    }
    
    void Start()
    {

    }

    public void SelectSlot()
    {
        slotImage.sprite = PlayerUI.Instance.selectSlotSprite;
        // 슬롯에 아이템 정보가 있으면 아이템 정보 패널 업데이트
    }

    public void UnselectSlot()
    {
        slotImage.sprite = PlayerUI.Instance.defaultSlotSprite;
    }
}
