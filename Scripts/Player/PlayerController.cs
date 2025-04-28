using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRigidbody2D;
    [SerializeField] private float _playerSpeed, angle;
    private Vector2 _playerDirection, mousePos;
    private Camera cam;
    [SerializeField] private GameObject model;
    private EnergySystem energySystem;
    private Animator anim;
    public PlayerStates playerStates;
    public static PlayerController Instance;
    private PlayerWeaponControll weaponControll;
    private string currentAnimState = "";
    public bool isFacingRight = true;
    public bool primaryShoot = false, specialAttackInput = false, interactInput = false, dashInput = false;
    public bool isTeleporting = false;
    public Transform weapon, turret;
    public ComboSystem comboSystem { get; private set; }

    // Knockback e dano temporizado
    private Vector2 externalForce;
    private float externalForceTimer = 0f;
    [SerializeField] private float externalForceDuration = 0.3f;

    private float damagedTimer = 0f;
    [SerializeField] private float damagedDuration = 0.5f;

    // Variáveis para o Dash
    [Header("Dash Config")]
    [SerializeField] private float dashSpeed = 10f; // Velocidade do dash
    [SerializeField] private float dashDuration = 0.2f; // Duração do dash
    [SerializeField] private float dashCooldown = 1f; // Cooldown do dash
    private float dashTimer = 0f;
    private bool isDashing = false;

    public enum PlayerStates
    {
        Idle,
        Moving,
        Damaged,
        Dead,
        Dashing
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
        anim = model.GetComponent<Animator>();
        energySystem = GetComponent<EnergySystem>();
        playerStates = PlayerStates.Idle;
        weaponControll = GetComponent<PlayerWeaponControll>();
        cam = Camera.main;
        comboSystem = GetComponent<ComboSystem>();
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        UpdateTimers();
        UpdatePlayerState();
        UpdateAnimation();

        if (CanMove() && !isTeleporting && !isDashing)
        {
            SetMovement();
        }

        if (externalForceTimer > 0f)
        {
            ApplyForce();
        }

        if (isDashing)
        {
            DashMovement();
        }
    }

    private void HandleInput()
    {
        _playerDirection.x = Input.GetAxisRaw("Horizontal");
        _playerDirection.y = Input.GetAxisRaw("Vertical");
        primaryShoot = Input.GetButton("Fire1");
        specialAttackInput = Input.GetButton("Fire2");
        interactInput = Input.GetKeyDown(KeyCode.E);
        dashInput = Input.GetButtonDown("Jump"); // Alterar de acordo com o seu input
    }

    private void UpdatePlayerState()
    {
        if (!energySystem.GetLifeStatus())
        {
            playerStates = PlayerStates.Dead;
        }
        else if (damagedTimer > 0f)
        {
            playerStates = PlayerStates.Damaged;
        }
        else if (isDashing)
        {
            playerStates = PlayerStates.Dashing;
        }
        else if (_playerDirection != Vector2.zero)
        {
            playerStates = PlayerStates.Moving;
        }
        else
        {
            playerStates = PlayerStates.Idle;
        }

        if (energySystem.GetLifeStatus())
        {
            if (primaryShoot)
            {
                weaponControll.Shoot();
            }
            else
            {
                weaponControll.StopShoot();
            }

            if (specialAttackInput && comboSystem.GetSpecialStatus())
            {
                weaponControll.ShootSpecial();
            }

            if(dashInput)
            {
                TryDash();
            }

            //SetRotation();
        }
    }

    private void UpdateAnimation()
    {
        switch (playerStates)
        {
            case PlayerStates.Idle:
                SetAnimationState("Idle");
                break;
            case PlayerStates.Moving:
                SetAnimationState("Moving");
                break;
            case PlayerStates.Damaged:
                SetAnimationState("Damaged");
                break;
            case PlayerStates.Dead:
                SetAnimationState("Dead");
                break;
            case PlayerStates.Dashing:
                SetAnimationState("Dashing");
                break;
        }
    }

    private void SetAnimationState(string newState)
    {
        if (currentAnimState == newState) return;

        if (!string.IsNullOrEmpty(currentAnimState))
            anim.SetBool(currentAnimState, false);

        anim.SetBool(newState, true);
        currentAnimState = newState;
    }

    private void SetMovement()
    {
        // Vira o personagem conforme a direção
        if (_playerDirection.x > 0 && !isFacingRight)
            Flip();
        else if (_playerDirection.x < 0 && isFacingRight)
            Flip();

        _playerRigidbody2D.MovePosition(_playerRigidbody2D.position + _playerSpeed * Time.fixedDeltaTime * _playerDirection.normalized);
    }

    public void SetRotation()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDir = mousePos - _playerRigidbody2D.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        turret.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (mousePos.x < transform.position.x && isFacingRight)
        {
            Flip();
        }

        if (mousePos.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = model.transform.localScale;
        scale.x *= -1;
        model.transform.localScale = scale;
    }

    private bool CanMove()
    {
        return playerStates == PlayerStates.Idle || playerStates == PlayerStates.Moving;
    }

    public void ApplyExternalForce(Vector2 force)
    {
        externalForce = force;
        externalForceTimer = externalForceDuration;
        damagedTimer = damagedDuration;
        playerStates = PlayerStates.Damaged;
    }

    private void ApplyForce()
    {
        _playerRigidbody2D.MovePosition(_playerRigidbody2D.position + externalForce * Time.fixedDeltaTime);
        externalForce = Vector2.Lerp(externalForce, Vector2.zero, 5f * Time.fixedDeltaTime);
    }

    private void UpdateTimers()
    {
        if (damagedTimer > 0f)
            damagedTimer -= Time.fixedDeltaTime;

        if (externalForceTimer > 0f)
            externalForceTimer -= Time.fixedDeltaTime;

        if (dashTimer > 0f)
            dashTimer -= Time.fixedDeltaTime;
        else if (isDashing)
        {
            isDashing = false;
        }
    }

    // Lógica do Dash
    private void DashMovement()
    {
        // Realiza o dash na direção em que o player está se movendo
        Vector2 dashDirection = _playerDirection.normalized;
        _playerRigidbody2D.velocity = dashDirection * dashSpeed;

        // Finaliza o dash após a duração
        if (dashTimer <= 0f)
        {
            isDashing = false;
        }
    }

    // Método que é chamado quando o jogador pressiona o botão de dash
    public void TryDash()
    {
        if (dashTimer <= 0f)
        {
            isDashing = true;
            dashTimer = dashDuration;
        }
    }
}
