using UnityEngine;
using UnityEngine.AI;
using TMPro;


public class Shop : MonoBehaviour
{
    public int healthPotions;
    public NavMeshAgent player;
    public GameObject shopUI;
    public TextMeshProUGUI healthPotionsText;
    public PlayerStats playerStat;
    private int healthCost = 20;
    private int speedCost = 40;
    private int damageCost = 50;
    private int increaseHealth = 50;
    void Start()
    {   
        player = GetComponent<NavMeshAgent>();
        playerStat = GetComponent<PlayerStats>();

    }
    
    private void Update()
    {
        healthPotionsText.text = "x" + healthPotions.ToString();
        if(Input.GetKey("a") && healthPotions > 0){
            usingHealthPotion();
        }
    }
    
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player"))
            Debug.Log("Reached shop");
            shopUI.gameObject.SetActive(true);
    }
    void OnTriggerStay(Collider other){
        if(other.CompareTag("Player"))
            Debug.Log("In shop");
            shopUI.gameObject.SetActive(true);
    }

    void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            Debug.Log("Exit shop");
            shopUI.gameObject.SetActive(false);
        }
    }
    
    public void PurchaseIncreaseDamage(){
        if (PlayerStats.coins > damageCost){
            PlayerStats.coins -= damageCost;
            Debug.Log("Damage increased!");
        }
    }

    public void PurchaseIncreaseSpeed(){
        if (PlayerStats.coins > speedCost){
            PlayerStats.coins -= speedCost;
            Debug.Log("Speed increased!");
        }
    }

    public void PurchaseHealthPotion(){
        if (PlayerStats.coins >= healthCost){
            PlayerStats.coins -= healthCost;
            healthPotions += 1;
           
        }
    }

    public void usingHealthPotion(){
        if((playerStat.health+increaseHealth) <= playerStat.maxHealth){
            playerStat.health += increaseHealth;
            healthPotions -= 1;
        }
    }
}
