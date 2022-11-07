using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent enemy;
    public GameObject tower;
    // Start is called before the first frame update
    void Start()
    {
        tower = GameObject.Find("Tower");
        enemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(tower.transform.position);
    }
}
