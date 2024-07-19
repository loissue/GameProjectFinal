using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BufManager : MonoBehaviour
{
    [Header("Buff Eye")]
    public Image eyeImage;
    public float orthographicSize = 5f;
    public float timeEye = 5f;
    public CinemachineVirtualCamera virtualCamera;
    private float originalSize;

    [Header("Buff Speed")]
    public float moveBuff = 10f;
    public float timeMove = 5f;
    public Image speedImage;

    [Header("Buff Jump")]
    public float jumpBuff = 10f;
    public float timeJump = 5f;
    public Image jumpImage;
    [Header("Buff Shield")]
    public Image shieldImage;
    public float timeShield = 10f;
    public Image shield;
    [Header("Buff Gravity Revert")]
    public float timeGravity = 10f;
    public Image gravityImage;
    [Header("Buff Teleport")]
    public float timeTeleport = 10f;
    public Image teleportImage;
    [Header("Buff TimeFreeze")]
    public float timeFreeze = 10f;
    public Image freezeImage;
    public AudioManager audioManager;

    [Header("Buff Health")]
    public float healthBuff = 100f;
    private void Start()
    {
        originalSize = virtualCamera.m_Lens.OrthographicSize;
        HideAllImages();
    }

    private void HideAllImages()
    {
        eyeImage.gameObject.SetActive(false);
        speedImage.gameObject.SetActive(false);
        jumpImage.gameObject.SetActive(false);
        gravityImage.gameObject.SetActive(false);
        teleportImage.gameObject.SetActive(false);
        freezeImage.gameObject.SetActive(false);
        shieldImage.gameObject.SetActive(false);
        shield.gameObject.SetActive(false);
    }

    public void ApplyGravityBuff(GameObject player)
    {
        gravityImage.gameObject.SetActive(true);
        PlayerMovement playerMove = player.GetComponent<PlayerMovement>();
        if (playerMove != null)
        {
            playerMove.isRevert = true;
            StartCoroutine(ApplyBuff(timeGravity, gravityImage, () =>
            {
                playerMove.isRevert = false;
                player.GetComponent<Rigidbody2D>().gravityScale = Mathf.Abs(player.GetComponent<Rigidbody2D>().gravityScale)/2; 
            }));
        }
    }

    public void ApplySpeedBuff(GameObject player)
    {
        speedImage.gameObject.SetActive(true);
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        float originalSpeed = movement.movespeed;
        movement.movespeed = moveBuff;
        StartCoroutine(ApplyBuff(timeMove, speedImage, () => movement.movespeed = originalSpeed));
    }

    public void ApplyJumpBuff(GameObject player)
    {
        jumpImage.gameObject.SetActive(true);

        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        float originalJumpForce = movement.jumpForce;
        movement.jumpForce = jumpBuff;
        StartCoroutine(ApplyBuff(timeJump, jumpImage, () => movement.jumpForce = originalJumpForce));
    }

    public void ApplyEyeBuff(GameObject player)
    {
        eyeImage.gameObject.SetActive(true);
        player.GetComponent<PlayerMovement>().isEye = true; 
        player.GetComponent<PlayerMovement>().orthographic = orthographicSize;
        StartCoroutine(ApplyBuff(timeEye, eyeImage,  () => { player.GetComponent<PlayerMovement>().isEye = false; virtualCamera.m_Lens.OrthographicSize = originalSize; }));
    }
    public void ApplyHealthBuff(GameObject player)
    {
        player.GetComponent<Health>().currentHealth += healthBuff;
        audioManager.PlaySfx(audioManager.healthClip);
    }

    public void ApplyShieldBuff(GameObject player)
    {
        player.GetComponent<Health>().isShield = true;
        shield.gameObject.SetActive(true) ; 
        StartCoroutine(ApplyBuff(timeShield, shieldImage, () =>
        {
            player.GetComponent<Health>().isShield = false;
            shield.gameObject.SetActive(false);
        }));
    }

    private IEnumerator ApplyBuff(float time, Image image, System.Action resetAction)
    {
        float startTime = Time.time;
        while (Time.time - startTime < time)
        {
            float elapsedTime = Time.time - startTime;
            float fillAmount = 1 - (elapsedTime / time);
            image.fillAmount = fillAmount;
            yield return null;
        }
        image.gameObject.SetActive(false);
        resetAction?.Invoke(); 
    }

    public void ApplyTeleportBuff(GameObject player)
    {
        teleportImage.gameObject.SetActive(true);
        PlayerMovement playerMove = player.GetComponent<PlayerMovement>();
            playerMove.isTeleport = true;
            StartCoroutine(ApplyBuff(timeTeleport, teleportImage, () =>
            {
                playerMove.isTeleport = false;
            }));
    }
    public void ApplyFreezeBuff(GameObject player)
    {
        freezeImage.gameObject.SetActive(true);
        player.GetComponent<PlayerMovement>().isFreeze = true;
        StartCoroutine(ApplyBuff(timeFreeze, freezeImage, () => { player.GetComponent<PlayerMovement>().isFreeze = false;audioManager.SetBackgroundSpeed(1f); }  ));
        
    }
}
