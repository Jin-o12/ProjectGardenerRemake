using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Collider wouldzone;                // 필드 구역
    [SerializeField] private Collider spawnzone;                // 몬스터 스폰 구역
    [SerializeField] private Transform MonsterObjectRoot;       // 몬스터 오브젝트를 가지고 있을 부모 오브젝트, 여기에 몬스터 스폰

    /* 몬스터 데이터 관리 밎 저장 */
    private List<Monster> nowMonsterList = new List<Monster>();
    private bool MonsterSpawnLoopFlag = true;
    private int maxAttempts = 50;                               // 몬스터 스폰 최대 시도 횟수 (지나친 스폰 및 연산 방지)

    void Update()
    {
        if(MonsterSpawnLoopFlag)
        {
            MonsterSpawnLoop();
        }
    }

    /* 스폰 리스트에 몬스터 추가 */
    public void InsertSpawnMonsterList(Monster _monster)
    {
        nowMonsterList.Add(_monster);
    }

    /* 스폰 리스트에 몬스터 제거 */
    public void RemoveSpawnMonsterList(Monster _monster)
    {
        nowMonsterList.Remove(_monster);
    }

    private void MonsterSpawnLoop()
    {
        Vector3 spawnPosition = SpawnzoneFinder();
        MonsterSpawn(spawnPosition, nowMonsterList[0]/*여기는 차후 변경*/);
    }

    private Vector3 SpawnzoneFinder()
    {
        foreach (Monster monster in nowMonsterList)
        {
            // 콜라이더 범위 가져옴
            Bounds spawnBounds = spawnzone.bounds;

            // 범위 내에 몬스터 스폰 로직
            for (int i = 0; i < maxAttempts; i++)
            {
                // 1. 스폰존에서 랜덤 좌표 생성
                float x = Random.Range(spawnBounds.min.x, spawnBounds.max.x);
                float z = Random.Range(spawnBounds.min.z, spawnBounds.max.z);
                float y = transform.position.y;

                Vector3 randomPoint = new Vector3(x, y, z);

                // 2. 생성된 점이 필드 안에 있는지 검사, 참일 시 다시 좌표 생성
                if (wouldzone.bounds.Contains(randomPoint))
                    continue;

                // 유효한 위치 리턴
                return randomPoint;
            }
        }

        // 반복 횟수를 초과했는데도 자리를 못 찾았을 때 (예외 처리)
        Debug.LogWarning("스폰 위치를 찾지 못했습니다.");
        return Vector3.zero;
    }

    private void MonsterSpawn(Vector3 _spawnPosition, Monster _monster)
    {
        // 몬스터 생성 (프리팹, 위치, 회전, 부모 오브젝트)
        Instantiate(_monster.GetPrefab(), _spawnPosition, Quaternion.identity, MonsterObjectRoot);
    }
}
