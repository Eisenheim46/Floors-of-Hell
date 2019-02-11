using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainCharacter : MonoBehaviour {
    /// <summary>
    /// Controls the Main Character is supposed to protect. Character uses the Unity Navigation Component to navigate through the level. 
    /// Designer can place points as to where the character should stop and where to continue next.
    /// </summary>
    [SerializeField] private Transform EndPoint; //Define the checkpoints in the inspector

    [SerializeField] private float moveSpeed; //Movement Speed of the character

    [SerializeField] private Animator characterAnimator;

    private NavMeshAgent navAgent;
    private Animator animator;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        navAgent.speed = moveSpeed;

        navAgent.SetDestination(EndPoint.position); //Update the character's destination
    }

    public void ContinueDestination()
    {
        navAgent.isStopped = false;

        characterAnimator.SetBool("Running", true);
    }


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
