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

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = $"{currentHealth} / {maxHealth}";
    }

    public void DamagePlayer()
    {
        currentHealth -= 1;

        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = $"{currentHealth} / {maxHealth}";

        if (currentHealth <= 0)
        {
            PlayerController.instance.gameObject.SetActive(false);
        }
    }
}
