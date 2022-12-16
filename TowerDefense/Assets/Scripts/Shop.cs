using UnityEngine;
using UnityEngine.AI;
using TMPro;


public class Shop : MonoBehaviour
{
    public int healthPotions;
    public NavMeshAgent player;
    public GameObject shopUI;
    public TextMeshProUGUI healthPotionsText;
    public TextMeshProUGUI damageCount;
    public TextMeshProUGUI speedCount;
    public PlayerStats playerStats;
    private int healthCost = 50;
    private int speedCost = 100;
    private int damageCost = 150;
    private int increaseHealth = 50;
    private int increaseSpeed = 5;
    private int increaseDamage = 5;
    void Start()
    {   
        player = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        if(Input.GetKey("a") && (healthPotions > 0)){
            usingHealthPotion();
        }
    }
    
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player"))
        {
            Debug.Log("Reached shop");
            shopUI.gameObject.SetActive(true);
        }
        
    }
    void OnTriggerStay(Collider other){
        if(other.CompareTag("Player")){
            Debug.Log("In shop");
            shopUI.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            Debug.Log("Exit shop");
            shopUI.gameObject.SetActive(false);
        }
    }
    
    public void PurchaseIncreaseDamage(){
        if (playerStats.coins >= damageCost){
            playerStats.coins -= damageCost;
            playerStats.damage += increaseDamage;
            damageCount.text = playerStats.damage.ToString();
            Debug.Log("Damage increased!");
        }
    }

    public void PurchaseIncreaseSpeed(){
        if (playerStats.coins >= speedCost){
            playerStats.coins -= speedCost;
            playerStats.speed += increaseSpeed;
            speedCount.text = playerStats.speed.ToString();
            Debug.Log("Speed increased!");
        }
    }

    public void PurchaseHealthPotion(){
        if (playerStats.coins >= healthCost){
            playerStats.coins -= healthCost;
            healthPotions += 1;
            healthPotionsText.text = "x" + healthPotions.ToString();
        }
    }

    public void usingHealthPotion(){
        if((playerStats.health+increaseHealth) <= playerStats.maxHealth){
            playerStats.health += increaseHealth;
            healthPotions -= 1;
            healthPotionsText.text = "x" + healthPotions.ToString();
        }
        Debug.Log("player health: " + playerStats.health);
    }
}
