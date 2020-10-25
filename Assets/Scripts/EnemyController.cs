using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rigidBody;

    [SerializeField]
    Animator animator;

    [SerializeField]
    float moveSpeed = 3f;

    [SerializeField]
    float visibilityRange = 7f;

    [SerializeField]
    float shootRange = 10f;

    [SerializeField]
    int health = 150;

    [SerializeField]
    GameObject[] deathSplatters;

    [SerializeField]
    GameObject damageEffect;

    [SerializeField]
    bool shouldShoot;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform firePoint;

    [SerializeField]
    float fireRate = 2f;

    [SerializeField]
    SpriteRenderer bodyRenderer;

    private Vector3 moveDirection;
    private WaitForSeconds fireDelay;
    private bool isFiring = false;

    // Start is called before the first frame update
    void Start()
    {
        fireDelay = new WaitForSeconds(fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (bodyRenderer.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < visibilityRange)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
                moveDirection.Normalize();
                rigidBody.velocity = moveDirection * moveSpeed;

                animator.SetBool("isWalking", true);
            }
            else
            {
                moveDirection = Vector3.zero;

                animator.SetBool("isWalking", false);
            }

            StartCoroutine(Shoot());
        }
        else
        {
            rigidBody.velocity = Vector3.zero;
            StopCoroutine(Shoot());
        }

        
    }

    private bool ShoudShoot()
    {
        return shouldShoot && !isFiring && PlayerController.instance.gameObject.activeInHierarchy &&
            Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootRange;
    }

    private IEnumerator Shoot()
    {
        if (!ShoudShoot()) yield break;

        isFiring = true;

        AudioManager.instance.PlaySfx(Sfx.Shoot2);

        Instantiate(bullet, firePoint.position, firePoint.rotation);
        yield return fireDelay;
        isFiring = false;

        StartCoroutine(Shoot());
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;

        AudioManager.instance.PlaySfx(Sfx.EnemyHurt);

        if (health <= 0)
        {
            Destroy(gameObject);
            AudioManager.instance.PlaySfx(Sfx.EnemyDeath);
            int splatterIndex = Random.Range(0, deathSplatters.Length);
            int rotation = Random.Range(0, 4);
            Instantiate(deathSplatters[splatterIndex], transform.position, Quaternion.Euler(0f, 0f, rotation * 90f));
        } else
        {
            Instantiate(damageEffect, transform.position, transform.rotation);
        }
    }
}
