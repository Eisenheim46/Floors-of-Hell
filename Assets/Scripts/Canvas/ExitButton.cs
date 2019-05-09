using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour, IInteractable
{
    public void OnOVRTriggerPressed()
    {
        Application.Quit();
    }
}
