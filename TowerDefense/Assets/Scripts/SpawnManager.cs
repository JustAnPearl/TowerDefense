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
    private int spawnEnemies = 0;
    public TextMeshProUGUI currentRoundText;
    public TextMeshProUGUI countdownText;

    private void Start()
    {
        // spawnEnemyWave(level);
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

            //when current time reaches time between rounds, start next round
            if (currentTime >= timeBetweenRounds)
            {
                currentTime = 0;
                level++;
                spawnEnemies = 0;
                StartCoroutine("SpawnEnemies");
            }
            
        }
        else if (enemyCount != 0)
        {
            countdownText.text = "";
            countdownTime = 5.0f;
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