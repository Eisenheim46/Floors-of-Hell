using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour, IInteractable
{

    //private void OnMouseDown()
    //{
    //    transform.parent.GetComponent<DoorWithPanels>().DoorPanelsCount -= 1;

    //    Destroy(gameObject);
    //}

    public void OnOVRTriggerPressed()
    {
        transform.parent.GetComponent<DoorWithPanels>().DoorPanelsCount -= 1;

        Destroy(gameObject);
    }

}
