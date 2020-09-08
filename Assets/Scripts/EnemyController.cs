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

    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < visibilityRange) 
        {
            moveDirection = PlayerController.instance.transform.position - transform.position;
            moveDirection.Normalize();
            rigidBody.velocity = moveDirection * moveSpeed;

            animator.SetBool("isWalking", true);
        } else
        {
            moveDirection = Vector3.zero;

            animator.SetBool("isWalking", false);
        }
    }
}
