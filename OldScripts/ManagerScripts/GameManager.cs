using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool gameOver { get; private set; }
    public GameStatus gameStatus { get; private set; }
    public Transform starPosition, enterPosition;
    private Character character;
    public Timer timer {  get; private set; }
    public string levelName;
    public enum GameStatus
    {
        Normal,
        Paused
    }
    private void Awake()
    {
        Instance = this;        
    }
    void Start()
    {
        character = Character.Instance;
        timer = GetComponent<Timer>();
        PlayerPrefs.SetString("CurrentLevelName", levelName);
        Debug.Log(PlayerPrefs.GetInt("DoorStatusID"));
        if (PlayerPrefs.GetInt("DoorStatusID") == 0 && starPosition != null) character.transform.position = starPosition.position;
        else if(enterPosition != null) character.transform.position = enterPosition.position;
    }
    void Update()
    {
        
    }
    public void Unpause()
    {

    }
    public void GameOver()
    {
        gameStatus = GameStatus.Paused;
        gameOver = true;
    }
}
