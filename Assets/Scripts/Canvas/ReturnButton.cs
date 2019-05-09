using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnButton : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator menuAnimator;
    [SerializeField] private GameObject buttonMenu;
    [SerializeField] private GameObject creditMenu;

    public void OnOVRTriggerPressed()
    {
        menuAnimator.SetTrigger("CreditReturnTrigger");

        buttonMenu.SetActive(true);
        creditMenu.SetActive(false);
    }
}
