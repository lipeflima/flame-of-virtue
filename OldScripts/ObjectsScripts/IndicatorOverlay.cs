using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace JLGames
{
    public class IndicatorOverlay : MonoBehaviour, IObjectPool
    {
        public Transform textTransform;
        public TMP_Text indicatorText;
        public Vector3 offSet;
        public Vector3 randomizeIntensity = new Vector3(0.5f, 0.0f, 0.0f);
        public float moveSpeed = 2f; // Velocidade de movimento do texto
        public float lifeTime = 3f;  // Tempo de vida do texto antes de desativar
        private float elapsedTime = 0f;
        private float counter;
        private float currentHealth;
        private enum IndicatorType { Damage, Default } 
        [SerializeField] private IndicatorType indicatorType;
        [SerializeField] private float frequency = 2f; // Frequência das curvas
        [SerializeField] private float amplitude = 0.5f; // Amplitude das curvas
        [SerializeField] private float stopYThreshold = 4f;
        [SerializeField] private float randomness = 0.2f;
        private float randomOffset; // Aleatoriedade aplicada ao movimento horizontal
        private bool stopMovement = false;


        void OnEnable()
        {
            InitializeRandomMovement();
            ResetIndicator();
        }

        void Update()
        {
            // Movimenta o texto 
            if (indicatorType == IndicatorType.Damage) 
            {
                ApplyCurvedMovement(textTransform);
            } else {
                textTransform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            }

            // Conta o tempo ate atingir o limite de vida
            counter += Time.deltaTime;
            if (counter >= lifeTime)
            {
                gameObject.SetActive(false); // Desativa o objeto apos o tempo de vida
            }
        }

        public void OnObjectSpawn()
        {
            // Posiciona o texto com offset e randomizacao
            textTransform.localPosition += offSet;
            textTransform.localPosition += new Vector3(
                Random.Range(-randomizeIntensity.x, randomizeIntensity.x),
                Random.Range(-randomizeIntensity.y, randomizeIntensity.y),
                Random.Range(-randomizeIntensity.z, randomizeIntensity.z)
            );
        }

        public void InitializeRandomMovement()
        {
            // Gera um deslocamento aleatório baseado na intensidade da aleatoriedade
            randomOffset = Random.Range(-randomness, randomness);
        }

        void ApplyCurvedMovement(Transform textTransform)
        {
            if (stopMovement) return;

            elapsedTime += Time.deltaTime;
            float verticalMovement = moveSpeed * Time.deltaTime;
            float horizontalMovement = Mathf.Sin((elapsedTime + randomOffset) * frequency * Mathf.PI) * amplitude + randomOffset;
            Vector3 sMovement = new Vector3(horizontalMovement, verticalMovement, 0);
            textTransform.Translate(sMovement);

            if (textTransform.position.y >= stopYThreshold)
            {
                stopMovement = true; 
            }
        }

        public void SetDamageText(int damage)
        {
            currentHealth += damage;
            indicatorText.text = "-" + currentHealth.ToString();
        }

        public void SetBuffText(string buffStats)
        {
            indicatorText.text = buffStats;
        }

        private void ResetIndicator()
        {
            counter = 0;        // Reinicia o contador
            currentHealth = 0;   // Reinicia o valor da vida
        }
    }
}
