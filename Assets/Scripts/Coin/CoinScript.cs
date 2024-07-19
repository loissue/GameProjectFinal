using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    // Start is called before the first frame update
    int coin;
    void Start()
    {
        coin = Random.Range(0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Add score using ScoringManagement singleton instance (if it exists)
            if (ScoringManagement.instance != null)
            {
                ScoringManagement.instance.AddScore(coin * (ChoseBiome.Instance.level + 1));
            }
            Destroy(gameObject);
        }
    }
}
