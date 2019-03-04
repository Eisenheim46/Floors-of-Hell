using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour, IInteractable
{

    private ParticleSystem sparks;

    private Collider panelCollider;
    private MeshRenderer panelMesh;

    private void Awake()
    {
        sparks = transform.GetComponentInChildren<ParticleSystem>();

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

        Destroy(gameObject, 5);
    }

}
