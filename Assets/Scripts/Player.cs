using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    private CharacterController _controller;
    [SerializeField] private float _speed = 4f;
    private float _gravity = 1f;
    [SerializeField] private float _jumpHeight = 15.0f;
    private float _yVelocity;
    private bool _canDoubleJump = true;

    [Header("Player Inventory")]
    private int _playerCoins;
    public int PlayerCoins
    {
        get
        {
            return _playerCoins;
        }       
    }


    void Start()
    {
        _controller = GetComponent<CharacterController>();
        GameEvents.current.coinsCollected.AddListener(CoinsCollected);
    }

    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalMovement, 0, 0);
        Vector3 velocity = direction * _speed;

        if (_controller.isGrounded)
        {
            _canDoubleJump = true;
            if (Input.GetButtonDown("Jump"))
            {
                _yVelocity = _jumpHeight;
            }
        }
        else
        {

            if (_canDoubleJump)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    _yVelocity += _jumpHeight;
                    _canDoubleJump = false;
                }
                else
                {
                    _yVelocity -= _gravity;
                }
            }
            else
            {
                _yVelocity -= _gravity;
            }
        }
        velocity.y = _yVelocity;

        _controller.Move(velocity * Time.deltaTime);
    }


    private void CoinsCollected(int numOfCoins)
    {
        _playerCoins += numOfCoins;  
    }
}
