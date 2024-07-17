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
        Freeze
    }
    public ItemType Type;

    private void OnItemPickUp(GameObject player)
    {
        switch (Type)
        {
            case ItemType.Shield:
                buffManager.ApplyShieldBuff();
                break;
            case ItemType.Speed:
                buffManager.ApplySpeedBuff(player);
                audioManager.PlaySfx(audioManager.speedClip);
                break;
            case ItemType.Jump:
                buffManager.ApplyJumpBuff(player); 
                break;
            case ItemType.Eye:
                buffManager.ApplyEyeBuff();
                audioManager.PlaySfx(audioManager.eyeClip);
                break;
            case ItemType.Gravity:
                buffManager.ApplyGravityBuff(player);
                player.GetComponent<Rigidbody2D>().gravityScale *= 2;
                break;
            case ItemType.Teleport:
                buffManager.ApplyTeleportBuff(player);
                break;
            case ItemType.Freeze:
                buffManager.ApplyFreezeBuff();
                break;

        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnItemPickUp(collision.gameObject);
        }
    }
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        buffManager = GameObject.FindGameObjectWithTag("BuffManager").GetComponent<BufManager>();
    }
}
