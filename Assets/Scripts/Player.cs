using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Personal
{

    public class Player : MonoBehaviour
    {
        [Header("Player Movement")]
        private CharacterController _controller;
        [SerializeField] private float _speed = 4f;
        private float _gravity = .8f;
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

        protected int myProtectedVar = 3;


        [Header("Player Lives")]
        private UIManager _uiManager;
        [SerializeField] private int _lives = 3;
        public int Lives
        {
            get
            {
                return _lives;
            }
        }
        public Transform _playerStartTransform;

        void Start()
        {
            // _playerStartPostition = transform.position;
            _controller = GetComponent<CharacterController>();
            GameEvents.current.coinsCollected.AddListener(CoinsCollected);
        }

        void Update() // physics update. Consistent. Useful for things require physics movement.
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

        public void PlayerDied()
        {
            if (_lives > 0)
            {
                _lives--;
                _controller.enabled = false; // need to remember to do this
                transform.position = _playerStartTransform.position;
                _controller.enabled = true;
                GameEvents.current.PlayerLivesRemaining(_lives);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }            
        }        
    }
}
