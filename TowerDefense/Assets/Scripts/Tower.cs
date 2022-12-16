using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameManager gameManager;
    // public GameObject tower;
    public float towerMaxHealth = 2000.0f;
    public float towerHealth;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        towerHealth = towerMaxHealth;
    }

    void Update(){
        if (towerHealth <= 0)
		{
			Destroy(GameObject.Find("Tower"));
            Debug.Log("Game over");
            //gameManager.GameOver();
        }
    }

}
