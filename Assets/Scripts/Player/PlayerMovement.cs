using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    private FMOD.Studio.EventInstance footstepSound;
    [HideInInspector]
    public Vector3 playerMovementVector;
    [HideInInspector]
    public float lastHorizontalDeCoupledVector;
    [HideInInspector]
    public float lastVerticalDeCoupledVector;

    [HideInInspector]
    public float lastHorizontalCoupledVector;
    [HideInInspector]
    public float lastVerticalCoupledVector;
    private ItemStats itemStats;

    [SerializeField] float playerMovementSpeed = 3f;
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDuration = 1f;
    [SerializeField] float dashCooldown = 1f;
    public float dashDamage = 10f;
    public int dashCount = 1;

    [SerializeField] Animator animator;
    private Vector3 originalFaceDirection;
    private Vector3 originalHealthbarDirection;
    private int lastFaceDirection = 1;

    private int _currentDashCount;
    private bool isDashing;
    private bool canDash;

    private bool isMoving = false;

    private PlayerHealth playerHealth;
    [SerializeField] Jets jets;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
        playerMovementVector = new Vector3();
    }

    void Start()
    {
        originalFaceDirection = transform.localScale;
        originalHealthbarDirection = playerHealth.hpBar.transform.localScale;
        lastHorizontalDeCoupledVector = -1f;
        lastVerticalDeCoupledVector = 1f;

        lastHorizontalCoupledVector = -1f;
        lastVerticalCoupledVector = 1f;
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }
        if (playerHealth.GetStunStatus())
        {
            rigidbody2D.linearVelocity = Vector2.zero;
            animator.SetBool("IsMoving", false);
            isMoving = false;
            return;
        }
        CorePlayerMovement();
        if (rigidbody2D.linearVelocity.magnitude > 0.1f)
        {
            animator.SetBool("IsMoving", true);
            if (!isMoving)
            {
                isMoving = true;
                FMODUnity.RuntimeManager.PlayOneShot("event:/Command_FootstepSTART");
            }
        }
        else
        {
            animator.SetBool("IsMoving", false);
            if (isMoving)
            {
                isMoving = false;
                FMODUnity.RuntimeManager.PlayOneShot("event:/Command_FootstepEND");
            }
        }

        HandlePlayerDirection();
    }

    void HandlePlayerDirection()
    {
        if (playerMovementVector.x > 0)
        {
            transform.localScale = new Vector3(originalFaceDirection.x, originalFaceDirection.y, originalFaceDirection.z);
            playerHealth.hpBar.transform.localScale = new Vector3(originalHealthbarDirection.x, originalHealthbarDirection.y, originalHealthbarDirection.z);
            lastFaceDirection = 1;
        }
        else if (playerMovementVector.x < 0)
        {
            transform.localScale = new Vector3(-originalFaceDirection.x, originalFaceDirection.y, originalFaceDirection.z);
            playerHealth.hpBar.transform.localScale = new Vector3(-originalHealthbarDirection.x, originalHealthbarDirection.y, originalHealthbarDirection.z);
            lastFaceDirection = -1;
        }
        else
        {
            transform.localScale = new Vector3(originalFaceDirection.x * lastFaceDirection, originalFaceDirection.y, originalFaceDirection.z);
            playerHealth.hpBar.transform.localScale = new Vector3(originalHealthbarDirection.x * lastFaceDirection, originalHealthbarDirection.y, originalHealthbarDirection.z);
        }
    }

    void CorePlayerMovement()
    {
        playerMovementVector.x = Input.GetAxisRaw("Horizontal");
        playerMovementVector.y = Input.GetAxisRaw("Vertical");

        if (playerMovementVector.x != 0 || playerMovementVector.y != 0)
        {
            lastHorizontalCoupledVector = playerMovementVector.x;
            lastVerticalCoupledVector = playerMovementVector.y;
        }

        if (playerMovementVector.x != 0)
        {
            lastHorizontalDeCoupledVector = playerMovementVector.x;
        }

        if (playerMovementVector.y != 0)
        {
            lastVerticalDeCoupledVector = playerMovementVector.y;
        }

        playerMovementVector *= playerMovementSpeed;

        rigidbody2D.linearVelocity = playerMovementVector;
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(PlayerDashAbility());
        }
    }

    private IEnumerator PlayerDashAbility()
    {
        isDashing = true;
        _currentDashCount++;
        playerHealth.SetInvulnerability(true);
        rigidbody2D.linearVelocity = new Vector3(playerMovementVector.x * dashSpeed, playerMovementVector.y * dashSpeed, 0);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player_BOOST");
        jets.ActivateJetParticles();
        yield return new WaitForSeconds(dashDuration);
        jets.DeactivateJetParticles();
        isDashing = false;
        playerHealth.SetInvulnerability(false);
        canDash = _currentDashCount < dashCount;
        yield return new WaitForSeconds(dashCooldown);
        _currentDashCount = 0;
        canDash = dashCount > 0;
    }


    public void UpgradeDash(int upgradedDashCount)
    {
        if (dashCount == 0 && upgradedDashCount > 0)
        {
            canDash = true;
        }
        dashCount += upgradedDashCount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDashing == true && collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(dashDamage);
            }
        }
    }

    private void OnDestroy()
    {
        footstepSound.release();
    }
}