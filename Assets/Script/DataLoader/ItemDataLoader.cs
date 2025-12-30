/// <summary>
/// 아이템 데이터 로더 및 아이템 클래스
/// 고유 번호: 100번 대 번호를 사용
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

/* 총알 아이템 클래스 */
public class Bullet: Item
{
    public int damage;          // 총알 데미지

    public Bullet(int id, string itemName, string description, Sprite icon, int damage, string prefabPath, string imagePath)
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

        // 아이템 데이터 로드 (예: JSON, XML, 데이터베이스 등)
        // 여기서는 예시로 하드코딩된 데이터를 사용
        itemDataList.Add(new ItemDataRapper(new Bullet(100, "Basic Bullet", "기본 총알", null, 10, "Prefabs/Bullet", "Images/Bullet")));
        itemDataList.Add(new ItemDataRapper(new Bullet(101, "Explosive Bullet", "폭발성 총알", null, 20, "Prefabs/ExplosiveBullet", "Images/ExplosiveBullet")));

        return itemDataList;
    }
}


