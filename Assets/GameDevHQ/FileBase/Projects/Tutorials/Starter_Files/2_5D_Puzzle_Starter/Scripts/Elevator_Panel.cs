using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Personal
{
    public class Elevator_Panel : MonoBehaviour
    {
        public GameObject _button;
        [SerializeField] private Elevator _elevator = null;
        private Player _player = null;

        [SerializeField] private int _coinsNeeded = 8;

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
    }
}
