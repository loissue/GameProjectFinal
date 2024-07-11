using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public GameObject pickupText; // Tham chiếu đến UI Text hiển thị chữ "E"
    public InventoryScript inventory; // Tham chiếu đến Inventory

    private ItemScript item; // Tham chiếu đến Item script

    void Start()
    {
        pickupText.SetActive(false); // Ẩn UI Text khi bắt đầu
        item = GetComponent<ItemScript>(); // Lấy Item script từ đối tượng

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            if (pickupText != null)
            {
                pickupText.SetActive(true); // Hiện UI Text khi player đi vào vùng va chạm
                
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            if (pickupText != null)
            {
                pickupText.SetActive(false); // Ẩn UI Text khi player rời khỏi vùng va chạm
            }
        }
    }
    
    void Update()
    {
        if (pickupText.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            //inventory.Add(item); // Thêm item vào inventory
            Destroy(gameObject); // Hủy đối tượng item sau khi nhặt
        }
    }
}
