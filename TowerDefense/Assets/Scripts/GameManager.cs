using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverObject;
    public TextMeshProUGUI enemiesLeftText;
    public TextMeshProUGUI coins;
    public int enemyCount;
    public PlayerStats playerStats;


    private void Update()
    {
        UpdateEnemyCount();
        enemiesLeftText.text = "ENEMIES: " + enemyCount;
        coins.text = playerStats.coins.ToString();
    }
    private void UpdateEnemyCount()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
    }

    public IEnumerator GameOver() {
        Debug.Log("Game over");
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
        gameOverObject.gameObject.SetActive(true);
    } 
}
