using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask whatCanBeClickedOn;

    private NavMeshAgent myAgent;
    public Animator playerAnimator;
    public bool isRunning;
    public bool targetedEnemy = false;
    public bool targetedShop = false;
    public bool leftClick = true;
    RaycastHit hitInfo;
    public PlayerStats player;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        myAgent = GetComponent<NavMeshAgent>();
        playerAnimator = GetComponent<Animator>();
        player = GetComponent<PlayerStats>();
        player.health = player.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Moving to position
        if (Input.GetMouseButtonDown(1))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(myRay, out hitInfo, 100, whatCanBeClickedOn))
            {
                myAgent.isStopped = false;
                Debug.Log(hitInfo.point);
                myAgent.SetDestination(hitInfo.point);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(myRay, out hitInfo))
            {
                // Check if click on enemy
                if (hitInfo.transform != null){
                    if (hitInfo.transform.gameObject.tag == "Enemy" ){
                        if (myAgent.isStopped){
                            myAgent.isStopped = false;
                        }
                        targetedEnemy = true;
                        leftClick = true;
                    }
                    if( hitInfo.transform.gameObject.tag == "Shop"){
                        targetedShop = true;
                        myAgent.isStopped = false;
                        myAgent.SetDestination(hitInfo.transform.position);
                    }
                }
            }
        }

        // Follow the targeted enemy or go to the shop
        if (targetedEnemy == true){
            myAgent.isStopped = false;
            myAgent.SetDestination(hitInfo.transform.position);
        }

        movingAnimation();
    }

    void movingAnimation(){
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
        if (other.CompareTag( "Enemy") && leftClick == true) {
            Damage(other.gameObject.transform);
            Attack();
        } 
        if (other.CompareTag( "Shop")){
            Debug.Log("reach shop");
            myAgent.isStopped = true;
        }
    }

    void  OnTriggerStay(Collider other)
    {
        if (other.CompareTag( "Enemy") && leftClick == true) {
            Damage(other.gameObject.transform);
            Attack();
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
        Debug.Log("Player health -" + amount + ", remain " + player.health);

		if (player.health <= 0)
		{
			playerAnimator.SetTrigger("isDead");
            StartCoroutine(gameManager.GameOver());
        }
    }

    void Attack(){
        myAgent.isStopped = true;
        targetedEnemy = false;
        leftClick = false;
        playerAnimator.SetTrigger("isAttacking");
    }

}
