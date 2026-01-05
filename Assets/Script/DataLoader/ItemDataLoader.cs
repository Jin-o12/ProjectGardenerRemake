/// <summary>
/// 아이템 데이터 로더 및 아이템 클래스
/// 고유 번호: 
///     소모성 아이템: 100번 대 번호 사용
///     고유 장비: 200번 대 번호 사용, 단 201번은 무기 정보
/// </summary>
using UnityEngine;
using System.Collections.Generic;

/* 아이템 클래스 */
public class Item
{
    public int id;              // 아이템 고유 번호
    public string itemName;     // 아이템 이름
    public string description;  // 아이템 설명
    public Sprite icon;         // 아이템 아이콘
    public string prefabPath;   // 아이템 프리팹 경로
    public string imagePath;    // 아이템 이미지 경로

    public Item(int id, string itemName, string description, Sprite icon, string prefabPath, string imagePath)
    {
        this.id = id;
        this.itemName = itemName;
        this.description = description;
        this.icon = icon;
        this.prefabPath = prefabPath;
        this.imagePath = imagePath;
    }
}

/* 무기 아이템 클래스 */
public class Weapon: Item
{
    public int damage;          // 총알 데미지
    public string FireFuncName; // 발사시 투사체 이동 방식

    public Weapon(int id, string itemName, string description, Sprite icon, int damage, string prefabPath, string imagePath)
        : base(id, itemName, description, icon, prefabPath, imagePath)
    {
        this.damage = damage;
    }

    public int GetDamage() { return damage; }
}

/* 아이템 데이터 래퍼 클래스 */
public class ItemDataRapper
{
    public Item item;
    
    public ItemDataRapper(Item item)
    {
        this.item = item;
    }
}

/* 아이템 데이터 로더 */
public class ItemDataLoader
{
    public List<ItemDataRapper> LoadItemData()
    {
        List<ItemDataRapper> itemDataList = new List<ItemDataRapper>();

        // 아이템 불러오는 코드 (차후 반복문으로 바꾸고 소모성 아이템도 불러오게 할 것)
        itemDataList.Add(new ItemDataRapper(new Weapon(201, "Gun", "무기", null, 20, "Prefabs/Weapon", "Images/Weapon")));
        itemDataList.Add(new ItemDataRapper(new Weapon(202, "Basic Bullet", "기본 총알", null, 10, "Prefabs/DefultBullet", "Images/DefultBullet")));

        return itemDataList;
    }

    /* item 데이터 검색 및 반환 */
    public Item ItemDataFinder(int _id)
    {
        Item item;
        foreach(Item itemData in itemDataList)
        {
            if(itemData.id == _id)
            {
                item = itemData;
                break;
            }
        }
        return item;
    }

    /* Weapon 데이터 검색 및 반환 */
    public Weapon ItemDataFinder(int _id)
    {
        Weapon weapon;
        foreach(Weapon itemData in itemDataList)
        {
            if(itemData.id == _id)
            {
                weapon = itemData;
                break;
            }
        }
        return weapon;
    }
}


