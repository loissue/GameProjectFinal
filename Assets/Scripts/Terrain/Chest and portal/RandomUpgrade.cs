using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUpgrade : MonoBehaviour
{
    public float spawnHeightOffset = 1f;
    [System.Serializable]
    public struct PrefabWithPercentage
    {
        public GameObject prefab;
        public float percentage;
    }
    public PrefabWithPercentage[] prefabsWithPercentages;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RandomDrop()
    {

        float totalPercentage = 0f;
        foreach (var item in prefabsWithPercentages)
        {
            totalPercentage += item.percentage;
        }

        System.Array.Sort(prefabsWithPercentages, (x, y) => x.percentage.CompareTo(y.percentage));

        float randomValue = Random.Range(0f, totalPercentage);

        float cumulativePercentage = 0f;
        GameObject selectedPrefab = null;
        foreach (var item in prefabsWithPercentages)
        {
            cumulativePercentage += item.percentage;
            if (randomValue <= cumulativePercentage)
            {
                selectedPrefab = item.prefab;
                break;
            }
        }

        if (selectedPrefab != null)
        {
            Vector3 spawnPosition = transform.position + Vector3.up * spawnHeightOffset;
            Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Selected prefab is null.");
        }
    }
}
