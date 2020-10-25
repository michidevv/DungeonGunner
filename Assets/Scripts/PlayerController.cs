using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DashState
{
    Inactive,
    Active,
    Timeout
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    float dashSpeed = 8f, dashTime = 0.5f, dashTimeout = 1f;

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

    [SerializeField]
    public SpriteRenderer bodyRenderer;
    [SerializeField]
    bool canMove = true;

    [HideInInspector]
    public DashState DashState { get; private set; } = DashState.Inactive;

    private Vector2 moveInput;
    private float shotCounter;

    private WaitForSeconds dashTimer, dashWaitTimer;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        dashTimer = new WaitForSeconds(dashTime);
        dashWaitTimer = new WaitForSeconds(dashTimeout);
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove || LevelManager.instance.IsPaused)
        {
            return;
        }

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        bool isWalking = moveInput != Vector2.zero;
        anim.SetBool("isWalking", isWalking);

        float speed = DashState == DashState.Active ? dashSpeed : moveSpeed;
        rigidBody.velocity = moveInput * speed;

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
            AudioManager.instance.PlaySfx(Sfx.Shoot1);
            Instantiate(bulletToFire, crosshair.position, crosshair.rotation);
            shotCounter = shotTimeout;
        }

        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                AudioManager.instance.PlaySfx(Sfx.Shoot1);
                Instantiate(bulletToFire, crosshair.position, crosshair.rotation);
                shotCounter = shotTimeout;
            }
        }

        if (isWalking && Input.GetKeyDown("space") && DashState == DashState.Inactive)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        AudioManager.instance.PlaySfx(Sfx.PlayerDash);

        DashState = DashState.Active;
        anim.SetTrigger("dash");
        UIController.instance.SetDashOpacity(0.3f);

        // TODO: Make as an event to prevent circular dependency issue.
        PlayerHealthController.instance.SetInvulnerability(dashTime);

        yield return dashTimer;
        StartCoroutine(CooldownDash());
    }

    private IEnumerator CooldownDash()
    {
        DashState = DashState.Timeout;
        yield return dashWaitTimer;
        DashState = DashState.Inactive;

        UIController.instance.SetDashOpacity(1f);
    }

    public void ChangeBodyOpacity(float opacity)
    {
        Color c = bodyRenderer.color;
        bodyRenderer.color = new Color(c.r, c.g, c.b, opacity);
    }

    public void StopMovement()
    {
        canMove = false;

        rigidBody.velocity = Vector2.zero;
        anim.SetBool("isWalking", false);
    }
}
