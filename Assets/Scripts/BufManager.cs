using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BufManager : MonoBehaviour
{
    [Header("Buff Eye")]
    public CinemachineVirtualCamera virtualCamera;
    public Image eyeImage;
    public float orthographicSize = 5f;
    public float timeEye = 5f;

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
    [Header("Buff Gravity Revert")]
    public float timeGravity = 10f;
    public Image gravityImage;
    [Header("Buff Teleport")]
    public float timeTeleport = 10f;
    public Image teleportImage;
    [Header("Buff TimeFreeze")]
    public float timeFreeze = 10f;
    public Image freezeImage;
    private void Start()
    {
        HideAllImages();
    }

    private void HideAllImages()
    {
        eyeImage.gameObject.SetActive(false);
        speedImage.gameObject.SetActive(false);
        jumpImage.gameObject.SetActive(false);
        shieldImage.gameObject.SetActive(false);
        gravityImage.gameObject.SetActive(false);
        teleportImage.gameObject.SetActive(false);
        freezeImage.gameObject.SetActive(false);
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
        speedImage.gameObject.SetActive(false);
    }

    public void ApplySpeedBuff(GameObject player)
    {
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        float originalSpeed = movement.movespeed;
        movement.movespeed = moveBuff;
        speedImage.gameObject.SetActive(true);
        StartCoroutine(ApplyBuff(timeMove, speedImage, () => movement.movespeed = originalSpeed));
        speedImage.gameObject.SetActive(false);
    }

    public void ApplyJumpBuff(GameObject player)
    {
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        float originalJumpForce = movement.jumpForce;
        movement.jumpForce = jumpBuff;
        jumpImage.gameObject.SetActive(true);
        StartCoroutine(ApplyBuff(timeJump, jumpImage, () => movement.jumpForce = originalJumpForce));
        jumpImage.gameObject.SetActive(false);
    }

    public void ApplyEyeBuff()
    {
        float originalSize = virtualCamera.m_Lens.OrthographicSize;
        virtualCamera.m_Lens.OrthographicSize = orthographicSize;
        eyeImage.gameObject.SetActive(true);
        StartCoroutine(ApplyBuff(timeEye, eyeImage, () => virtualCamera.m_Lens.OrthographicSize = originalSize));
        eyeImage.gameObject.SetActive(false);
    }

    public void ApplyShieldBuff()
    {
        shieldImage.gameObject.SetActive(true);
        StartCoroutine(ApplyBuff(timeShield, shieldImage, null));
        shieldImage.gameObject.SetActive(false);
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
        if (playerMove != null)
        {
            playerMove.isTeleport = true;
            StartCoroutine(ApplyBuff(timeTeleport, teleportImage, () =>
            {
                playerMove.isTeleport = false;
            }));
        }
        teleportImage.gameObject.SetActive(false);
    }
    public void ApplyFreezeBuff()
    {
        freezeImage.gameObject.SetActive(true);
        StartCoroutine(ApplyBuff(timeFreeze, freezeImage, () => Time.timeScale=0.4f));
        freezeImage.gameObject.SetActive(false);
    }
}
