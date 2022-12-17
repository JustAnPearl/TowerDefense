using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnLocations;
    public int enemyCount;
    public int level = 1;
    public int maxLevel = 10;
    public float timeBetweenRounds = 5.0f;
    public float currentTime = 0;
    public float countdownTime = 5.0f;
    private int[] enemiesToSpawn = {5,8,13,20,23,27,35,37,44,50};

    private int tmp = 1;
    private int spawnEnemies = 0;
    public TextMeshProUGUI currentRoundText;
    public TextMeshProUGUI countdownText;
    public EnemyStats enemyStats;
    public GameObject congrats;

    private void Start()
    {
        // spawnEnemyWave(level);
        enemyStats.maxHealth = 60.0f;
        enemyStats.damage = 10.0f;
        enemyStats.speed = 10.0f;
        StartCoroutine("SpawnEnemies");
    }
    private void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0 && level <= maxLevel)
        {
            //count up current time
            currentTime += Time.deltaTime;

            countdownTime -= Time.deltaTime;
            if(tmp == level){
                level++;
            }

            //when current time reaches time between rounds, start next round
            if (currentTime >= timeBetweenRounds)
            {
                Debug.Log(level);
                currentTime = 0;
                tmp++;
                spawnEnemies = 0;
                enemyStats.maxHealth += 10.0f;
                enemyStats.damage += 5.0f;
                enemyStats.speed += 1.0f;
                StartCoroutine("SpawnEnemies");
            }
            
        }
        else if (enemyCount != 0)
        {
            countdownText.text = "";
            countdownTime = 5.0f;
        }
        else if (level > maxLevel){
            Time.timeScale = 0;
            congrats.gameObject.SetActive(true);
        }

        if (enemyCount == 0 && level <= maxLevel)
        {
            countdownText.text = "Next round in: " + countdownTime.ToString("0");
        }

        //update round counter text
        currentRoundText.text = "ROUND: " + level;
    }

    IEnumerator SpawnEnemies(){
        Debug.Log("start spawning");
        while(spawnEnemies < enemiesToSpawn[level-1])
        {
            Vector3 spawnLocation = spawnLocations[Random.Range(0,spawnLocations.Length)].transform.position;
            Instantiate(enemyPrefab,spawnLocation , enemyPrefab.transform.rotation);
            spawnEnemies += 1;
            yield return new WaitForSeconds(3.0f);
        }
    }

}