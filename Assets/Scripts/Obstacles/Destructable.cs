using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{

    [SerializeField] private int physicalHealth;

    [SerializeField] private MainCharacter character;

    private Transform obstacle;

    public int ObstacleHealth
    {
        get
        {
            return physicalHealth;
        }

        set
        {
            physicalHealth = value;

            if (physicalHealth <= 0)
            {
                MoveObstacle();

                Destroy (gameObject, 3);
            }
        }
    }

    private void Awake()
    {
        obstacle = transform.parent;
    }

    private void OnMouseDown()
    {
        ObstacleHealth--;
    }
    private void MoveObstacle()
    {
        Vector3 tempPosition = obstacle.position;

        tempPosition.z = 9.1f;

        obstacle.position = tempPosition;

        //checkpoint.GetComponent<Collider>().enabled = false;
    }


}
