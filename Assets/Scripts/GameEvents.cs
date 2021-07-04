using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    #region Event Types
    [System.Serializable]
    public class IntEvent : UnityEvent<int>
    {        
    }

    #endregion

    #region Events
    public IntEvent coinsCollected;
    
    public void CoinsCollected(int numOfCoins)
    {
        if (coinsCollected != null)
        {
            coinsCollected.Invoke(numOfCoins);
        }
    }


    public IntEvent playerLivesRemaining;

    public void PlayerLivesRemaining (int lives)
    {
        if (playerLivesRemaining != null)
        {
            playerLivesRemaining.Invoke(lives);
        }
    }


    #endregion
}
