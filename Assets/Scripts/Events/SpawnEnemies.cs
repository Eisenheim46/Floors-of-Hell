using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour, IEvents
{

    [Header("Enemies")]

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
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }
    }


}
