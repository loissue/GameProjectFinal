using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffItemController : MonoBehaviour
{
    // Start is called before the first frame update
   // private AudioManager audioManager;
    public enum ItemType
    {
        Shield,
        Speed,
        Jump
    }
    public ItemType Type;
    private void Awake()
    {
    //    audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
   
    private void OnItemPickUp(GameObject player)
    {
        switch (Type)
        {
            case ItemType.Shield:
                break;
            case ItemType.Speed:
                player.GetComponent<PlayerMovement>().movespeed = 30f;
                break;
            case ItemType.Jump:
                player.GetComponent<PlayerMovement>().jumpForce = 30f;
                break;



        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //audioManager.PlaySfx(audioManager.itemClip);
            OnItemPickUp(collision.gameObject);

        }
    }
}
