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

    public void InitializeChosenList()
    {
            chosenList = ChooseList();
            
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

    
}
