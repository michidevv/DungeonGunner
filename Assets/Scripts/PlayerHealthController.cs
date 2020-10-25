using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [SerializeField]
    int currentHealth;

    [SerializeField]
    int maxHealth;

    [SerializeField]
    float invulnerabilityTimeout = 1f;

    private bool isInvulnerable = false;
    private WaitForSeconds invulnerabilityDelay;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        invulnerabilityDelay = new WaitForSeconds(invulnerabilityTimeout);

        currentHealth = maxHealth;

        UIController.instance.UpdateHealthUI(currentHealth, maxHealth);
    }

    public void DamagePlayer()
    {
        if (isInvulnerable) return;

        AudioManager.instance.PlaySfx(Sfx.PlayerHurt);

        currentHealth -= 1;

        UIController.instance.UpdateHealthUI(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            AudioManager.instance.PlaySfx(Sfx.PlayerDeath);

            PlayerController.instance.gameObject.SetActive(false);
            UIController.instance.gameOverScreen.SetActive(true);
            AudioManager.instance.PlayGameOver();
        } 
        else
        {
            PlayerController.instance.ChangeBodyOpacity(0.4f);
            StartCoroutine(MakeInvulnerable(invulnerabilityDelay));
        }
    }

    private IEnumerator MakeInvulnerable(WaitForSeconds delay)
    {
        isInvulnerable = true;
        yield return delay;
        isInvulnerable = false;
        PlayerController.instance.ChangeBodyOpacity(1f);
    }

    public void SetInvulnerability(float timeout)
    {
        StartCoroutine(MakeInvulnerable(new WaitForSeconds(timeout)));
    }

    public bool HealPlayer(int amount)
    {
        if (currentHealth == maxHealth) return false;

        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);

        UIController.instance.UpdateHealthUI(currentHealth, maxHealth);

        return true;
    }
}
