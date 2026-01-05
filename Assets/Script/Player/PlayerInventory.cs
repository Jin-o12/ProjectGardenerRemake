using System;
using System.Collections.Generic;
using UnityEngine;

enum ItemType
{
    Static,     // 고정 아이템 (장비 등)
    Material    // 소모품 아이템
}

enum StaticItemType
{
    Weapon,     // 총기
    Bullet,     // 탄약
}

public class PlayerInventory : MonoBehaviour
{
    /* Component */
    private InputSettings InputSettings;

    /* Inventory Data */
    private Dictionary<StaticItemType, Item> staticItem = new Dictionary<StaticItemType, Item>();    // 스태틱 아이템 리스트 (고정적으로 장비되는 아이템)
    private List<Item> materialItems = new List<Item>(); // 소모품 아이템 리스트

    /* Get Function */
    const int eqipmentNum = 2;
    public Dictionary<StaticItemType, Item> GetStaticItemTypes() { return staticItem; }
    public Dictionary<StaticItemType, Item> GetMaterialItems() { return materialItems; }
    
    void Start()
    {
        InputSettings = PlayerManager.Instance.InputSettings;
    }

    void Update()
    {
        QuickSlot();
    }

    /* 인벤토리 초기화 */
    public void InitInventory(bool _loadData)
    {
        if(_loadData)
        {
            // 세이브 데이터 가져와 초기화
        }
        else
        {
            // 인벤 모두 비우고 기본 장비 세팅
            foreach(List<Item> materialItems in materialItems)
            {
                materialItems = null;
            }

            /* 장비 초기화: 갯수가 얼마 안되고 반복으로 실행하기에 더 복잡하니 직접 초기화 */
            Item defaultGun = ItemDataLoader.ItemDataFinder(201);
            Bullet defaultBullet = ItemDataLoader.ItemDataFinder(202);
            
            staticItem.Add(StaticItemType.Weapon, defaultGun);
            staticItem.Add(StaticItemType.Weapon, defaultBullet);
        }
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
