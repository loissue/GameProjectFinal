using UnityEngine;

namespace EnemyS
{
    public class DropItem : MonoBehaviour
    {
        public SpawnableObject[] spawnableObjects;

        [System.Serializable]
        public class SpawnableObject
        {
            public GameObject objectToSpawn;
            [Range(0, 100)] public float spawnChance; // Tỷ lệ phần trăm để spawn đối tượng này
        }

        public void spawnBuff(Vector3 spawntitle)
        {
            float randomValue = Random.Range(0f, 100f);

            Debug.Log("Spawning");
            float randomValueEnemy = Random.Range(0f, 100f);
            foreach (SpawnableObject spawnable in spawnableObjects)
            {
                if (randomValueEnemy <= spawnable.spawnChance)
                {
                    Vector3 spawnPosition =
                        new Vector3(spawntitle.x, spawntitle.y, spawntitle.z); // Giảm 1 đơn vị trên trục Z

                    Debug.Log(spawnPosition);
                    Instantiate(spawnable.objectToSpawn, spawnPosition, Quaternion.identity);
                    break;
                }
            }
        }
    }
}