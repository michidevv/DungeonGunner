using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField]
    int healAmount = 1;
    [SerializeField]
    float timeoutBeforeUse = 0.5f;

    private bool isUseable = false;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timeoutBeforeUse);
        isUseable = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isUseable && other.CompareTag("Player"))
        {
            if (PlayerHealthController.instance.HealPlayer(healAmount))
            {
                AudioManager.instance.PlaySfx(Sfx.PickupHealth);

                Destroy(gameObject);
            }
        }
    }
}
