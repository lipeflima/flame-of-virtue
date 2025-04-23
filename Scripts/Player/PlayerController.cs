using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRigidbody2D;
    [SerializeField] private float _playerSpeed;
    private Vector2 _playerDirection;
    [SerializeField] private GameObject model;
    private EnergySystem energySystem;
    private Animator anim;
    public PlayerStates playerStates;
    public static PlayerController Instance;
    private PlayerWeaponControll weaponControll;
    private string currentAnimState = "";
    public bool isFacingRight = true;
    public bool primaryShoot = false;
    public Transform weapon, turret;

    // Knockback e dano temporizado
    private Vector2 externalForce;
    private float externalForceTimer = 0f;
    [SerializeField] private float externalForceDuration = 0.3f;

    private float damagedTimer = 0f;
    [SerializeField] private float damagedDuration = 0.5f;

    public enum PlayerStates
    {
        Idle,
        Moving,
        Damaged,
        Dead
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

        if (CanMove())
        {
            SetMovement();
        }

        if (externalForceTimer > 0f)
        {
            ApplyForce();
        }
    }

    private void HandleInput()
    {
        _playerDirection.x = Input.GetAxisRaw("Horizontal");
        _playerDirection.y = Input.GetAxisRaw("Vertical");
        primaryShoot = Input.GetButton("Fire1");
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
            
        else if (_playerDirection != Vector2.zero)
        {
            playerStates = PlayerStates.Moving;
        }
            
        else
        {
            playerStates = PlayerStates.Idle;
        }

        if(energySystem.GetLifeStatus())
        {
            if(primaryShoot)
            {
                weaponControll.Shoot();
            }
            else
            {
                weaponControll.StopShoot();
            }
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

        if(mousePos.x < transform.position.x && isFacingRight)
        {
            Flip();
        }

        if(mousePos.x > transform.position.x && !isFacingRight)
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
    }
}
