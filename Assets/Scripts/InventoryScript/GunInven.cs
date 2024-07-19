using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunInven : MonoBehaviour
{
    public List<GameObject> thisbulletlist;
    public Magazin Thisgunmagazin;
    public GridLayoutGroup GunGrid; // Sử dụng GridLayoutGroup để dễ dàng quản lý các ô
    public GameObject bulletSlotPrefab; // Prefab cho ô đạn

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        if (GunGrid != null)
        {
            bool isActive = GunGrid.gameObject.activeSelf;
            GunGrid.gameObject.SetActive(!isActive);

            if (GunGrid.gameObject.activeSelf)
            {
                // UpdateInventoryUI(); // Bạn có thể cập nhật lại UI nếu cần thiết
            }
        }
        else
        {
            Debug.LogWarning("GunGrid is not assigned in the Inspector.");
        }
    }

    public void GetBullet(List<GameObject> bullets)
    {
        thisbulletlist = bullets;
        GetFirstGun(thisbulletlist);
    }

    public void GetFirstGun(List<GameObject> bulletlist)
    {
        thisbulletlist = bulletlist;
        foreach (Transform child in GunGrid.transform)
        {
            Destroy(child.gameObject);
        }
        
        // Xoá tất cả các con hiện tại trong GunGrid
        

        // Thêm các GameObject trong bulletlist vào Grid
        foreach (var bullet in bulletlist)
        {
            if (bullet != null)
            {
                GameObject slot = Instantiate(bulletSlotPrefab, GunGrid.transform);
                slot.GetComponent<Image>().sprite = bullet.GetComponent<SpriteRenderer>().sprite;
                slot.SetActive(true); // Đảm bảo GameObject slot được kích hoạt

                Image icon = slot.GetComponent<Image>(); // Giả sử InventorySlot có một Image component
                icon.sprite = bullet.GetComponent<SpriteRenderer>().sprite; // Lấy sprite từ GameObject
                icon.gameObject.SetActive(true); // Đảm bảo Image component được kích hoạt
            }
        }
    }
}
