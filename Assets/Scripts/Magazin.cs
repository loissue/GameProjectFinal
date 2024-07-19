using System.Collections.Generic;
using UnityEngine;

public class Magazin : MonoBehaviour
{
    public Shoot shoot;
    public GunInven GunInven;
    public GameObject bulletSlotPrefab; // Prefab cho các slot đạn
    public Transform bulletGrid; // Grid Layout Group trong Panel

    public List<GameObject> Bullets = new List<GameObject>(); // Thay đổi từ mảng sang danh sách

    void Start()
    {
        // Khởi tạo danh sách Bullets nếu cần
        CreateBulletSlots();
    }

    public void addbullettomagazin(GameObject bullet)
    {
        if (Bullets.Count < 4) // Giới hạn số lượng đạn trong magazin là 4
        {
            Bullets.Add(bullet);
        }
        else
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                if (Bullets[i] == null)
                {
                    Bullets[i] = bullet;
                    break;
                }
            }
        }
    }

    void Update()
    {
        shoot.getbulletlist(Bullets.ToArray());
    }

    public void UpdateBullets()
    {
        Bullets.Clear();
        foreach (Transform slot in bulletGrid)
        {
            DraggableItem draggableItem = slot.GetComponentInChildren<DraggableItem>();
            if (draggableItem != null)
            {
                Bullets.Add(draggableItem.gameObject); // Lấy chính GameObject
            }
        }
    }

    private void CreateBulletSlots()
    {
        foreach (GameObject bullet in Bullets)
        {
            GameObject slot = Instantiate(bulletSlotPrefab, bulletGrid); // Tạo đối tượng và gán cha
            slot.SetActive(true);

            if (slot.GetComponent<CanvasGroup>() == null)
                slot.AddComponent<CanvasGroup>();

            if (slot.GetComponent<DropZone>() == null)
                slot.AddComponent<DropZone>().dropZoneType = DropZone.DropZoneType.Weapon;

            DraggableItem draggableItem = slot.GetComponent<DraggableItem>();
            if (draggableItem == null)
                draggableItem = slot.AddComponent<DraggableItem>();

            draggableItem.Magazin = this;

            // Đặt bullet vào slot
            GameObject bulletInstance = Instantiate(bullet);
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
