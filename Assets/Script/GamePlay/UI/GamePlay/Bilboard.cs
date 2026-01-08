using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // 성능을 위해 시작할 때 메인 카메라를 한 번만 찾아 저장합니다.
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        // LateUpdate는 모든 카메라 이동이 끝난 후 호출되므로 떨림 현상이 없습니다.
        if (mainCamera != null)
        {
            // 오브젝트의 앞쪽 방향(transform.forward)을
            // 카메라의 앞쪽 방향(mainCamera.transform.forward)과 일치시킵니다.
            transform.forward = mainCamera.transform.forward;
        }
    }
}