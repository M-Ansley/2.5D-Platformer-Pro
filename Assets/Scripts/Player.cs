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
        private Vector3 _direction;
        private Vector3 _velocity;

        private Vector3 _wallJumpDirection;

        private bool _canWallJump = false;

        private float _pushPower = 2f;

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

            if (_controller.isGrounded)
            {
                _direction = new Vector3(horizontalMovement, 0, 0);
                _velocity = _direction * _speed;
                _canDoubleJump = true;
                if (Input.GetButtonDown("Jump"))
                {
                    _yVelocity = _jumpHeight;
                }
            }
            else
            {
                if (Input.GetButtonDown("Jump") && !_canWallJump)
                {
                    if (_canDoubleJump)
                    {
                        _yVelocity += _jumpHeight;
                        _canDoubleJump = false;
                    }
                }
                else if (Input.GetButtonDown("Jump") && _canWallJump)
                {
                    _yVelocity = _jumpHeight;
                    _velocity = _wallJumpDirection * _speed;
                }

                _yVelocity -= _gravity;
            }
            _velocity.y = _yVelocity;

            _controller.Move(_velocity * Time.deltaTime);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit) // whenever the character controller hits something
        {

            if (hit.transform.CompareTag("MoveableObject"))
            {
                Rigidbody rb = hit.collider.attachedRigidbody;
                if (rb != null && !rb.isKinematic && hit.moveDirection.y < 0.3f)
                {
                    Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
                    rb.velocity = pushDir * _pushPower;
                }
            }

            if (!_controller.isGrounded && hit.transform.CompareTag("Wall"))
            {
                _canWallJump = true;
                _wallJumpDirection = hit.normal;
                Debug.DrawLine(hit.point, hit.normal + hit.point, Color.blue); // normal is perpendicular to what we've hit   
                                                                               // https://forum.unity.com/threads/raycasthit-normal-not-correct.776591/

            }
            else
            {
                _canWallJump = false;
            }
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



//if (Input.GetButtonDown("Jump"))
//{
//    if (_canWallJump)
//    {
//        _velocity = _wallJumpDirection * _speed;
//        _yVelocity += _jumpHeight;
//        // _yVelocity += _jumpHeight;
//        // _canWallJump = false;
//    }
//    else
//    {
//        if (_canDoubleJump)
//        {
//            _yVelocity += _jumpHeight;
//            _canDoubleJump = false;
//        }
//        else
//        {
//            _yVelocity -= _gravity;
//        }
//    }

//}
//else
//{
//    _yVelocity -= _gravity;
//}