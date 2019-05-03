using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSound : MonoBehaviour
{
    [SerializeField] private AudioClip shotClip;
    [SerializeField] private AudioClip reloadClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlayShotClip()
    {
        audioSource.clip = shotClip;

        audioSource.Play();
    }

    public void PlayReloadClip()
    {
        audioSource.clip = reloadClip;

        audioSource.Play();
    }

}
