using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // public TextMeshProUGUI gameoverText;

    public TextMeshProUGUI enemiesLeftText;
    public int enemyCount;


    private void Start()
    {
        
    }

    private void Update()
    {
        UpdateEnemyCount();
        enemiesLeftText.text = "ENEMIES: " + enemyCount;
    }
    private void UpdateEnemyCount()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
    }

    public void GameOver() {
        // gameoverText.gameObject.SetActive(true);
        Debug.Log("Game over");
    }

    
}
