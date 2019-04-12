using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1Start : MonoBehaviour, IEvents
{

    [SerializeField] MainCharacter character;

    public void TriggerEvent()
    {
        character.StopMainCharacter();
    }
}
