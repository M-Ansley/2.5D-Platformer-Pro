using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Personal
{    
    public class Elevator : MonoBehaviour
    {
        [SerializeField] private Elevator_Panel _elevatorPanel = null;

        public Transform targetA;
        public Transform targetB;
        [SerializeField] private float _speed = 1.0f;

        private bool _movingDown = false;

        public void CallElevator()
        {
            _movingDown = !_movingDown;
            if (_elevatorPanel != null)
            {
                _elevatorPanel.SetButtonColour(Color.green);
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (_movingDown)
            {
                MoveToPosition(targetB.position);
                if (CheckPositionReached(targetB.position))
                {
                    if (_elevatorPanel != null)
                    {
                        _elevatorPanel.SetButtonColour(Color.red);
                    }
                }
            }
            else
            {
                MoveToPosition(targetA.position);
                if (CheckPositionReached(targetA.position))
                {
                    if (_elevatorPanel != null)
                    {
                        _elevatorPanel.SetButtonColour(Color.red);
                    }
                }
            }
        }

        private void MoveToPosition(Vector3 targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * _speed);
        }

        private bool CheckPositionReached(Vector3 targetPosition)
        {
            if (transform.position != targetPosition)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
