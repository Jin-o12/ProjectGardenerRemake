/// <summary>
/// 몬스터 데이터 로더 및 몬스터 클래스
/// 고유 번호: 1000번 대 번호를 사용
/// </summary>
using UnityEngine;
using System.Collections.Generic;

public class Monster
{
    public int id;                 // 몬스터 고유 번호 
    public string entityName;      // 몬스터 이름
    public string description;     // 몬스터 설명
    public string prefabPath;      // 몬스터 프리팹 경로
    public int health;              // 몬스터 체력
    public int attack;              // 몬스터 공격력
    public float moveSpeed;         // 몬스터 이동 속도
    public GameObject prefab;       // 몬스터 프리팹
    public List<int> dropItem;      // 몬스터 드랍 아이템 리스트

    public Monster(string name, int health, float moveSpeed, int attackPower, GameObject prefab)
    {
        this.entityName = name;
        this.health = health;
        this.moveSpeed = moveSpeed;
        this.attack = attackPower;
        this.prefab = prefab;
    }

    /* Get Functuions */
    public string GetName() { return entityName; }
    public int GetHealth() { return health; }
    public float GetMoveSpeed() { return moveSpeed; }
    public int GetAttackPower() { return attack; }
    public GameObject GetPrefab() { return prefab; }

    /* 오브젝트 삭제 */
    public void DestroyMonster()
    {
        GameObject.Destroy(this.prefab);
    }
}

public class MonsterDataLoader
{
    
}
