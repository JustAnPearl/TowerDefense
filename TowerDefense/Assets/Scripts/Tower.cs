using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tower : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject tower;
    public GameObject gameOverObject;
    public float towerMaxHealth = 1500.0f;
    public float towerHealth;
    public TextMeshProUGUI health;

    void Start()
    {
        tower = GameObject.Find("Tower");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        towerHealth = towerMaxHealth;
    }

    void Update()
    {
        health.text = towerHealth.ToString() + "/" + towerMaxHealth.ToString();
        if (towerHealth <= 0)
		{
            Destroy(GameObject.Find("Tower"));

            Time.timeScale = 0;
            gameOverObject.gameObject.SetActive(true);
        }
    }

}
