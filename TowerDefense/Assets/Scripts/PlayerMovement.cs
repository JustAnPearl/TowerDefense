using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask whatCanBeClickedOn;

    private NavMeshAgent myAgent;
    public Animator playerAnimator;
    public bool isRunning;
    public bool targetedEnemy = false;
    public bool targetedShop = false;
    public bool leftClick;
    RaycastHit hitInfo1;
    public PlayerStats player;
    public GameManager gameManager;
    private float attackRate = 1.0f;
    private float attackCooldown;
    public TextMeshProUGUI health;
    public GameObject particle;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        myAgent = GetComponent<NavMeshAgent>();
        playerAnimator = GetComponent<Animator>();
        player.maxHealth = 500.0f;
        player.health = player.maxHealth;
        player.speed = 20.0f;
        player.coins = 0;
        player.damage = 20.0f;
        attackCooldown = 0;
        leftClick = false;
    }

    // Update is called once per frame
    void Update()
    {
        health.text = player.health.ToString() + "/" + player.maxHealth.ToString();
        myAgent.speed = player.speed;
        // Moving to position
        if (Input.GetMouseButtonDown(1))
        {
            GotRightClick();
        }

        if (Input.GetMouseButtonDown(0))
        {
            GotLeftClick();
        }

        if (attackCooldown >=0){
            attackCooldown -= Time.deltaTime;
        }


        // Follow the targeted enemy or go to the shop
        if (targetedEnemy == true){
            myAgent.isStopped = false;
            myAgent.SetDestination(hitInfo1.transform.position);
        }

        movingAnimation();
    }

    private void GotLeftClick()
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(myRay, out hitInfo1))
            {
                // Check if click on enemy
                if (hitInfo1.transform != null)
                {
                    if (hitInfo1.transform.gameObject.tag == "Enemy" )
                    {
                        if(attackCooldown <= 0)
                        {
                            if (myAgent.isStopped)
                                myAgent.isStopped = false;
                
                            targetedEnemy = true;
                            leftClick = true;
                            attackCooldown = attackRate;
                        }
                    }

                    if( hitInfo1.transform.gameObject.tag == "Shop")
                    {
                        targetedShop = true;
                        myAgent.isStopped = false;
                        leftClick = true;
                        myAgent.SetDestination(hitInfo1.transform.position);
                    }
                }
            }
    }

    public void GotRightClick()
    {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(myRay, out hitInfo1, 100, whatCanBeClickedOn))
            {
                Instantiate(particle, hitInfo1.point, particle.transform.rotation);
                myAgent.isStopped = false;
                myAgent.SetDestination(hitInfo1.point);
            }
    }

    void movingAnimation()
    {
        if (myAgent.remainingDistance <= myAgent.stoppingDistance || myAgent.isStopped == true)
        {
            isRunning = false;
            myAgent.angularSpeed = 0;
        }
        else
        {
            isRunning = true;
            myAgent.angularSpeed = 600;
        }
        playerAnimator.SetBool("isRunning", isRunning);
    }

    // Reached enemy
    void OnTriggerEnter(Collider other) {
        if(leftClick == true)
        {
            if (other.CompareTag( "Enemy") && other.gameObject.transform.position == hitInfo1.transform.position) 
            {
                Damage(other.gameObject.transform);
                Attack();
            } 
            if (other.CompareTag( "Shop")){
                leftClick = false;
                myAgent.isStopped = true;
            }
        }
    }

    void  OnTriggerStay(Collider other)
    {
        if(leftClick == true)
        {
            if (other.CompareTag( "Enemy") && other.gameObject.transform.position == hitInfo1.transform.position) 
            {
                Damage(other.gameObject.transform);
                Attack();
            } 
        }
    }

    void Damage (Transform enemy)
	{
		Enemy e = enemy.GetComponent<Enemy>();
        transform.LookAt(e.transform);

		if (e != null)
		{
            e.enemyAttacked = true;
			e.TakeDamage(player.damage);
		}
	}

    public void TakeDamage (float amount){
        player.health -= amount;

		if (player.health <= 0)
		{
			playerAnimator.SetTrigger("isDead");
            StartCoroutine(gameManager.GameOver());
        }
    }

    void Attack()
    {
        myAgent.isStopped = true;
        targetedEnemy = false;
        leftClick = false;
        playerAnimator.SetTrigger("isAttacking");
    }

}
