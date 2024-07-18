using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private int groundLayer = 3; // Layer index cho Ground

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == groundLayer)
        {
            Debug.Log("spawn");
            MobSpawn mobSpawn = other.GetComponent<MobSpawn>();
            
            // Kiểm tra xem mobSpawn có bị null không
            if (mobSpawn != null)
            {
                
                mobSpawn.spawngameobject();
            }
            else
            {
                Debug.LogError("MobSpawn component not found on the Ground object.");
            }
        }
    }
}
