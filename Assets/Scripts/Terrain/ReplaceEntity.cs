using UnityEngine;

public class ManualPercentageReplaceEntity : MonoBehaviour
{
    void Start()
    {
        if (ChoseBiome.Instance != null)
        {
            ChoseBiome.PrefabWithPercentage[] prefabsWithPercentages = ChoseBiome.Instance.chosenList;
            if (prefabsWithPercentages != null && prefabsWithPercentages.Length > 0)
            {
                ReplaceWithPercentageBasedPrefab(prefabsWithPercentages);
            }
            else
            {
                Debug.LogError("Chosen list is null or empty.");
            }
        }
        else
        {
            Debug.LogError("ChoseBiome instance not found!");
        }
    }

    void ReplaceWithPercentageBasedPrefab(ChoseBiome.PrefabWithPercentage[] prefabsWithPercentages)
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
            Instantiate(selectedPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Selected prefab is null.");
        }
    }
}
