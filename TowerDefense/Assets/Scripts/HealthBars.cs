using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HealthBars : MonoBehaviour
{
    public PlayerStats playerStats;
    public Image playerHPFilled;
    public GameObject tower;
    public Image towerHPFilled;

    void Start()
    {
        tower = GameObject.FindWithTag("Tower");;
    }

    void Update()
    {
        GetPlayerHPFill();
        GetTowerHPFill();
    }

    void GetPlayerHPFill()
    {
        float fillAmount = playerStats.health/playerStats.maxHealth;
        playerHPFilled.fillAmount = fillAmount;
    }

    void GetTowerHPFill()
    {
        Tower t = tower.GetComponent<Tower>();
        float fillAmount = t.towerHealth/t.towerMaxHealth;
        towerHPFilled.fillAmount = fillAmount;
    }
}
