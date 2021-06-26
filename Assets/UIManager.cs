using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int _playerCoins;
    [SerializeField] private TextMeshProUGUI _coinsText;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.coinsCollected.AddListener(CoinsCollected);
        _coinsText.text = "Coins: " + _playerCoins.ToString();
    }

    private void CoinsCollected(int numOfCoins)
    {
        _playerCoins += numOfCoins;
        _coinsText.text = "Coins: " + _playerCoins.ToString();
    }
}
