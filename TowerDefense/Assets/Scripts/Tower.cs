using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tower : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject tower;
    public GameObject gameOverObject;


    // public GameObject tower;
    public float towerMaxHealth = 700.0f;
    public float towerHealth;
    public TextMeshProUGUI health;
    // Start is called before the first frame update
    void Start()
    {
        tower = GameObject.Find("Tower");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        towerHealth = towerMaxHealth;
    }

    void Update(){
        health.text = towerHealth.ToString() + "/" + towerMaxHealth.ToString();
        if (towerHealth <= 0)
		{
            //Time.timeScale = 0;
            Destroy(GameObject.Find("Tower"));
            Debug.Log("Game over");

            Time.timeScale = 0;
            gameOverObject.gameObject.SetActive(true);

            //StartCoroutine(gameManager.GameOver());
            //tower.gameObject.SetActive(false);
        }
    }

}
