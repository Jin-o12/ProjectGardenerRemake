using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    /* Component */
    private InputSettings InputSettings;

    void Start()
    {
        InputSettings = PlayerManager.Instance.InputSettings;
    }

    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (Input.GetKeyDown(InputSettings.Firing))
        {
            Debug.Log("Fire!");
        }
    }
}
