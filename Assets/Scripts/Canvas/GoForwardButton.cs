using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForwardButton : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private MainCharacter mainCharacter;

    public void OnOVRTriggerPressed()
    {
        if (!mainCharacter.IsAtCheckPoint)
            mainCharacter.ContinueDestination();

        canvas.SetActive(false);
    }
}
