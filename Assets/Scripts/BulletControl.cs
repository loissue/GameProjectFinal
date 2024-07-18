using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletControl : MonoBehaviour
{
    private RandomUpgrade chestopen;
    public InventoryScript InventoryScript;
    public GameObject pickupText; // Tham chiếu tới UI Text để hiển thị thông báo
    public PlayerInteract playerInteract;
    private bool isNearItem = false;
    private InventoryScript.BulletList currentItem; // Item hiện tại mà player đang đứng gần

    private GameObject currentItemObject; // Đối tượng item hiện tại mà player đang đứng gần
    private string tags;

    void Start()
    {
        pickupText.SetActive(false); // Ban đầu ẩn thông báo
    }

    void Update()
    {
        if (isNearItem && Input.GetKeyDown(KeyCode.E) && tags == "Bullet")
        {
            InventoryScript.AddItemToInventory(currentItem); // Thêm item vào inventory
            isNearItem = false; // Đặt lại biến kiểm tra

            InventoryScript.UpdateInventoryUI();
            pickupText.SetActive(false); // Ẩn thông báo sau khi nhặt item
            currentItemObject.SetActive(false);
        }
        else if (isNearItem && Input.GetKeyDown(KeyCode.E) && tags == "Weapon")
        {
            playerInteract.UnequipCurrentWeapon(); // Gọi hàm để xóa vũ khí hiện tại
            playerInteract.EquipWeapon(currentItemObject); // Equip vũ khí mới
            isNearItem = false; // Đặt lại biến kiểm tra
            pickupText.SetActive(false); // Ẩn thông báo sau khi nhặt item
        }
        else if (isNearItem && Input.GetKeyDown(KeyCode.E) && tags == "Chest")
        {
            isNearItem = false; // Đặt lại biến kiểm tra
            chestopen.RandomDrop();
            chestopen = null;
        }
        else if (isNearItem && Input.GetKeyDown(KeyCode.E) && tags == "buffbullet")
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
        if (other.CompareTag("Bullet")) {
            InventoryScript.BulletList item = other.gameObject.GetComponent<Item>().bulletList; // Giả sử item có component chứa thông tin về BulletList
            if (item != null)
            {
                tags = "Bullet";
                currentItem = item;
                currentItemObject = other.gameObject;
                isNearItem = true;
                pickupText.SetActive(true); // Hiện thông báo khi player ở gần item
            }
        }
        else if (other.CompareTag("Weapon"))
        {
            isNearItem = true;
            tags = "Weapon";
            currentItemObject = other.gameObject;
            pickupText.SetActive(true);
        }
        else if (other.CompareTag("Chest"))
        {
            isNearItem = true;
            tags = "Chest";
            currentItemObject = other.gameObject;
            pickupText.SetActive(true);
            chestopen = other.GetComponent<RandomUpgrade>();
        }
        else if (other.CompareTag("buffbullet")) {
            InventoryScript.BulletList item = other.gameObject.GetComponent<Item>().bulletList; // Giả sử item có component chứa thông tin về BulletList
            if (item != null)
            {
                tags = "Bullet";
                currentItem = item;
                currentItemObject = other.gameObject;
                isNearItem = true;
                pickupText.SetActive(true); // Hiện thông báo khi player ở gần item
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
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
        else if (other.CompareTag("Weapon"))
        {
            isNearItem = false;
            pickupText.SetActive(false);
        }
        else if (other.CompareTag("Chest"))
        {
            isNearItem = false;
            pickupText.SetActive(false);
            chestopen = null;
        }
        else if (other.CompareTag("buffbullet"))
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
}
