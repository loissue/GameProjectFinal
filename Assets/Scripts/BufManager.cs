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
    private void Start()
    {
        HideAllImages();
        ApplyEyeBuff();
    }

    private void HideAllImages()
    {
        eyeImage.gameObject.SetActive(false);
        speedImage.gameObject.SetActive(false);
        jumpImage.gameObject.SetActive(false);
        shieldImage.gameObject.SetActive(false);
    }

    public void ApplySpeedBuff(GameObject player)
    {
        StartCoroutine(ApplySpeed(player));
    }

    public void ApplyJumpBuff(GameObject player)
    {
        StartCoroutine(ApplyJump(player));
    }

    public void ApplyEyeBuff()
    {
        StartCoroutine(ApplyEye());
    }

    private IEnumerator ApplySpeed(GameObject player)
    {
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        float originalSpeed = movement.movespeed;
        movement.movespeed = moveBuff;
        speedImage.gameObject.SetActive(true); 

        float startTime = Time.time;

        while (Time.time - startTime < timeMove)
        {
            float elapsedTime = Time.time - startTime;
            float fillAmount = 1 - (elapsedTime / timeMove);
            speedImage.fillAmount = fillAmount;

            yield return null;
        }

        movement.movespeed = originalSpeed;
        speedImage.gameObject.SetActive(false); 
    }

    private IEnumerator ApplyJump(GameObject player)
    {
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        float originalJumpForce = movement.jumpForce;
        movement.jumpForce = jumpBuff;
        jumpImage.gameObject.SetActive(true);

        float startTime = Time.time;

        while (Time.time - startTime < timeJump)
        {
            float elapsedTime = Time.time - startTime;
            float fillAmount = 1 - (elapsedTime / timeJump);
            jumpImage.fillAmount = fillAmount;

            yield return null;
        }

        movement.jumpForce = originalJumpForce;
        jumpImage.gameObject.SetActive(false); 
    }

    private IEnumerator ApplyEye()
    {
        float originalSize = virtualCamera.m_Lens.OrthographicSize;
        virtualCamera.m_Lens.OrthographicSize = orthographicSize;

        Debug.Log("Changed virtual camera orthographic size to: " + orthographicSize);
        eyeImage.gameObject.SetActive(true); 

        float startTime = Time.time;

        while (Time.time - startTime < timeEye)
        {
            float elapsedTime = Time.time - startTime;
            float fillAmount = 1 - (elapsedTime / timeEye);
            eyeImage.fillAmount = fillAmount;

            yield return null;
        }

        Debug.Log("Restored virtual camera orthographic size to: " + originalSize);
        virtualCamera.m_Lens.OrthographicSize = originalSize;
        eyeImage.gameObject.SetActive(false); 
    }

    public void ApplyShieldBuff()
    {
        StartCoroutine(ApplyShield());
    }
    private IEnumerator ApplyShield()
    {
        shieldImage.gameObject.SetActive(true); 
        yield return new WaitForSeconds(timeShield);
        shieldImage.gameObject.SetActive(false); 
    }
}
