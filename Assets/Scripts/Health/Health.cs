using System.Collections;
using System.Collections.Generic;
using Boss;
using EnemyS;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] public float startingHealth=400;
    public float currentHealth=0;
    private Animator anim;
    private bool dead=false;
    public Image healthBar;
    public bool isShield;
    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;
    private Coroutine burnCoroutine;
     public AudioManager audioManager;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
    }
    
    public void TakeDamage(float _damage)
    {
        
        if (invulnerable) return;
        if (!isShield)
        {
            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        }

        healthBar.fillAmount = currentHealth/startingHealth;
        audioManager.PlaySfx(audioManager.hurtClip);
        if (currentHealth > 0)
        {
            Debug.Log("Player hurt");
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else if (!dead) 
        {
            dead = true; 
            anim.SetTrigger("die");
            Debug.Log("Player died");

            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            foreach (Behaviour component in components)
                component.enabled = false;

            var dropItem = GetComponent<DropItem>();
            if (dropItem != null)
                dropItem.spawnBuff(gameObject.transform.position);

            StartCoroutine(DisableAfterDelay());
        }
    }
    private IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    public void ApplyBurnEffect(float duration, float damagePerSecond)
    {

        if (burnCoroutine != null)
            StopCoroutine(burnCoroutine);

        burnCoroutine = StartCoroutine(Burn(duration, damagePerSecond));
    }

    private IEnumerator Burn(float duration, float damagePerSecond)
    {
        float elapsed = 0;

        while (elapsed < duration)
        {
            TakeDamage(damagePerSecond * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }
    

}
