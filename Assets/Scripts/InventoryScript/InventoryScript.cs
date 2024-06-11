using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public BulletList[] BulletLists;
    public GameObject inventoryCanvas; // Tham chiếu đến Canvas Inventory
    public GameObject inventorySlotPrefab; // Prefab cho ô inventory
    public Transform inventoryGrid; // Grid Layout Group trong Panel

    [System.Serializable]
    public class BulletList
    {
        public GameObject Bullet;
    }

    public List<BulletList> inventory = new List<BulletList>(); // Danh sách chứa các item được nhặt

    private void Start()
    {
        inventoryCanvas.SetActive(true);
    }

    void Update()
    {
        
        UpdateInventoryUI();
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventory();

        }
    }

    void ToggleInventory()
    {
        if (inventoryCanvas != null)
        {
            Debug.Log("inventory off");
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
            if (inventoryCanvas.activeSelf)
            {
                
                UpdateInventoryUI();
                
            }
        }
        else
        {
            Debug.LogWarning("Inventory Canvas is not assigned in the Inspector.");
        }
    }
    
    public void AddItemToInventory(BulletList item)
    {
        BulletList itemCopy = new BulletList();
        itemCopy.Bullet = item.Bullet;

        inventory.Add(itemCopy);
        Debug.Log("Item added to inventory: " + itemCopy.Bullet.name);
    }

    public void UpdateInventoryUI()
    {

        // Xóa tất cả các ô inventory cũ
        foreach (Transform child in inventoryGrid)
        {
            Destroy(child.gameObject);
        }

        // Tạo ô inventory mới dựa trên danh sách inventory
        foreach (BulletList item in inventory)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryGrid);
            slot.SetActive(true); // Đảm bảo GameObject slot được kích hoạt
            
            Image icon = slot.GetComponent<Image>(); // Giả sử InventorySlot có một Image component
            
            icon.sprite = item.Bullet.GetComponent<SpriteRenderer>().sprite; // Lấy sprite từ GameObject
            icon.gameObject.SetActive(true); // Đảm bảo Image component được kích hoạt

        }
    }
}
