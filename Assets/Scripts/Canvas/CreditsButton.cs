using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator menuAnimator;
    [SerializeField] private GameObject buttonMenu;


    public void OnOVRTriggerPressed()
    {
        menuAnimator.SetTrigger("CreditTrigger");

        buttonMenu.SetActive(false);
    }

}
