using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem myItem { get; set; }

    [SerializeField] private GameObject selectionFrame;

    public void SetSelected(bool selected)
    {
        if (selectionFrame != null)
            selectionFrame.SetActive(selected);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (myItem != null)
        {
            myItem.SetSelected(!myItem.isSelected);
        }
    }
}
