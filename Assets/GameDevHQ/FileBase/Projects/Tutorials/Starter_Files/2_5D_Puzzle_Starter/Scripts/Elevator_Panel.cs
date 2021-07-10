using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Personal
{
    public class Elevator_Panel : MonoBehaviour
    {
        [SerializeField] private Elevator _elevator = null;

        public GameObject _button;
        private bool _buttonPressed = false;

        private Player _player = null;

        [SerializeField] private int _coinsNeeded = 8;

        private int coinsToProgress = 4;

        private bool _playerEntered = false;

        private void Update()
        {
            if (_playerEntered)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    print(_player.PlayerCoins.ToString());
                    if (_player.PlayerCoins >= _coinsNeeded)  // Sufficient coins
                    {
                        SetButtonColour(Color.green);
                        _elevator.CallElevator();
                        _buttonPressed = true;
                        print("Sufficient coins");
                    }
                    else // Insufficient coins. 
                    {
                        print("Insufficient coins");
                    }
                }
            }
        }

        public void SetButtonColour(Color colour)
        {
            _button.GetComponent<MeshRenderer>().material.SetColor("_Color", colour);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (other.GetComponent<Player>() != null)
                {
                    print("Player entered");
                    _player = other.GetComponent<Player>();
                    _playerEntered = true;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                print("Player exited");
                _playerEntered = false;
            }
        }

        //private void OnTriggerStay(Collider other)
        //{
        //    if (other.CompareTag("Player"))
        //    {
        //        if (other.GetComponent<Player>() != null)
        //        {
        //            _player = other.GetComponent<Player>();

        //            if (Input.GetKeyDown(KeyCode.E))
        //            {
        //                print(_player.PlayerCoins.ToString());
        //                if (_player.PlayerCoins >= _coinsNeeded)  // Sufficient coins
        //                {
        //                    _button.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
        //                    _elevator.CallElevator();
        //                    _buttonPressed = true;
        //                    print("Sufficient coins");
        //                }
        //                else // Insufficient coins. 
        //                {
        //                    print("Insufficient coins");
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
