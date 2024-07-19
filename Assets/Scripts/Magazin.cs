using System.Collections.Generic;
using UnityEngine;

public class Magazin : MonoBehaviour
{
    public Shoot shoot;
    public GunInven GunInven;
    public GameObject bulletSlotPrefab; // Prefab cho các slot đạn
    public Transform bulletGrid; // Grid Layout Group trong Panel

    public List<GameObject> bulletPrefabs; // Danh sách các loại đạn có thể spawn ngẫu nhiên
    public int maxBullets = 4; // Số lượng đạn tối đa trong một slot

    public List<GameObject> Bullets = new List<GameObject>(); // Danh sách chứa các bullet

    void Start()
    {
        CalculateMaxBulletsBasedOnDepth();
        GenerateRandomBullets();
        

        
    }

    void CalculateMaxBulletsBasedOnDepth()
    {
        // Giả sử độ sâu tối đa là -100 và độ sâu nhỏ nhất là 0
        // Tính maxBullets tỷ lệ với độ sâu
        float depth = Mathf.Abs(transform.position.y);
        float maxDepth = 100f; // Độ sâu tối đa giả định
        maxBullets = Mathf.Clamp(Mathf.RoundToInt((depth / maxDepth) * 9), 1, 9);
    }

    void GenerateRandomBullets()
    {
        int bulletCount = Random.Range(1, maxBullets + 1);
        Bullets = new List<GameObject>();
        for (int i = 0; i < bulletCount; i++)
        {
            int randomIndex = Random.Range(0, bulletPrefabs.Count);
            GameObject randomBullet = bulletPrefabs[randomIndex];
            Bullets.Add(randomBullet);
        }
    }

    

    public void addbullettomagazin(GameObject bullet)
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

    void Update()
    {
        shoot.getbulletlist(Bullets);
    }
}
