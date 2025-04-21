using UnityEngine;

public class PlayerInicialization : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPoint
    {
        public int originLevel;
        public Transform spawnPosition;
        public bool isPlayerCurrentPosition;        
    }

    public SpawnPoint[] spawnPoints;
    public Transform player;
    Vector3 currentPosition;
    public bool isCombatScene, resetData;
    /// <summary>
    /// Inicializa o jogador na posição correta com base no nível de origem armazenado.
    /// </summary>
    public void InitializePlayerPosition()
    {
        if (resetData)
        {
            PlayerPrefs.SetInt("CurrentOriginLevel", 0);
            PlayerPrefs.SetInt("DoorStatusID", 0);
        }

        int currentOriginLevel = PlayerPrefs.GetInt("CurrentOriginLevel", 0); // Padrão 0

        // Busca o ponto de spawn correspondente
        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint.originLevel == currentOriginLevel)
            {
                // Move o jogador para o ponto de spawn correspondente
                if(!spawnPoint.isPlayerCurrentPosition)
                {
                    player.position = spawnPoint.spawnPosition.position;
                    player.rotation = spawnPoint.spawnPosition.rotation;
                }
                else
                {
                    player.position = currentPosition;
                    player.rotation = spawnPoint.spawnPosition.rotation;
                }
                return;
            }
        }

        Debug.LogWarning("Nenhum ponto de spawn correspondente foi encontrado para o nível de origem: " + currentOriginLevel);
    }

    /// <summary>
    /// Salva o nível de origem atual no PlayerPrefs.
    /// </summary>
    /// <param name="originLevel">O índice do nível de origem.</param>
    public void SetCurrentOriginLevel(int originLevel)
    {
        PlayerPrefs.SetInt("CurrentOriginLevel", originLevel);
        if (!isCombatScene)
        {
            PlayerPrefs.SetFloat("CurrentPlayerPositionX", player.position.x);
            PlayerPrefs.SetFloat("CurrentPlayerPositionY", player.position.y);
            PlayerPrefs.SetFloat("CurrentPlayerPositionZ", player.position.z);
        }
        PlayerPrefs.Save();
    }

    private void Start()
    {
        if(!isCombatScene) player = Character.Instance.transform; 

        currentPosition = new Vector3(PlayerPrefs.GetFloat("CurrentPlayerPositionX"), PlayerPrefs.GetFloat("CurrentPlayerPositionY"), PlayerPrefs.GetFloat("CurrentPlayerPositionZ"));
        
        // Inicializa a posição do jogador quando o script é carregado
        InitializePlayerPosition();
    }
}
