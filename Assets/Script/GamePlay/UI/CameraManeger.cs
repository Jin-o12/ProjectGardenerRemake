using UnityEngine;

public class CameraManeger : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform player;

    private float cameraHeight = 8.8f;
    private float cameraDistance = -9.8f;

    void Awake()
    {
        ChechkReferences();
    }
    
    /* 레퍼런스 체크 */
    private void ChechkReferences()
    {
        if (player == null)
            Debug.LogError("Player reference is missing in CameraManager.");
    }

    void Update()
    {
        FollowPlayer();
    }

    /* 플레이어 위치 추적 */
    private void FollowPlayer()
    {
        if (player != null)
        {
            Vector3 newPosition = new Vector3(player.position.x, cameraHeight, player.position.z+cameraDistance);
            transform.position = newPosition;
        }
    }
}
