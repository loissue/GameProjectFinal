using Cinemachine;
using UnityEngine;

public class BuffItemController : MonoBehaviour
{
    public BufManager bufManager; 
   public AudioManager audioManager;
    public enum ItemType
    {
        Shield,
        Speed,
        Jump,
        Eye,
        Gravity
    }
    public ItemType Type;

    private void OnItemPickUp(GameObject player)
    {
        switch (Type)
        {
            case ItemType.Shield:
                bufManager.ApplyShieldBuff();
                break;
            case ItemType.Speed:
                bufManager.ApplySpeedBuff(player); 
                break;
            case ItemType.Jump:
                bufManager.ApplyJumpBuff(player); 
                break;
            case ItemType.Eye:
                bufManager.ApplyEyeBuff(); 
                break;
            case ItemType.Gravity:
                bufManager.ApplyGravityBuff(player);
                break;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager.PlaySfx(audioManager.itemClip);
            OnItemPickUp(collision.gameObject);
        }
    }
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
}
