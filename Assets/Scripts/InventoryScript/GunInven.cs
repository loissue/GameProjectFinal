using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GunInven : MonoBehaviour
{

    public GameObject[] thisbulletlist;
    public Magazin Thisgunmagazin;
    public GridLayoutGroup GunGrid; // Sử dụng GridLayoutGroup để dễ dàng quản lý các ô
    //private Transform firstGunSlotContainer; // Container cho ô to hơn chứa firstgun
    public GameObject bulletSlotPrefab; // Prefab cho ô đạn
    public GameObject firstGunSlotPrefab; // Prefab cho ô to hơn chứa firstgun

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void GetBUllet(GameObject[] a)
    {
        //thisbulletlist = a;
        //GetFirstGun(thisbulletlist, bulletSlotPrefab);

    }
    public void GetFirstGun(GameObject[] bulletlist, GameObject firstgun)
    {

        thisbulletlist = bulletlist;
        
        // Xoá tất cả các con hiện tại trong GunGrid
        foreach (Transform child in GunGrid.transform)
        {
            Destroy(child.gameObject);
        }

        // Thêm các GameObject trong a vào Grid
        foreach (var bullet in bulletlist)
        {
            if (bullet != null)
            {

                // Bạn có thể cập nhật thông tin của slot tại đây nếu cần, ví dụ:
                GameObject slot = Instantiate(bulletSlotPrefab, GunGrid.transform);
                slot.GetComponent<Image>().sprite = bullet.GetComponent<SpriteRenderer>().sprite;
                
                slot.SetActive(true); // Đảm bảo GameObject slot được kích hoạt

                Image icon = slot.GetComponent<Image>(); // Giả sử InventorySlot có một Image component

                icon.sprite = bullet.GetComponent<SpriteRenderer>().sprite; // Lấy sprite từ GameObject
                icon.gameObject.SetActive(true); // Đảm bảo Image component được kích hoạt
            }
        }

        //Thêm firstgun vào một ô to hơn bên dưới Grid
        // Xoá tất cả các con hiện tại trong firstGunSlotContainer
        //foreach (Transform child in firstGunSlotContainer)
        //{
        //    Destroy(child.gameObject);
        //}

        //// Tạo một ô to hơn để chứa firstgun
        //GameObject firstGunSlot = Instantiate(firstGunSlotPrefab, firstGunSlotContainer);
        ////Cập nhật thông tin của firstGunSlot nếu cần
        // firstGunSlot.GetComponent<Image>().sprite = firstgun.GetComponent<SpriteRenderer>().sprite;
    }
}
