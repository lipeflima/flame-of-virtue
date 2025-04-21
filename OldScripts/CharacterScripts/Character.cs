using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region Declare Variables
    public StateMachine StateMachine { get; private set; }
    public CharacterInputHandler InputHandler { get; private set; }
    [SerializeField]
    private CharacterData characterData;

    public CharacterController Controller { get; private set; }
    public Collector Collector { get; private set; }
    public CharacterColector characterCollector { get; private set; }
    public SaveDataManager saveDataManager { get; private set; }
    public GameObject Model;
    private Light spotLight;

    public Animator Animator { get; private set; }
    public Rigidbody Rb { get; private set; }
    public GameManager GameManager { get; private set; }
    public Health Health { get; private set; }
    private float nextTimeToCheck, counterAction;
    public float staminaMultiplier {get; set;}
    public bool StartCounterAction { get; set; }
    public bool isGrounded { get; set; }
    public bool inMineZone { get; set; }
    public bool inEncounterZone {get; set;}
    public bool isMinning { get; set; }
    public bool isOverWeighted { get; set; }
    public bool isMounting { get; set; }
    public bool attacking {get; set;}
    public bool encountering {get; set;}
    public float overWeightSpeedMult { get; set; }
    public bool hasToolEquiped {get; set;}
    public static Character Instance;
    public CharacterIdle idleState { get; private set; }
    public CharacterMoving moveState { get; private set; }
    public CharacterMinning mineState { get; private set; }
    public CharacterDeath deathState { get; private set; }
    public CharacterMounting mountState {  get; private set; }
    public CharacterInAir inAirState {get; private set;}
    public CharacterAttack attackState {get; private set;}
    private Camera mainCamera;
    public Vector3 CurrentVelocity{get; set;}
    public GameObject torch;
    #endregion

    #region Main Methods
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Garante que apenas uma inst�ncia do pool exista
            return;
        }

        StateMachine = new StateMachine();
        idleState = new CharacterIdle(this, StateMachine, characterData, "Idle");
        moveState = new CharacterMoving(this, StateMachine, characterData, "Moving");
        mineState = new CharacterMinning(this, StateMachine, characterData, "Minning");
        deathState = new CharacterDeath(this, StateMachine, characterData, "Dead");
        mountState = new CharacterMounting(this, StateMachine, characterData, "Mounting");
        inAirState = new CharacterInAir(this, StateMachine, characterData, "InAir");
        attackState = new CharacterAttack(this, StateMachine, characterData, "Attacking");

        Animator = Model.GetComponent<Animator>();
    }

    void Start()
    {
        InputHandler = GetComponent<CharacterInputHandler>();
        Rb = GetComponent<Rigidbody>();
        GameManager = GameManager.Instance;
        saveDataManager = GetComponent<SaveDataManager>();
        Health = GetComponent<Health>();
        Collector = GetComponent<Collector>();
        characterCollector = GetComponent<CharacterColector>();
        // Verifica se a câmera principal está atribuída
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        StateMachine.Initialize(idleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();

        isGrounded = CheckIfGrounded();
        CheckGravity();
        CheckLightTorch();
        CurrentVelocity = Rb.velocity;
        if(attacking)
        {
            Debug.Log("Call Stop!");
            Invoke("StopAttack", characterData.attackTime);
        }
        //else CheckGravity(characterData.gravity);
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Getters & Setters
    public void SetStop()
    {
        // Define a velocidade do Rigidbody como zero para parar o movimento
        Rb.velocity = Vector2.zero;
    }

    public void SetVelocity(float velocityX, float velocityZ, float mult)
    {
        // Calcula a direção com base na entrada e na câmera
        Vector3 inputDirection = new Vector3(velocityX, 0, velocityZ).normalized;

        // Evita variação de velocidade ao pressionar ambas as direções
        if (inputDirection.magnitude > 1)
        {
            inputDirection = inputDirection.normalized;
        }

        // Gira a direção em relação à câmera
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        // Remove a influência do eixo Y
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Define a direção final do movimento
        Vector3 moveDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;

        // Calcula a velocidade final
        Vector3 velocity = moveDirection * characterData.moveVelocity * mult;

        // Move o Rigidbody suavemente
        Rb.velocity = new Vector3(velocity.x, Rb.velocity.y, velocity.z);

        // Rotaciona o objeto na direção do movimento
        if (moveDirection.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            Rb.rotation = Quaternion.Slerp(Rb.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    public void SetVelocityY(float gravity)
    {
        Rb.velocity = new Vector3(Rb.velocity.x, gravity, Rb.velocity.z);
    }
    public void SetDashVelocity(float dashVelocity, Vector2 dir)
    {
        // Configura a detecção de colisão para modo contínuo
        Rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Obtém a rotação da câmera principal no plano XZ (apenas no eixo Y)
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0; // Mantém a direção no plano horizontal
        cameraForward.Normalize();

        Vector3 cameraRight = mainCamera.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        // Cria a direção ajustada com base na orientação da câmera e nos inputs de dash
        Vector3 movementVector = cameraForward * dir.y + cameraRight * dir.x;
        movementVector.Normalize();

        // Define a velocidade do Rigidbody na direção do dash
        Rb.velocity = movementVector * dashVelocity;
    }
    public void SetOverWeightedAnimation(bool status)
    {
        isOverWeighted = status;
        Animator.SetBool("Overweighted", isOverWeighted);
    }
    public void SetAttack()
    {
        
    }
    public void StopAttack()
    {
        attacking = false;
        StateMachine.ChangeState(idleState);
    }
    public void TakeItem()
    {
        //Collector.SetColectItem();
    }

    public bool GetAliveStatus()
    {
        return Health.GetAliveStatus();
    }

    public CharacterData GetCharacterData()
    {
        return characterData;
    }
    #endregion

    #region Checks

    public bool CheckIfGrounded()
    {
        // Origem do Raycast com um offset
        Vector3 rayOrigin = transform.position + characterData.raycastOffset;

        // Lança o raycast para baixo
        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, characterData.raycastDistance, characterData.groundLayer))
        {
            // Chão detectado
            return true;
        }

        // Nenhum chão detectado
        return false;
    }
    void CheckLightTorch()
    {
        if (GameManager.timer != null)
        {
            bool shouldEnableTorch = !GameManager.timer.isDay && !isOverWeighted && !isMinning;

            Animator.SetBool("TorchOn", shouldEnableTorch);
            torch.SetActive(shouldEnableTorch);
        }
      
    }

    // Gizmos para visualização no Editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 rayOrigin = transform.position + characterData.raycastOffset;
        Gizmos.DrawLine(rayOrigin, rayOrigin + Vector3.down * characterData.raycastDistance);
    }
    public void CheckGravity()
    {
        // Add gravity handling if necessary
        //Rb.velocity += Physics.gravity * characterData.gravity;
        Rb.AddForce(Physics.gravity * characterData.gravity, ForceMode.Acceleration);
    }
    #endregion
}
