using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEnd : MonoBehaviour, IEvents
{
    [SerializeField] private string LoadSceneName;

    public void TriggerEvent()
    {
        SceneManager.LoadScene(LoadSceneName);
    }

}
