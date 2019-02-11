﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWithPanels : MonoBehaviour
{
    [SerializeField] private MainCharacter character;

    private Transform obstacle;

    private int doorPanelsCount;
    public int DoorPanelsCount
    {
        get
        {
            return doorPanelsCount;
        }

        set
        {
            doorPanelsCount = value;

            if (doorPanelsCount <= 0)
            {
                MoveObstacle();
            }
        }
    }


    private void Awake()
    {
        obstacle = transform.parent;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            DoorPanelsCount++;
        }

    }

    private void MoveObstacle()
    {
        Vector3 tempPosition = obstacle.position;

        tempPosition.z = 9.1f;

        obstacle.position = tempPosition;

        //checkpoint.GetComponent<Collider>().enabled = false;
    }
}