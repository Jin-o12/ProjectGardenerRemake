/// <summary>
/// 스폰된 몬스터의 이동, 공격, 패턴 등 모든 동작 및 상호작용을 관리합니다. 
/// </summary>
using UnityEngine;

public class MonsterObject : MonoBehaviour
{
    Monster monsterData; // 몬스터 데이터 참조

    void Start()
    {
        monsterData = GetComponent<Monster>();
    }

    void Update()
    {
        Move();
        Attack();
    }

    private void OnTriggerEnter(Collider collision)
    {
        // 부딫힌 물체가 Bullet 컴포넌트를 가지고 있는 경우 (충돌한 물체가 공격일 경우)
        if (collision.TryGetComponent<BulletObject>(out BulletObject bulletData))
        {
            OnMonsterHit(bulletData);
        }
    }

    /* 플레이어로 공격으로 인한 몬스터의 피격 판정 처리 */
    public void OnMonsterHit(BulletObject bulletData)
    {
        /* 피격 애니메이션 있을 경우 재생 */

        //GetDamage(bulletData.bullet.GetDamage());

        /* 만일 사망 체력일 경우 사망 및 삭제 처리 */
    }

    /* 몬스터의 이동 */
    private void Move()
    {
        // 플레이어 쪽으로 이동
    
        
    }

    /* 몬스터의 공격 */
    private void Attack()
    {
        float attackRange = monsterData.GetAttackRange();
        if(isPlayerInRange(attackRange))
        {
            // 플레이어에게 공격
        }
    }

    /* 플레이어가 특정 범위 내에 있는지에 대한 판정 */
    private bool isPlayerInRange(float _distance)
    {
        // 플레이어와 몬스터 간의 거리 계산 후 판정
        float distance = Vector3.Distance(transform.position, PlayerManager.Instance.transform.position);
        if (distance <= _distance)
            return true;
        else
            return false;
    }

    /* 데미지 계산 처리 */
    private void GetDamage(int _dmg)
    {
        monsterData.AddHealth(_dmg);
        IsMonsterDead();
    }

    /* 몬스터의 사망 판정 처리 */
    private void IsMonsterDead()
    {
        if (monsterData.GetHealth() <= 0)
        {
            MonsterDie();
        }
    }

    /* 몬스터 사망 처리 */
    private void MonsterDie()
    {
        monsterData.DestroyMonster();
    }
}
