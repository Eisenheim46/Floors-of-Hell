using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContingencyDoor : MonoBehaviour
{
    [Header("Move Obstacle When Completed")]
    [SerializeField] private bool moveOnZ;
    [SerializeField] private float moveAmount;

    [Header("Unique Attributes")]
    [SerializeField] private Transform enemyList;

    private Transform obstacle;
    private Vector3 obstacleOrigin;

    private int enemyListCount;

    private void OnEnable() //Called before Awake and Start
    {
        //Set List Count
        enemyListCount = 0;

        //Check if ObstacleOrigin has the stored origin before setting the obstacle
        if (obstacleOrigin == Vector3.zero) //If the original position is zero then store the origin position
            obstacleOrigin = transform.parent.position;
        else //Set obstacle back to original Position
            obstacle.position = obstacleOrigin;
    }

    private void OnDisable()
    {
        MoveObstacle();
    }

    private void Awake()
    {
        obstacle = transform.parent;
    }

    private void Start()
    {
        obstacleOrigin = transform.parent.position;

        this.enabled = false;
    }

    private void Update()
    {
        enemyListCount = CountEnemies();

        Debug.Log(enemyListCount);

        if (enemyListCount <= 0)
        {
            this.enabled = false;
        }
    }

    public int CountEnemies()
    {
        int counter = 0;

        foreach(Transform child in enemyList)
        {
            counter++;
        }

        return counter;
    }

    private void MoveObstacle()
    {
        Vector3 tempPosition = obstacle.position;

        if (moveOnZ)
            tempPosition.z += moveAmount;
        else
            tempPosition.x += moveAmount;

        obstacle.position = tempPosition;

        //checkpoint.GetComponent<Collider>().enabled = false;
    }


}
