using UnityEngine;

public class ChoseBiome : MonoBehaviour
{
    public static ChoseBiome Instance;

    [System.Serializable]
    public struct PrefabWithPercentage
    {
        public GameObject prefab;
        public float percentage;
    }

    public PrefabWithPercentage[] list1;
    public PrefabWithPercentage[] list2;
    public PrefabWithPercentage[] list3;

    public PrefabWithPercentage[] chosenList;
    public Transform terrainParent;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeChosenList();
    }

    void InitializeChosenList()
    {
        if (chosenList == null || chosenList.Length == 0)
        {
            chosenList = ChooseList();
            DebugChosenList();
        }
    }

    PrefabWithPercentage[] ChooseList()
    {
        PrefabWithPercentage[] list = null;

        while (list == null || list.Length == 0)
        {
            int randomListIndex = Random.Range(0, 3);
            switch (randomListIndex)
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
}
