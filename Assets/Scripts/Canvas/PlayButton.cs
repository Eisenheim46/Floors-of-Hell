using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour, IInteractable
{
    [SerializeField] private string sceneToLoad;

    public void OnOVRTriggerPressed()
    {
    SceneManager.LoadScene(sceneToLoad);
    }
}
