using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Collider))]
public class TriggerManager : MonoBehaviour
{
    public Character character { get; private set; }
    public CharacterColector characterColector { get; private set; }
    public MenuMain menu { get; private set; }

    public bool activated {  get; set; }
    public bool isDisabled { get; set; }
    public bool isActivated { get; set; }
    public bool disabled { get; set; }

    public bool hasPromptActivation, hasCharacterInput, hasCinematics, hasHud;
    public GameObject prompt, cinematics, hud;
    private SphereCollider sphereCollider;
    public TMP_Text infoText;
    public UnityEvent OnActivation;
    [Header("Timing Config")]
    [Tooltip("Cooldown time to prevent repeated activations.")]
    public float cooldownTime = 0.25f;
    private float cooldownCounter;
    

    // Reference to the character's InputHandler (assumed dependency)
    public CharacterInputHandler inputHandler { get; private set; }
    public void OnEnable()
    {
        Enabled();
    }
    private void Start()
    {
        // Ensure the GameObject has a SphereCollider and configure it as a trigger

        if(disabled) return;
        sphereCollider = GetComponent<SphereCollider>();
        if(sphereCollider != null) sphereCollider.isTrigger = true;
        character = Character.Instance;
        characterColector = character.GetComponent<CharacterColector>();
        //menu = MenuMain.Instance;
        Initialize();
        // Attempt to retrieve the InputHandler from the character
        if (hasCharacterInput)
        {            
            if (character != null)
            {
                inputHandler = character.GetComponent<CharacterInputHandler>();
                if (inputHandler == null)
                {
                    Debug.LogError("InputHandler not found on the character. Dialogues will not work properly.");
                }
            }
        }
    }
    private void Update()
    {
        CheckingUpdate();

        if (isActivated)
        {
            // Increment cooldown counter while activated
            cooldownCounter += Time.deltaTime;

            if (cooldownCounter >= cooldownTime)
            {
                // Reset activation and cooldown
                cooldownCounter = 0f;
                isActivated = false;
            }
        }
    }
    public void OnDisable()
    {
        Disabled();
    }
    void OnTriggerEnter(Collider col)
    {
        if (disabled) return;

        if (col.gameObject.CompareTag("Player"))
        {
            if (hasPromptActivation && !isDisabled) PromptActivation(true);
            ManageObjectActivation(true);
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (disabled) return;

        if (col.gameObject.CompareTag("Player"))
        {
            CheckInputPress();
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (disabled) return;

        if (col.gameObject.CompareTag("Player"))
        {
            if (hasPromptActivation) PromptActivation(false);
            ManageObjectActivation(false);
        }
    }
    public void OpenHud(bool status)
    {
        if (hud != null) hud.SetActive(status);
    }
    public virtual void Enabled(){}
    public virtual void ManageObjectActivation(bool status) { }
    public virtual void CheckInputPress() { }
    public virtual void Initialize() { }
    public virtual void CheckingUpdate() { }
    public virtual void SaveStatus() { }
    public virtual void SetInfoHud(string id) { }
    public virtual bool CheckIfIsDisabled() { return false; }
    void PromptActivation(bool status)
    {
        prompt.SetActive(status);
    }
    public virtual void Disabled() { }
}
