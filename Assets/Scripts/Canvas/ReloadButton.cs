using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadButton : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GunPointer gun;

    public void OnOVRTriggerPressed()
    {

        gun.ManualGunReload();
        //canvas.SetActive(false);
    }

}
