using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawn : MonoBehaviour
{
    public float removegroundchance = 5f;
    public float spawnHeightOffset = 1f; // Chiều cao trên mặt đất để spawn kẻ thù

    [System.Serializable]
    public class SpawnableObject
    {
        public GameObject objectToSpawn;
        [Range(0, 100)] public float spawnChance; // Tỷ lệ phần trăm để spawn đối tượng này
    }

    public SpawnableObject[] spawnableObjects;

    void Start()
    {
        // Có thể thêm code khởi tạo ở đây nếu cần thiết
    }

    public void spawngameobject()
    {
        // Tính tổng tỷ lệ spawn để chọn đối tượng
        float totalChance = 0f;
        foreach (SpawnableObject spawnable in spawnableObjects)
        {
            totalChance += spawnable.spawnChance;
        }

        // Sinh ra một giá trị ngẫu nhiên trong khoảng từ 0 đến tổng tỷ lệ
        float randomValue = Random.Range(0f, totalChance);

        // Tìm đối tượng để spawn dựa trên giá trị ngẫu nhiên
        float cumulativeChance = 0f;
        foreach (SpawnableObject spawnable in spawnableObjects)
        {
            cumulativeChance += spawnable.spawnChance;
            if (randomValue <= cumulativeChance)
            {
                Vector3 spawnPosition = transform.position + Vector3.up * spawnHeightOffset;
                Instantiate(spawnable.objectToSpawn, spawnPosition, Quaternion.identity);
                break;
            }
        }

        // Vô hiệu hóa script sau khi spawn
        this.enabled = false;
    }
}
