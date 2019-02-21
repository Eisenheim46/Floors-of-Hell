using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForwardButton : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private MainCharacter mainCharacter;

    public void OnOVRTriggerPressed()
    {
        mainCharacter.ContinueDestination();

        canvas.SetActive(false);
    }
}
