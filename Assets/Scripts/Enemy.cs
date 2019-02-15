using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

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
	
	// Update is called once per frame
	void Update ()
    {
        navAgent.SetDestination(player.position);
	}

    private void OnMouseDown()
    {
        Health -= 1;
    }

    private void OnDestroy()
    {
        
    }

}
