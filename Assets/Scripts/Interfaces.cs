using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEvents
{
    void TriggerEvent();
}

public interface IInteractable
{
    void OnOVRTriggerPressed();
}
