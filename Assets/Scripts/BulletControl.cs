using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BulletControl : MonoBehaviour
{
    private RandomUpgrade chestopen;
    public InventoryScript InventoryScript;
    public GameObject pickupText; // Tham chiếu tới UI Text để hiển thị thông báo
    public Text message;
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
        if (isNearItem && Input.GetKeyDown(KeyCode.E) && tags == "Weapon")
        {
            
            isNearItem = false; // Đặt lại biến kiểm tra

            playerInteract.EquipWeapon(currentItemObject);
            
            

        }

        if (isNearItem && Input.GetKeyDown(KeyCode.E) && tags == "Chest")
        {
            if (ScoringManagement.instance.score >= 5)
            {
                isNearItem = false; // Đặt lại biến kiểm tra
                ScoringManagement.instance.AddScore(-5);
                chestopen.RandomDrop();
                chestopen = null;
            }
            else
            {
                StartCoroutine(DisplayTextTemp(10));
            }
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
        if (other.CompareTag("Weapon"))
        {
                isNearItem = true;
                tags = "Weapon";
                currentItemObject = other.gameObject;
                pickupText.SetActive(true);
        }
        RandomUpgrade upgrade = other.GetComponent<RandomUpgrade>();
        if (other.CompareTag("Chest"))
        {
                isNearItem = true;
                tags = "Chest";
                currentItemObject = other.gameObject;
                chestopen = upgrade;
                pickupText.SetActive(true);
        }
        if (other.CompareTag("Portal"))
        {
            if (NewLevel.instant != null)
            {
                NewLevel.instant.nextLevel();
                Debug.Log("Instance created and nextLevel method called.");
            }
            else
            {
                Debug.Log("Instance not created.");
            }
            transform.position = new Vector2(29, -3);
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
        if (other.CompareTag("Weapon"))
        {
            currentItem = null;
            currentItemObject = null;
            pickupText.SetActive(false);
        }
        RandomUpgrade upgrade = other.GetComponent<RandomUpgrade>();
        if (other.CompareTag("Chest"))
        {
                isNearItem = false;
                pickupText.SetActive(false);
            chestopen = null;    
        }
    }

    IEnumerator DisplayTextTemp(float seconds)
    {
        message.text = "Not enought coin, need 5";
        message.enabled = true;

        yield return new WaitForSeconds(seconds);

        message.enabled = false;
    }


}
