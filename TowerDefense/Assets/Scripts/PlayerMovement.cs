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

    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(myRay, out hitInfo, 100, whatCanBeClickedOn))
            {
                myAgent.SetDestination(hitInfo.point);
            }
        }

        // Moving animation
        if (myAgent.remainingDistance <= myAgent.stoppingDistance)
        {
            isRunning = false;
        }
        else
        {
            isRunning = true;
        }
        playerAnimator.SetBool("isRunning", isRunning);
    }
}
