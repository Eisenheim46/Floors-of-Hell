using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject canvas;

    public void OnOVRTriggerPressed()
    {
        canvas.SetActive(false);
    }

}
