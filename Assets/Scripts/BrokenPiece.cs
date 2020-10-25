using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPiece : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 3f;
    [SerializeField]
    float deceleration = 5f;
    [SerializeField]
    float lifetime = 3f;
    [SerializeField]
    SpriteRenderer renderer;

    private Vector3 moveDirection;
    private WaitForSeconds lifetimeTimer;
    private bool isFading = false;

    private void Start()
    {
        lifetimeTimer = new WaitForSeconds(lifetime);
        moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
        moveDirection.y = Random.Range(-moveSpeed, moveSpeed);
        StartCoroutine(StartTimer());
    }

    private void Update()
    {
        transform.position += moveDirection * Time.deltaTime;
        moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.deltaTime);
        if (isFading)
        {
            renderer.color = new Color(
                renderer.color.r,
                renderer.color.g,
                renderer.color.b,
                Mathf.MoveTowards(renderer.color.a, 0f, lifetime * Time.deltaTime)
            );
            if (renderer.color.a == 0f) Destroy(gameObject);
        }
    }

    private IEnumerator StartTimer()
    {
        yield return lifetimeTimer;
        isFading = true;
    }
}
