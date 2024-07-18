using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawn1 : MonoBehaviour
{
    public float removegroundchance = 5f;
    public float spawnHeightOffset = 1f; // Chiều cao trên mặt đất để sinh ra kẻ thù

    [System.Serializable]
    public class SpawnableObject
    {
        public GameObject prefabToSpawn; // Prefab cần sinh ra
        [Range(0, 100)] public float spawnChance; // Tỷ lệ phần trăm để sinh ra đối tượng này
    }

    public SpawnableObject[] spawnableObjects;

    void Start()
    {
        foreach (SpawnableObject spawnable in spawnableObjects)
        {
            float randomValue = Random.Range(0f, 100f);
            if (randomValue <= spawnable.spawnChance)
            {
                Vector3 spawnPosition = transform.position + Vector3.up * spawnHeightOffset;
                
                // Sinh ra một prefab mới
                GameObject spawnedObject = Instantiate(spawnable.prefabToSpawn, spawnPosition, Quaternion.identity);

                // Thay đổi thành phần của prefab được sinh ra
                // Ví dụ: Bật hoặc tắt một thành phần
                Shoot shoot = spawnedObject.GetComponent<Shoot>();
                if (shoot != null)
                {
                    shoot.enabled = true; // Thay đổi trạng thái của component
                }
            }
        }
    }
}
