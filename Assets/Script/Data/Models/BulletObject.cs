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
    public SpriteRenderer bodyRenderer; // 이동 스크립트와 콜라이더만 있는 껍데기
    
    [Header("총알 발사를 구현 할 요소들")]
    public GameObject particlePrefab;
    public Sprite bulletImage;

    /* 고정값, 안바꿔도 됨 */
    [Header ("GameObject Info")]
    private Transform FirePoint;

    void Start()
    {
        // 1. 렌더러 찾기
        bodyRenderer = GetComponentInChildren<SpriteRenderer>();

        // 2. [조립] 이미지 적용
        if (bulletImage != null && bodyRenderer != null)
        {
            bodyRenderer.sprite = bulletImage;
        }

        // 3. [조립] 파티클 생성 및 부착
        if (particlePrefab != null)
        {
            // 내 위치에 파티클 생성
            GameObject vfx = Instantiate(particlePrefab, transform.position, transform.rotation);
            
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
        // 충돌 로직...
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
