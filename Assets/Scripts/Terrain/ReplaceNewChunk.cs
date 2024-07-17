using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceNewChunk : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReplaceEntity()
    {
        if (prefab != null)
        {
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;

            Destroy(gameObject);

            GameObject newEntity = Instantiate(prefab, position, rotation);
        }
        else
        {
            Debug.LogWarning("Prefab is not set!");
        }
    }
}
