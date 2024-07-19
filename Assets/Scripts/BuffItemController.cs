using Cinemachine;
using UnityEngine;

public class BuffItemController : MonoBehaviour
{
    private BufManager buffManager; 
   private AudioManager audioManager;
    public enum ItemType
    {
        Shield,
        Speed,
        Jump,
        Eye,
        Gravity,
        Teleport,
        Freeze,
        Health
    }
    public ItemType Type;

    private void OnItemPickUp(GameObject player)
    {
        switch (Type)
        {
            case ItemType.Shield:
                buffManager.ApplyShieldBuff(player);
                break;
            case ItemType.Speed:
                buffManager.ApplySpeedBuff(player);
                break;
            case ItemType.Jump:
                buffManager.ApplyJumpBuff(player); 
                break;
            case ItemType.Eye:
                buffManager.ApplyEyeBuff(player);
                break;
            case ItemType.Gravity:
                buffManager.ApplyGravityBuff(player);
                player.GetComponent<Rigidbody2D>().gravityScale *= 2;
                break;
            case ItemType.Teleport:
                buffManager.ApplyTeleportBuff(player);
                break;
            case ItemType.Freeze:
                buffManager.ApplyFreezeBuff(player);
                break;
            case ItemType.Health:
                buffManager.ApplyHealthBuff(player);
                break;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnItemPickUp(collision.gameObject);
            audioManager.PlaySfx(audioManager.itemClip);
        }
    }
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        buffManager = GameObject.FindGameObjectWithTag("BuffManager").GetComponent<BufManager>();
    }
}
