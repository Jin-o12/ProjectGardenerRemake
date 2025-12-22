using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    /* Component */
    private InputSettings InputSettings;

    void Start()
    {
        InputSettings = PalyerInfomation.Instance.InputSettings;
    }

    void Update()
    {
        QuickSlot();
    }
    
    /* 퀵슬롯 사용 처리 */
    private void QuickSlot()
    {
        int num = 0;
        if (Input.GetKeyDown(InputSettings.QuickSlot1))
        {
            num = 1;
        }
        if (Input.GetKeyDown(InputSettings.QuickSlot2))
        {
            num = 2;
        }
        if (Input.GetKeyDown(InputSettings.QuickSlot3))
        {
            num = 3;
        }
        if (Input.GetKeyDown(InputSettings.QuickSlot4))
        {
            num = 4;
        }
        if (Input.GetKeyDown(InputSettings.QuickSlot5))
        {
            num = 5;
        }

        if (num == 0)
            return;
        PlayerUI.Instance.UpdateSelectSlot(num);
    }
}
