using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform inventoryPanel;
    public Transform magazinePanel;
    public int inventorySlotCount = 10;
    public int magazineSlotCount = 4;

    [System.Serializable]
    public class BulletList
    {
        public GameObject Bullet;
    }

    public List<BulletList> inventory = new List<BulletList>(); // Danh sách chứa các item được nhặt

    private List<GameObject> inventorySlots = new List<GameObject>();
    private List<GameObject> magazineSlots = new List<GameObject>();

    void Start()
    {
        CreateSlots(inventoryPanel, inventorySlotCount, inventorySlots);
        CreateSlots(magazinePanel, magazineSlotCount, magazineSlots);
    }

    void CreateSlots(Transform panel, int slotCount, List<GameObject> slotList)
    {
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slot = Instantiate(slotPrefab, panel); // Tạo đối tượng và gán cha
            slot.SetActive(true);
            slotList.Add(slot);
        }
    }

    public void AddItemToInventory(BulletList item)
    {
        BulletList itemCopy = new BulletList();
        itemCopy.Bullet = Instantiate(item.Bullet); // Tạo một bản sao của prefab
        inventory.Add(itemCopy);
        Debug.Log("Item added to inventory: " + itemCopy.Bullet.name);
    }

    public void UpdateInventory()
    {
        inventory.Clear();
        foreach (Transform slot in inventoryPanel)
        {
            DraggableItem draggableItem = slot.GetComponentInChildren<DraggableItem>();
            if (draggableItem != null)
            {
                BulletList bulletList = new BulletList();
                bulletList.Bullet = draggableItem.gameObject; // Thay đổi ở đây để lấy chính GameObject
                inventory.Add(bulletList);
            }
        }
    }

    public void UpdateInventoryUI()
    {
        // Xóa tất cả các ô inventory cũ
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        // Tạo ô inventory mới dựa trên danh sách inventory
        foreach (BulletList item in inventory)
        {
            GameObject slot = Instantiate(slotPrefab, inventoryPanel); // Tạo đối tượng và gán cha
            slot.SetActive(true); // Đảm bảo GameObject slot được kích hoạt

            // Thêm DraggableItem và DropZone cho slot nếu chưa có
            if (slot.GetComponent<CanvasGroup>() == null)
                slot.AddComponent<CanvasGroup>();

            DraggableItem draggableItem = slot.GetComponent<DraggableItem>();
            if (draggableItem == null)
                draggableItem = slot.AddComponent<DraggableItem>();

            draggableItem.Inventory = this; // Gán InventoryScript cho DraggableItem

            DropZone dropZone = slot.GetComponent<DropZone>();
            if (dropZone == null)
                dropZone = slot.AddComponent<DropZone>();
            dropZone.dropZoneType = DropZone.DropZoneType.Inventory;

            Image icon = slot.GetComponent<Image>(); // Giả sử InventorySlot có một Image component
            icon.sprite = item.Bullet.GetComponent<SpriteRenderer>().sprite; // Lấy sprite từ GameObject
            icon.gameObject.SetActive(true); // Đảm bảo Image component được kích hoạt

            // Đặt item vào slot
            GameObject bulletInstance = Instantiate(item.Bullet); // Tạo bản sao của bullet
            bulletInstance.transform.SetParent(slot.transform, false);
            bulletInstance.transform.localPosition = Vector3.zero;

            // Kiểm tra và chỉ đặt anchoredPosition nếu bullet có RectTransform
            RectTransform rectTransform = bulletInstance.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = Vector2.zero;
            }
        }
    }
}
