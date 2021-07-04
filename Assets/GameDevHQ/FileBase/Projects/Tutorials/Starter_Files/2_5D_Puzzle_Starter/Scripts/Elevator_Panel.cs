using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Panel : MonoBehaviour
{
    public GameObject _button;
    private bool _buttonPressed = false;
    private bool _playerEntered = false;

    private void Update()
    {
        if (_playerEntered)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _button.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
                _buttonPressed = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerEntered = false;
        }
    }
}
