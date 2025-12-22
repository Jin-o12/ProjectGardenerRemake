/// <summary>
/// 아이템 데이터 로더 및 아이템 클래스
/// 고유 번호: 100번 대 번호를 사용
/// </summary>

using UnityEngine;

public class ItemDataLoader : MonoBehaviour
{
    public int id;
    public string itemName;
    public string description;
    public string prefabPath;
    public string imagePath;
}

public class Item
{
    public int id;
    public string itemName;
    public string description;
    public Sprite icon;

    public Item(int id, string itemName, string description, Sprite icon)
    {
        this.id = id;
        this.itemName = itemName;
        this.description = description;
        this.icon = icon;
    }

    
}
