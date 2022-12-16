using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // public GameObject tower;
    public float towerMaxHealth = 2000.0f;
    public float towerHealth;
    // Start is called before the first frame update
    void Start()
    {
        towerHealth = towerMaxHealth;
    }

    void Update(){
        if (towerHealth <= 0)
		{
			Destroy(GameObject.Find("Tower"));
            Debug.Log("Game over");
		}
    }

}
