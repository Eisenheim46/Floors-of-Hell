using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainCharacter : MonoBehaviour {
    /// <summary>
    /// Controls the Main Character is supposed to protect. Character uses the Unity Navigation Component to navigate through the level. 
    /// Designer can place points as to where the character should stop and where to continue next.
    /// </summary>
    /// 
    [SerializeField] private int health;

    [SerializeField] private Transform startPoint; //Define the start location
    [SerializeField] private Transform endPoint; //Define the final destination in the inspector

    [SerializeField] private float moveSpeed; //Movement Speed of the character

    [SerializeField] private Animator characterAnimator;

    private NavMeshAgent navAgent;
    private Animator animator;

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;

            if (health <= 0)
            {

            }
        }
    }

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        navAgent.speed = moveSpeed;

        navAgent.SetDestination(endPoint.position); //Update the character's destination
    }

    //Functions to control the main Character
    public void ContinueDestination()
    {
        navAgent.SetDestination(endPoint.position);

        navAgent.isStopped = false;

        characterAnimator.SetBool("Running", true);
    }
    public void RetraceDestination()
    {
        navAgent.SetDestination(startPoint.position);

        navAgent.isStopped = false;

        characterAnimator.SetBool("Running", true);
    }

    public void StopMainCharacter()
    {
        navAgent.isStopped = true;

        characterAnimator.SetBool("Running", false);
    }
    //End Functions to control the main Character

    //Character will trigger the next navigation point as well as any events along the way
    private void OnTriggerEnter(Collider other)
    {
        string objectTag = other.gameObject.tag;

        if (objectTag == "CheckPoint")
        {
            navAgent.isStopped = true;

            characterAnimator.SetBool("Running", false);
        }
        else if (objectTag == "Event") //If it's an event
        {
            other.GetComponent<IEvents>().TriggerEvent(); //Trigger the event
        }
        else if (objectTag == "Enemy")
        {
            Vector3 enemyDirection = other.transform.position - transform.position;

            if (Vector3.Dot(transform.forward, enemyDirection) > 0)
            {
                navAgent.isStopped = true;

                characterAnimator.SetBool("Running", false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string objectTag = other.gameObject.tag;

        if (objectTag == "CheckPoint")
        {
            navAgent.isStopped = false;

            characterAnimator.SetBool("Running", true);
        }
    }

}
