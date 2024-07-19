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
        if (isNearItem && Input.GetKeyDown(KeyCode.E) && tags == "Weapon")
        {
            // Gọi hàm để tách và đánh rơi vũ khí hiện tại nếu có
            DropCurrentWeapon();

            // Equip vũ khí mới
            playerInteract.EquipWeapon(currentItemObject);

            isNearItem = false; // Đặt lại biến kiểm tra
            pickupText.SetActive(false); // Ẩn thông báo sau khi nhặt item
        }
        else if (isNearItem && Input.GetKeyDown(KeyCode.E) && tags == "Chest")
        {
            isNearItem = false; // Đặt lại biến kiểm tra
            chestopen.RandomDrop();
            chestopen = null;
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

    private void DropCurrentWeapon()
    {
        // Kiểm tra nếu người chơi đang trang bị vũ khí
        GameObject currentWeapon = playerInteract.GetEquippedWeapon();
        if (currentWeapon != null)
        {
            // Tách vũ khí hiện tại khỏi người chơi
            currentWeapon.transform.SetParent(null);
            currentWeapon.transform.position = transform.position; // Đặt lại vị trí của vũ khí tại vị trí của người chơi
            currentWeapon.SetActive(true);

            // Đảm bảo vũ khí bị đánh rơi có thể nhặt lại
            if (currentWeapon.GetComponent<Collider2D>() == null)
            {
                currentWeapon.AddComponent<BoxCollider2D>();
            }

            // Đặt tag cho vũ khí bị đánh rơi để có thể tương tác lại
            currentWeapon.tag = "Weapon";

            // Gỡ bỏ vũ khí hiện tại khỏi người chơi
            playerInteract.UnequipCurrentWeapon();
        }
    }
}
