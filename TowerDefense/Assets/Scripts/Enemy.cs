using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent enemy;
    public GameObject tower;
    public GameObject player;
    public GameManager gameManager;
    public TextMeshProUGUI coins;
	public float startHealth = 60.0f;
	public float health;
    private float attackRate = 5.0f;
    private float cooldown;

    public float enemyDamage = 10.0f;
    public bool enemyAttacked = false;
    public int enemyWorth = 10;
    public bool attackTower = false;
    public bool attackPlayer = false;


    // Start is called before the first frame update
    void Start()
    {
        tower = GameObject.FindWithTag("Tower");
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        enemy = GetComponent<NavMeshAgent>();
		health = startHealth;
        cooldown = attackRate;
    }

    // Update is called once per frame
    void Update()
    {
        // Move to the tower
        enemy.SetDestination(tower.transform.position);
        // Debug.Log("Destination: " + enemy.pathEndPosition);
        
        // got attacked, then follow player
        if (enemyAttacked == true){
            enemy.isStopped = false;
            attackTower = false;
            enemy.SetDestination(player.transform.position);
        }

        // Enemy reached target
        if (enemy.isStopped == true){
            if(cooldown <= 0){
                cooldown = attackRate;
                if (attackTower == true)
                    DamageTower(tower.transform);
                if(attackPlayer == true)
                    DamagePlayer(player.transform);
            }
            else
                cooldown -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Tower")){
            enemy.isStopped = true;
            transform.LookAt(tower.transform);
            attackTower = true;
        }
        else if(other.CompareTag("Sword")){
            transform.LookAt(player.transform);
            enemy.isStopped = true;
            attackPlayer = true;
            enemyAttacked = false;
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.CompareTag("Sword")){
            enemyAttacked = true;
        }
    }

	public void TakeDamage (float amount)
	{
		health -= amount;
        Debug.Log("Enemy health -"+ amount + ", remain " + health);


		if (health <= 0)
		{   
            PlayerStats.coins += enemyWorth;
            Debug.Log(PlayerStats.coins);
			Destroy(gameObject);
		}
	}

    void DamageTower(Transform target){
        Tower t = target.GetComponent<Tower>();
        transform.LookAt(t.transform);

        if (t != null)
		{
			t.towerHealth -= enemyDamage;
            Debug.Log("towerHealth: " + t.towerHealth);
		}
        
    }

    void DamagePlayer(Transform target){
        PlayerMovement p = target.GetComponent<PlayerMovement>();
        transform.LookAt(p.transform);

        if (p != null)
		{
            p.TakeDamage(enemyDamage);
		}   
    }
}

