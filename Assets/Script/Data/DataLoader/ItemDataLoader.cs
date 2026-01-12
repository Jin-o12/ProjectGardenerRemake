/// <summary>
/// 아이템 데이터 로더 및 아이템 클래스
/// 고유 번호: 
///     소모성 아이템: 100번 대 번호 사용
///     총알 아이템: 200번 대 번호 사용
/// </summary>
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

enum ItemType
{
    MATERIAL,
    EQUIP
}

/// 규칙적이지 않은 텍스트를 정렬하고 싶은데, 어떻게 할지 고민 중 ///

/* 아이템 클래스 */
public class Item
{
    // CSV 순서: ID,Type,ItemKey,NameKey,DescKey,FireType,HitEffectPath,Price

    // 기본 정보
    public int id;                  // 아이템 고유 번호
    public string type;

    // 키 코드
    public string ItemKey;
    public string NameKey;
    public string DescKey;

    public string fireFuncName;     // 발사 함수 이름
    public string HitEffectPath;    // 히트 이펙트 경로

    // 리소스 경로
    public string prefabPath;       // 프리팹
    public string imagePath;        // 이미지

    // 텍스트 데이터 키
    public string Name => ItemDataLoader.Instance.GetText(NameKey);  // "ITEM_NAME_###"
    public string Description => ItemDataLoader.Instance.GetText(DescKey);  // "ITEM_DESC_###"

    // 전투 정보 (총알 전용)
    public int damage;              // 데미지
    

    // 필요할 때 아이콘 이미지 로드
    public Sprite LoadIcon()
    {
        return Resources.Load<Sprite>(imagePath);
    }
}

/* 아이템 데이터 로더 */
public class ItemDataLoader : MonoBehaviour
{
    // 싱글톤으로 처리
    public static ItemDataLoader Instance;

    // 아이템 데이터 사전
    public Dictionary<int, Item> ItemDataDict = new Dictionary<int, Item>();
    // 아이템 텍스트 사전
    private Dictionary<string, string> ItmeTextDict = new Dictionary<string, string>();

    private void Awake()
    {
        // 싱글톤 보장 및 씬 전환 시 파괴 방지
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀔 때 파괴 방지
        }
        else
        {
            // 이미 매니저가 존재한다면, 새로 생긴 인스턴스는 스스로 파괴
            Destroy(gameObject);
        }

        LoadLocalization("KR"); // 언어 로드  ==>> ※차후 설정 메뉴 제작시 로컬 데이터로 언어 설정 저장되게 바꿀 것※
        LoadItemTable();        // 2. 그 다음 아이템 테이블 로드
    }

    /* 언어 타입을 받아 해당하는 언어 파일을 로드한다 */
    public void LoadLocalization(string langCode)
    {
        // 해당 언어의 파일 위치
        TextAsset jsonFile = Resources.Load<TextAsset>($"GameData/Localization/Strings_{langCode}");

        if (jsonFile != null)
        {
            ItmeTextDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonFile.text);
            Debug.Log($"언어 데이터 로드 완료: {langCode} (총 {ItmeTextDict.Count}개)");
        }
        else
        {
            Debug.LogError("언어 파일을 찾을 수 없습니다!");
        }
    }

    public string GetText(string key)
    {
        if (ItmeTextDict.ContainsKey(key)) return ItmeTextDict[key];
        return key; // 번역 없으면 키값 그대로 리턴 (에러 방지)
    }

    /* 아이템 데이터(CSV 데이터) 로드 */
    public void LoadItemTable()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("Data/GameData/ItemTable");

        if (csvFile == null)
        {
            Debug.LogError("아이템 테이블 CSV를 찾을 수 없습니다!");
            return;
        }

        // 줄바꿈으로 행 분리
        string[] lines = csvFile.text.Split('\n');
        ItemDataDict.Clear();

        // [중요] CSV 데이터 순서 정의
        // 0:ID, 1:Type, 2:ItemKey, 3:NameKey, 4:DescKey, 5:FireFuncName, 6:HitEffectPath, 7:PrefabPath, 8:ImagePath, 9:Damage
        
        for (int i = 1; i < lines.Length; i++) // 1번 인덱스부터 시작 (헤더 건너뛰기)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] cols = lines[i].Split(',');

            // 데이터 개수가 부족하면 스킵 (오류 방지)
            if (cols.Length < 10) 
            {
                // 필요하다면 경고 로그 출력
                // Debug.LogWarning($"{i}번째 줄 데이터 부족: {lines[i]}");
                continue; 
            }

            try
            {
                Item newItem = new Item();

                // 1. 기본 정보
                newItem.id = int.Parse(cols[0]);
                newItem.type = cols[1].Trim();

                // 2. 키 코드
                newItem.ItemKey = cols[2].Trim();
                newItem.NameKey = cols[3].Trim();
                newItem.DescKey = cols[4].Trim();

                // 3. 기능 및 이펙트
                newItem.fireFuncName = cols[5].Trim();
                newItem.HitEffectPath = cols[6].Trim();

                // 4. 리소스 경로
                newItem.prefabPath = cols[7].Trim();
                newItem.imagePath = cols[8].Trim();

                // 5. 전투 정보
                newItem.damage = int.Parse(cols[9]); // 정수형 파싱

                // 딕셔너리에 추가
                if (!ItemDataDict.ContainsKey(newItem.id))
                {
                    ItemDataDict.Add(newItem.id, newItem);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"CSV 파싱 에러 ({i}번째 줄): {e.Message} / 데이터: {lines[i]}");
            }
        }

        Debug.Log($"아이템 로드 완료: 총 {ItemDataDict.Count}개");
    }

    // // 외부에서 아이템을 가져가는 함수
    // public Item GetItem(int id)
    // {
    //     if (itemMap.ContainsKey(id))
    //         return itemMap[id];
        
    //     Debug.LogWarning($"아이템 ID {id}를 찾을 수 없습니다.");
    //     return null;
    // }
}