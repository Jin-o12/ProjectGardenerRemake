using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    /* Component */
    private InputSettings InputSettings;
    private PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
        InputSettings = PlayerManager.Instance.InputSettings;
    }

    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        Dictionary<StaticItemType, Item> staticItem = playerInventory.GetStaticItems();
        if (Input.GetKeyDown(InputSettings.Firing))
        {
            // 총알 생성 (프리팹, 위치, 회전, 부모 오브젝트)
            Instantiate(_monster.GetPrefab(), _spawnPosition, Quaternion.identity, MonsterObjectRoot);
        }
    }
}
