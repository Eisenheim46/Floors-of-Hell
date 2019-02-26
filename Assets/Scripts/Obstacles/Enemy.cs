using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform enemyList;
    [SerializeField] private Transform player;
    [SerializeField] private int health;

    private NavMeshAgent navAgent;


    //Properties
    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;

            if (health == 0)
            {
                navAgent.isStopped = true;

                Destroy(gameObject);
            }
        }
    }

	// Use this for initialization
	void Awake ()
    {
        navAgent = GetComponent<NavMeshAgent>();
	}

    private void Start()
    {
        //Move to the Enemy List
        transform.parent = enemyList;
    }

    // Update is called once per frame
    void Update ()
    {
        navAgent.SetDestination(player.position);
	}

    //private void OnMouseDown()
    //{
    //    Health -= 1;
    //}
    public void OnOVRTriggerPressed()
    {
        Health -= 1;
    }
    private void OnDestroy()
    {
        
    }


}
