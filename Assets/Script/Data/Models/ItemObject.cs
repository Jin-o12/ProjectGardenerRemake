using UnityEngine;


public class ItemObject : MonoBehaviour
{
    private Item item;
    private int number;

    /* Get Function */
    public Item GetItem() { return item; }
    public int GetNumber() { return number; }

    /* Set Function */
    public void SetItem(Item item) { this.item = item; }
    public void SetNumber(int number) { this.number = number; }
}
