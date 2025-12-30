using System;
using System.Collections.Generic;
using UnityEngine;

enum ItemType
{
    Static,     // 고정 아이템 (장비 등)
    Material    // 소모품 아이템
}

enum StaticItem
{
    Tool,       // 도구
    Weapon,     // 총기
    Bullet,     // 탄약(투사체)
}

public class PlayerInventory : MonoBehaviour
{
    /* Component */
    private InputSettings InputSettings;

    /* Inventory Data */
    private Dictionary<String, Item> staticItems = new Dictionary<String, Item>();    // 스태틱 아이템 리스트 (고정적으로 장비되는 아이템)
    private List<Item> MaterialItems = new List<Item>(); // 소모품 아이템 리스트

    void Start()
    {
        InputSettings = PlayerManager.Instance.InputSettings;
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
