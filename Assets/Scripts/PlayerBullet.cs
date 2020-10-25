using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    float speed = 7.5f;

    [SerializeField]
    Rigidbody2D rigidBody;

    [SerializeField]
    GameObject impactEffect;

    [SerializeField]
    int damage = 50;

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);

        AudioManager.instance.PlaySfx(Sfx.Impact);

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>()?.DamageEnemy(damage);
        }

        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
