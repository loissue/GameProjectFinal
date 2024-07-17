using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replace : MonoBehaviour
{
    public GameObject prefab;
    void ReplaceEntity()
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
