using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour, IInteractable
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

                var eventComponent = GetComponent<IEvents>();

                if (eventComponent != null)
                {
                    eventComponent.TriggerEvent();
                }

                Destroy (gameObject, 3);
            }
        }
    }

    private void Awake()
    {
        obstacle = transform.parent;
    }

    private void MoveObstacle()
    {
        Vector3 tempPosition = obstacle.localPosition;

        tempPosition.z = 9.1f;

        obstacle.localPosition = tempPosition;

        //checkpoint.GetComponent<Collider>().enabled = false;
    }

    public void OnOVRTriggerPressed()
    {
        ObstacleHealth -= 1;
    }
}
