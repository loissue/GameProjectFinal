using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawn : MonoBehaviour
{
    public float removegroundchance = 5f;
    public float spawnHeightOffset = 1f; // Height above the ground to spawn enemies

    [System.Serializable]
    public class SpawnableObject
    {
        public GameObject objectToSpawn;
        [Range(0, 100)] public float spawnChance; // Tỷ lệ phần trăm để spawn đối tượng này
    }

    public SpawnableObject[] spawnableObjects;

    void Start()
    {
        
    }
    public void spawngameobject(){
        foreach (SpawnableObject spawnable in spawnableObjects)
        {
            float randomValueEnemy = Random.Range(0f, 100f);
            if (randomValueEnemy <= spawnable.spawnChance)
            {
                Vector3 spawnPosition = transform.position + Vector3.up * spawnHeightOffset;
                Instantiate(spawnable.objectToSpawn, spawnPosition, Quaternion.identity);
            }
        }
    }
    
}