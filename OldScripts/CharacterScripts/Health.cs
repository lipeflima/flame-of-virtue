using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Health Instance;
    public float currentHealth;// { get; private set; }
    public float maxHealth = 100;
    public float healthBottleNormalAmount = 20;
    public float healthBottleRecursiveAmount = 25;
    float counterDmg, temp, tempHealth, nextTimeToAdd;
    bool isAlive, recovering, damaged, startDamageCounter;
    public bool healing;
    private Character character;
    private CharacterData characterData;
    private HealthBar healthBar;
    private GameManager manager;
    public ParticleSystem healEffect;
    public AudioSource painSoundFX;
    public GameObject painHud;
    public bool gameOver{get; private set;}
    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        isAlive = true;
        character = GetComponent<Character>();
        characterData = character.GetCharacterData();
        healthBar = GetComponent<HealthBar>();
        currentHealth = characterData.maxHealth;
        healthBar.UpdateHealthHud(currentHealth, maxHealth, true);
        manager = GameManager.Instance;
    }
    private void Update()
    {
        CheckDeath();
        CheckDamagedStatus();
        //Restore();
    }
    public void SetDamage(float damage)
    {
        if (isAlive)
        {
            if (!recovering)
            {
                currentHealth -= damage;
                damaged = true;
                startDamageCounter = true;

                healthBar.UpdateHealthHud((int)currentHealth, maxHealth, false);
                //Instantiate(character.GetCharacterData().bloodFX, transform.position, Quaternion.identity);

                if(painSoundFX != null) painSoundFX.Play();
                if(painHud != null) painHud.SetActive(true);
            }
        }
    }
    public void AddHealth(string mode, float amount)
    {
        switch(mode)
        {
            case "Normal":

                if(amount + currentHealth <= maxHealth)
                {
                    currentHealth += amount;
                }
                else
                {
                    currentHealth = maxHealth;
                }

                healthBar.UpdateHealthHud((int)currentHealth, maxHealth, true);
                healEffect.Play();

                break;

            case "Recursive":
                
                if (amount + currentHealth <= maxHealth)
                {
                    temp = currentHealth + amount;
                    tempHealth = currentHealth;
                    healing = true;
                    StartCoroutine(AddRecursively());
                }

                break;
        }
    }
    IEnumerator AddRecursively()
    {
        while (tempHealth < temp)
        {
            yield return new WaitForSeconds(0.1f);
            currentHealth += 1;
            tempHealth += 1;
            healEffect.Play();

            healthBar.UpdateHealthHud((int)currentHealth, maxHealth, true);
        }

        yield return 0;
    }
    void Restore()
    {
        if (currentHealth < maxHealth)
        {
            if (Time.time >= nextTimeToAdd)
            {
                nextTimeToAdd = Time.time + 1f / characterData.h_restoreRate;
                currentHealth += characterData.h_restoreAmount;

                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }

                healthBar.UpdateHealthHud((int)currentHealth, maxHealth, true);
            }
        }
    }
    public bool GetAliveStatus() 
    {
        return isAlive;
    }
    public bool GetDamageStatus()
    {
        return damaged;
    }
    void CheckDamagedStatus()
    {
        if (startDamageCounter)
        {
            counterDmg += Time.deltaTime;
            recovering = true;            

            if (counterDmg >= character.GetCharacterData().recoverDmgTime)
            {
                counterDmg = 0;
                startDamageCounter = false;
                damaged = false;
                recovering = false;
            }
        }
    }
    void CheckDeath()
    {
        if (isAlive && currentHealth <= 0)
        {
            isAlive = false;
            currentHealth = 0;
            Invoke("GameOver", 5);
        }
    }
    void GameOver()
    {
        manager.GameOver();
    }
}
