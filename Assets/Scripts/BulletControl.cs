using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletControl : MonoBehaviour
{
    public InventoryScript InventoryScript;
    public GameObject pickupText; // Tham chiếu tới UI Text để hiển thị thông báo
    
    private bool isNearItem = false;
    private InventoryScript.BulletList currentItem; // Item hiện tại mà player đang đứng gần

    private GameObject currentItemObject; // Đối tượng item hiện tại mà player đang đứng gần

    void Start()
    {
        pickupText.SetActive(false); // Ban đầu ẩn thông báo
    }

    void Update()
    {
        if (isNearItem && Input.GetKeyDown(KeyCode.E))
        {
            InventoryScript.AddItemToInventory(currentItem); // Thêm item vào inventory
            isNearItem = false; // Đặt lại biến kiểm tra

            InventoryScript.UpdateInventoryUI();
            pickupText.SetActive(false); // Ẩn thông báo sau khi nhặt item
            currentItemObject.SetActive(false);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        InventoryScript.BulletList item = other.gameObject.GetComponent<Item>().bulletList; // Giả sử item có component chứa thông tin về BulletList
        if (item != null )
        {
            currentItem = item;
            currentItemObject = other.gameObject ;
            isNearItem = true;
            pickupText.SetActive(true); // Hiện thông báo khi player ở gần item
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        InventoryScript.BulletList item = other.gameObject.GetComponent<Item>().bulletList;
        if (item != null)
        {
            currentItem = null;
            currentItemObject = null;
            isNearItem = false;
            pickupText.SetActive(false); // Ẩn thông báo khi player rời xa item
        }
    }

    
}
