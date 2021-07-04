using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Personal
{
    public class DeadZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Player>().PlayerDied();
            }
        }
    }
}