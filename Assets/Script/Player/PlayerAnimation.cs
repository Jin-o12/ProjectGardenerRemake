/// <summary>
/// 플레이어 행동에 대한 애니메이션 재생 및 관련 파라미터를 관리합니다.
/// 주로 다른 코드에 참조되어 특정 함수가 실행되면, 속성을 바꾸어 애니메이션을 재생하게 합니다.
/// </summary>
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header ("Componant")]
    [SerializeField] public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Walk()
    {}

    public void Dodge()
    {}

    public void Dead()
    {}
}
