using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEnableCredits : MonoBehaviour
{
    [SerializeField] private GameObject creditMenu;


    public void EnableCreditsButton()
    {
        creditMenu.SetActive(true);
    }

}
