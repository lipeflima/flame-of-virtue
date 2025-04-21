using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRigidbody2D;
    [SerializeField] private float _playerSpeed;
    private Vector2 _playerDirection;
    
    void Start()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        _playerDirection.x = Input.GetAxisRaw("Horizontal");
        _playerDirection.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        _playerRigidbody2D.MovePosition(_playerRigidbody2D.position + _playerSpeed * Time.fixedDeltaTime * _playerDirection.normalized);
    }
}
