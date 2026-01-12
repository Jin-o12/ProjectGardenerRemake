/// <summary>
/// 총알의 이동, 충돌 후 상호작용을 관리합니다.
/// 총알 프리팹에 기본적으로 세팅되어 있는 컴포넌트로, Bullet 클래스를 받아 관련 작업을 처리합니다.
/// </summary>
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    public Item bullet; // 총알 데이터 참조

    /* 테스트를 위한 임시 함수, 변동될 데이터 */
    [Header("기본 틀")]
    public float speed = 10f;
    public float lifeTime = 3f;
    public int dmg = 2;
    
    [Header("총알 발사를 구현 할 요소들")]
    public GameObject particlePrefab;
    public Sprite bulletImage;
    //public SpriteRenderer ImageObj; // 이미지를 가질 자식 오브젝트 (회전해야 하기 때문에 별도로 배치)

    /* 고정값, 안바꿔도 됨 */
    [Header ("GameObject Info")]
    private Transform FirePoint;
    public float particleSize;      // 파티클 크기
    public float visualOffsetY;     // 총알 위치 보정

    void Start()
    {
        if (particlePrefab != null)
        {
            // 내 위치에 파티클 생성
            GameObject vfx = Instantiate(particlePrefab, transform.position, transform.rotation);

            // 파티클이 보기에 파뭍히지 않게 y축 보정
            vfx.transform.localPosition = new Vector3(0, visualOffsetY, 0);
            vfx.transform.localRotation = Quaternion.identity;

            // 이미지 연결
            ParticleSystemRenderer psr = vfx.GetComponent<ParticleSystemRenderer>();
            Material mat = psr.material;            // 머테리얼의 복사본 가져옴
            mat.mainTexture = bulletImage.texture;  // 메인 텍스쳐를 해당 이미지로 설정

            // 파티클 크기 조정
            ParticleSystem ps = vfx.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                // 구조체라 변수에 담아서 수정해야 함.
                var mainModule = ps.main;

                // 인스펙터에서 설정한 크기로 변경
                // (StartSize는 3D 공간에서의 크기를 의미)
                mainModule.startSize = particleSize;
            }
            
            // 나를 부모로 설정 (함께 이동)
            vfx.transform.SetParent(this.transform);
            
            // 위치 정렬
            vfx.transform.localPosition = Vector3.zero;
            vfx.transform.localRotation = Quaternion.identity;
        }

        // 4. 수명 설정 (자동 파괴)
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // 5. 이동 (설정된 속도로)
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("dmg: "+dmg);
            MonsterObject monster = other.GetComponent<MonsterObject>();
            monster.GetDamage(dmg);
            Destroy(gameObject);
        }
    }
}
