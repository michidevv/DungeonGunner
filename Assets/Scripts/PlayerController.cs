using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    Rigidbody2D rigidBody;

    [SerializeField]
    Transform gunHand;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    Animator anim;

    [SerializeField]
    GameObject bulletToFire;

    [SerializeField]
    Transform crosshair;

    [SerializeField]
    readonly float shotTimeout = 0.2f;

    private Vector2 moveInput;
    private float shotCounter;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        bool isWalking = moveInput != Vector2.zero;
        anim.SetBool("isWalking", isWalking);

        rigidBody.velocity = moveInput * moveSpeed;

        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.localPosition);

        if (mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            gunHand.localScale = new Vector3(-1, -1, 1);
        } else
        {
            transform.localScale = Vector3.one;
            gunHand.localScale = Vector3.one;
        }

        // Rotate gun hand
        Vector2 offset = mousePos - screenPoint;
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        gunHand.rotation = Quaternion.Euler(0, 0, angle);

        // TODO: Rewrite with Coroutines.
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletToFire, crosshair.position, crosshair.rotation);
            shotCounter = shotTimeout;
        }

        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                Instantiate(bulletToFire, crosshair.position, crosshair.rotation);
                shotCounter = shotTimeout;
            }
        }
    }
}
