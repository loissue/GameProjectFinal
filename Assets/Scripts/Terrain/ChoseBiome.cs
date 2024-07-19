using UnityEngine;

public class ChoseBiome : MonoBehaviour
{
    public static ChoseBiome Instance;
    [SerializeField]
    public int level;

    [System.Serializable]
    public struct PrefabWithPercentage
    {
        public GameObject prefab;
        public float percentage;
    }

    BackgroundManager backgroundManager;
    public PrefabWithPercentage[] list1;
    public PrefabWithPercentage[] list2;
    public PrefabWithPercentage[] list3;

    public PrefabWithPercentage[] chosenList;
    public Transform terrainParent;

    public GameObject portalPrefab; 
    public float minX;              
    public float maxX;              
    public float fixedY = -197;     
    public float fixedZ = 0;
    void Awake()
    {
        Debug.Log("Awake called. Current Instance is: " + (Instance == null ? "null" : "not null"));

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Instance set to this and marked as DontDestroyOnLoad");
        }
        else if (Instance != this)
        {
            Debug.Log("Another instance detected, destroying this GameObject");
            Destroy(gameObject);
            return;
        }

        InitializeChosenList();
        PositionPortal();
    }

    public void InitializeChosenList()
    {
            chosenList = ChooseList();
            DebugChosenList();
    }

    PrefabWithPercentage[] ChooseList()
    {
        PrefabWithPercentage[] list = null;

        while (list == null || list.Length == 0)
        {
            switch (level)
            {
                case 0:
                    list = list1;
                    break;
                case 1:
                    list = list2;
                    break;
                case 2:
                    list = list3;
                    break;
            }
        }

        return list;
    }

    void DebugChosenList()
    {
        if (chosenList != null && chosenList.Length > 0)
        {
            Debug.Log("Chosen list initialized with " + chosenList.Length + " items.");
            foreach (var item in chosenList)
            {
                Debug.Log("Prefab: " + item.prefab.name + ", Percentage: " + item.percentage);
            }
        }
        else
        {
            Debug.LogError("Chosen list is null or empty.");
        }
    }

    void PositionPortal()
    {
        if (portalPrefab != null)
        {
            float randomX = Random.Range(minX, maxX);
            Vector3 newPosition = new Vector3(randomX, fixedY, fixedZ);

            Instantiate(portalPrefab, newPosition, Quaternion.identity);
            Debug.Log("Portal positioned at: " + newPosition);
        }
        else
        {
            Debug.LogError("Portal prefab is not assigned.");
        }
    }
}
