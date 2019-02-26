using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contingency_0 : MonoBehaviour, IEvents
{

    [SerializeField] private ContingencyDoor door;

    [SerializeField] private GameObject[] enemies;

    private void Start()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }

    public void TriggerEvent()
    {
        door.enabled = true;

        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }

        GetComponent<Collider>().enabled = false;
    }
}
