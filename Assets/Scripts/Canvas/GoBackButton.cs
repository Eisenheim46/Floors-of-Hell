using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackButton : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private MainCharacter mainCharacter;

    public void OnOVRTriggerPressed()
    {
        mainCharacter.RetraceDestination();

        canvas.SetActive(false);
    }
}
