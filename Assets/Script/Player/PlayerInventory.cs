using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    /* Component */
    private InputSettings inputSettings;

    /* Inventory Data */
    private Dictionary<String, Item> staticItems = new Dictionary<String, Item>();    // 스태틱 아이템 리스트 (고정적으로 장비되는 아이템)
    private List<Item> MaterialItems = new List<Item>(); // 소모품 아이템 리스트

    void Start()
    {
        inputSettings = PlayerManager.Instance.InputSettings;
    }

    void Update()
    {
        QuickSlot();
    }
    
    /* 게임 시작시 한번 실행되어 초기화 등을 수행 */
    private void InitInventory()
    {
        InitStaticItems();
    }

    /* 퀵슬롯 사용 처리 */
    private void QuickSlot()
    {
        int num = 0;
        if (Input.GetKeyDown(inputSettings.QuickSlot1))
        {
            num = 1;
        }
        if (Input.GetKeyDown(inputSettings.QuickSlot2))
        {
            num = 2;
        }
        if (Input.GetKeyDown(inputSettings.QuickSlot3))
        {
            num = 3;
        }
        if (Input.GetKeyDown(inputSettings.QuickSlot4))
        {
            num = 4;
        }
        if (Input.GetKeyDown(inputSettings.QuickSlot5))
        {
            num = 5;
        }

        if (num == 0)
            return;
        PlayerUI.Instance.UpdateSelectSlot(num);
    }

    /* 고정 아이템 초기화 */
    private void InitStaticItems()
    {
        
    }
}
