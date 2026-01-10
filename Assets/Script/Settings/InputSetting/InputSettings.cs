using UnityEngine;


[CreateAssetMenu(fileName = "InputSettings", menuName = "Settings/Input")]
public class InputSettings : ScriptableObject
{
    /* 플레이어 이동*/
    [Header("Movement Keys")]
    public KeyCode MoveUp = KeyCode.W;
    public KeyCode MoveDown = KeyCode.S;
    public KeyCode MoveLeft = KeyCode.A;
    public KeyCode MoveRight = KeyCode.D;

    /* 플레이어 상호작용 */
    [Header("Action Keys")]
    public KeyCode Dodge = KeyCode.Space;
    public KeyCode Interact = KeyCode.Mouse1;
    public KeyCode Firing = KeyCode.Mouse0;
    public KeyCode QuickSlot1 = KeyCode.Alpha1;
    public KeyCode QuickSlot2 = KeyCode.Alpha2;
    public KeyCode QuickSlot3 = KeyCode.Alpha3;
    public KeyCode QuickSlot4 = KeyCode.Alpha4;
    public KeyCode QuickSlot5 = KeyCode.Alpha5;
}