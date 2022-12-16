using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // public TextMeshProUGUI gameoverText;

    public TextMeshProUGUI enemiesLeftText;
    public TextMeshProUGUI coins;
    public int enemyCount;

    private void Update()
    {
        UpdateEnemyCount();
        enemiesLeftText.text = "ENEMIES: " + enemyCount;
        coins.text = "COINS: " + PlayerStats.coins;
    }
    private void UpdateEnemyCount()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
    }

    public void GameOver() {
        Debug.Log("Game over");
    }

    
}
