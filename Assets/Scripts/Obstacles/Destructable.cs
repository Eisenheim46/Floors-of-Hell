using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour, IInteractable
{

    [Header("Move Obstacle When Completed")]
    [SerializeField] private bool moveOnZ;
    [SerializeField] private float moveAmount;

    [Header("Unique Attributes")]
    [SerializeField] private int physicalHealth;
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private AudioClip hitSoundClip;
    [SerializeField] private AudioClip destroyedSoundClip;


    private Transform obstacle;
    private AudioSource objectSound;

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

                explosionParticle.Play();

                objectSound.clip = destroyedSoundClip;
                objectSound.Play();

                Destroy (gameObject, 5);
            }
        }
    }

    private void Awake()
    {
        obstacle = transform.parent;

        objectSound = GetComponent<AudioSource>();
        objectSound.clip = hitSoundClip;
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

    public void OnOVRTriggerPressed()
    {
        ObstacleHealth -= 1;

        objectSound.Play();
    }
}
