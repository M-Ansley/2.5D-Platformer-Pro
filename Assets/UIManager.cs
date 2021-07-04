using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int _playerCoins;
    [SerializeField] private TextMeshProUGUI _coinsText;

    private int _playerLives = 3;
    [SerializeField] private TextMeshProUGUI _livesText;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.coinsCollected.AddListener(CoinsCollected);
        GameEvents.current.playerLivesRemaining.AddListener(UpdateLivesDisplay);

        _coinsText.text = "Coins: " + _playerCoins.ToString();
        _livesText.text = "Lives: " + _playerLives.ToString();
    }

    private void CoinsCollected(int numOfCoins)
    {
        _playerCoins += numOfCoins;
        _coinsText.text = "Coins: " + _playerCoins.ToString();
    }


    private void UpdateLivesDisplay(int lives)
    {
        _playerLives = lives;
        _livesText.text = "Lives: " + _playerLives.ToString();
    }
}
