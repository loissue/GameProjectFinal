using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevel : MonoBehaviour
{
    public KeyCode replaceKey = KeyCode.R;

    void Update()
    {
        if (Input.GetKeyDown(replaceKey))
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
