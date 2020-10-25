using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    [SerializeField]
    GameObject[] brokenPieces;
    [SerializeField]
    int maxNumberOfPieces = 5;
    [SerializeField]
    bool shouldDropItems;
    [SerializeField]
    GameObject[] itemsToDrop;
    [SerializeField]
    float dropChancePercent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet") || 
            (PlayerController.instance.DashState == DashState.Active && other.CompareTag("Player")))
        {
            AudioManager.instance.PlaySfx(Sfx.BoxBreaking);

            Destroy(gameObject);

            MakeBrokenPieces();

            if (shouldDropItems) DropItems();
        }
    }

    private void DropItems()
    {
        float dropChance = Random.Range(0f, 100f);
        if (dropChancePercent >= dropChance)
        {
            int index = Random.Range(0, itemsToDrop.Length);
            Instantiate(itemsToDrop[index], transform.position, transform.rotation);
        }
    }

    private void MakeBrokenPieces()
    {
        int numberOfPieces = Random.Range(1, maxNumberOfPieces);
        for (int i = 0; i < numberOfPieces; i++)
        {
            int index = Random.Range(0, brokenPieces.Length);

            Instantiate(brokenPieces[index], transform.position, transform.rotation);
        }
    }
}
