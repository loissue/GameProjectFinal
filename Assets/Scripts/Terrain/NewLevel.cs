using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevel : MonoBehaviour
{
    public static NewLevel instant;

    void Awake()
    {
        if (instant == null)
        {
            instant = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
    }
    public void nextLevel()
    {
        if (ChoseBiome.Instance.level < 2)
        {
            Debug.LogWarning("ChoseBiome.Instance.level");
            ChoseBiome.Instance.level++;
        }
        else
        {
            ChoseBiome.Instance.level = 0;
        }
        ChoseBiome.Instance.InitializeChosenList();
        ReplaceAllEntities();
    }
    void ReplaceAllEntities()
    {
        ReplaceNewChunk[] replaceChunks = FindObjectsOfType<ReplaceNewChunk>();

        foreach (ReplaceNewChunk replaceChunk in replaceChunks)
        {
            replaceChunk.ReplaceEntity();
        }
    }
}
