using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour, IInteractable
{

    private ParticleSystem sparks;
    private AudioSource destroySound;

    private Collider panelCollider;
    private MeshRenderer panelMesh;

    private void Awake()
    {
        sparks = transform.GetComponentInChildren<ParticleSystem>();
        destroySound = GetComponent<AudioSource>();

        panelCollider = GetComponent<Collider>();
        panelMesh = GetComponent<MeshRenderer>();
    }

    //Interface Implementation
    public void OnOVRTriggerPressed()
    {
        transform.parent.GetComponent<DoorWithPanels>().DoorPanelsCount -= 1;

        panelCollider.enabled = false;
        panelMesh.enabled = false;

        sparks.Play();
        destroySound.Play();

        Destroy(gameObject, 5);
    }

}
