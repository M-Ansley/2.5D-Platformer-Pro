using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    [SerializeField] private GameObject _padDisplay;
    [SerializeField] private float _minDistance = 0.5f;
    private Vector3 _pressurePadOffset;
    private GameObject _box;


    private void Start()
    {
        _pressurePadOffset = transform.position + new Vector3(0, 0f, 0);

    }

    private void Update()
    {
        if (_box != null)
        {
            float distance = Vector3.Distance(_pressurePadOffset, _box.transform.position);
            if (distance <= _minDistance)
            {
                Rigidbody boxRigidBody = _box.GetComponent<Rigidbody>();
                if (boxRigidBody != null)
                {
                    ChangePadColour(Color.green);
                    boxRigidBody.isKinematic = true;
                    Destroy(this);
                }
            }
            else
            {
                ChangePadColour(Color.yellow);
            }
        }
    }
    

    private void ChangePadColour(Color colour)
    {
        _padDisplay.GetComponent<MeshRenderer>().material.SetColor("_Color", colour);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MoveableObject"))
        {
            _box = other.gameObject;
        }
    }
}
