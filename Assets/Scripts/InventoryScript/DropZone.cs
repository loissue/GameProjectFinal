using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public enum DropZoneType
    {
        Inventory,
        Weapon
    }

    public DropZoneType dropZoneType;

    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem draggableItem = eventData.pointerDrag.GetComponent<DraggableItem>();
        if (draggableItem != null)
        {
            Transform originalSlot = draggableItem.OriginalParent;
            Transform newSlot = transform;

            if (newSlot.childCount > 0)
            {
                // Nếu slot này đã có item, hoán đổi vị trí
                Transform existingItem = newSlot.GetChild(0);
                existingItem.SetParent(originalSlot);
                existingItem.localPosition = Vector3.zero;

                // Cập nhật slot hiện tại của item cũ
                DraggableItem existingDraggableItem = existingItem.GetComponent<DraggableItem>();
                if (existingDraggableItem != null)
                {
                    existingDraggableItem.OriginalParent = originalSlot;
                }
            }

            // Di chuyển item được kéo vào slot này
            draggableItem.transform.SetParent(newSlot);
            draggableItem.transform.localPosition = Vector3.zero;
            draggableItem.OriginalParent = newSlot; // Cập nhật slot hiện tại của item mới
        }
    }
}
