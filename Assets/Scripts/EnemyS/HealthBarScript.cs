using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    Image healthbar;
    Enemy enemy = new Enemy();
    float healthnow;
    float maxhealth;
    // Start is called before the first frame update
    void Start()
    {
        float maxhealth = enemy.getHealth();
    }

    // Update is called once per frame
    void Update()
    {
        healthnow = enemy.getHealth();
        
    }
}
