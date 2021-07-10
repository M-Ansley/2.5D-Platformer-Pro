using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Personal
{
    public class Elevator_Collider : MonoBehaviour
    {
        [SerializeField] private Elevator _elevator;
        private bool _playerEntered = false;

        private void Update()
        {
            if (_playerEntered)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _elevator.CallElevator();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.transform.SetParent(_elevator.gameObject.transform);
                if (other.GetComponent<Player>() != null)
                {
                    print("Player entered");
                    _playerEntered = true;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.transform.SetParent(null);
                print("Player exited");
                _playerEntered = false;
            }
        }
    }
}