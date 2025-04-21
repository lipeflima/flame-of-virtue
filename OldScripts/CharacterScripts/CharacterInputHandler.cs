using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterInputHandler : MonoBehaviour
{
    [SerializeField]
    private int playerID = 0;
    [SerializeField]
    private Player player;
    public Vector2 MovementInput {get; private set;}
	public Vector2 LookInput {get; private set;}
	public bool LeftMouseInput { get; private set; }
    public bool RightMouseInput { get; private set; }
    public bool SprintInput {get; private set;}
    public bool DashInput { get; private set; }
    public bool MinningInput { get; private set;}
    public bool InteractInput { get; private set; }
    public bool PauseInput {get; private set;}
	public float ZoomInput { get; private set; }
    public bool SelectInput {get; private set;}
    public bool ToggleInventoryInput { get; private set; }
    public bool ToggleMapInput { get; private set; }

    public static CharacterInputHandler Instance;
    Camera mainCamera;
    void Awake()
	{
		Instance = this;
	}
    void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
        mainCamera = Camera.main;
    }
    void Update()
	{
		MovementInput = new Vector2(player.GetAxis("Move Horizontal"), player.GetAxis("Move Vertical"));
		LookInput = new Vector2(player.GetAxis("Look Horizontal"), player.GetAxis("Look Vertical"));
		LeftMouseInput = player.GetButton("Left Mouse Button");
        ZoomInput = player.GetAxis("Zoom");
        RightMouseInput = player.GetButton("Secondary Mouse Button");
        DashInput = player.GetButton("Dash");
        MinningInput = player.GetButton("Minning");
        InteractInput = player.GetButton("Interact");
        ToggleInventoryInput = player.GetButtonDown("Toggle Inventory");
        ToggleMapInput = player.GetButtonDown("Toggle Map");
        PauseInput = player.GetButtonDown("Pause");
        SelectInput = player.GetButton("Select");

    }
	public void UsePauseInput() => PauseInput = false;
    public void UseDashInput() => DashInput = false;    
}
