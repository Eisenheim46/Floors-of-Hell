using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContingencyDoor : MonoBehaviour
{

    [SerializeField] private Transform enemyList;

    private int enemyListCount;

    private void OnEnable()
    {
        enemyListCount = 0;
    }

    private void OnDisable()
    {
        
    }

    private void Update()
    {
        enemyListCount = CountEnemies();

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
        Vector3 tempPosition = obstacle.localPosition;

        tempPosition.z = 9.1f;

        obstacle.localPosition = tempPosition;

        //checkpoint.GetComponent<Collider>().enabled = false;
    }

}
