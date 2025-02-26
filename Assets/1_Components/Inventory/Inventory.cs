using UnityEngine;
using UnityEngine.UI;

public enum InventoryType { Buy, Sell }

public class Inventory : MonoBehaviour
{
    public InventoryType inventoryType;

    [SerializeField] public InventorySlot[] slots;
    [SerializeField] public InventoryItem itemPrefab; 
    [SerializeField] Item[] items;
    public Inventory otherInventory;
    public void Start()
    {
        for (int i = 0; i < 10; i++) 
        {
            SpawnItem();
        }


    }
    public void SpawnItem(Item item = null)
    {
        Item _item = item ?? PickRandomItem();

        foreach (var slot in slots)
        {
            if (slot.myItem == null)
            {
                InventoryItem newItem = Instantiate(itemPrefab, slot.transform);
                newItem.Initialize(_item, slot);
                break;
            }
        }
    }

    Item PickRandomItem()
    {
        if (items == null || items.Length == 0)
        {
            Debug.LogWarning("Item list is empty");
            return null;
        }
        int index = Random.Range(0, items.Length);
        return items[index];
    }

    public void TransferSelectedItems()
    {
        if (otherInventory == null)
        {
            Debug.LogWarning("Other Inventory reference is not assigned");
            return;
        }
        TransferSelectedItemsTo(otherInventory);
    }

    public void TransferSelectedItemsTo(Inventory target)
    {
        foreach (var slot in slots)
        {
            if (slot.myItem != null && slot.myItem.isSelected)
            {
                InventoryItem item = slot.myItem;
                slot.myItem = null;
                slot.SetSelected(false);

                foreach (var targetSlot in target.slots)
                {
                    if (targetSlot.myItem == null)
                    {
                        item.transform.SetParent(targetSlot.transform);
                        item.transform.localPosition = Vector3.zero;
                        targetSlot.myItem = item;
                        item.activeSlot = targetSlot;
                        item.SetSelected(false);
                        break;
                    }
                }
            }
        }
    }

}
