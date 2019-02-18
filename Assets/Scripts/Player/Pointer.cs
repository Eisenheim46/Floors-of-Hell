﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{

    public float m_Distance = 10f;
    public LineRenderer m_LineRenderer = null;
    public LayerMask m_EverythingMask = 0;
    public LayerMask m_InteractableMask = 0;

    private Transform m_currentOrigin = null;

    private void Awake()
    {
        PlayerInput.OnControllerSource += UpdateOrigin;
        PlayerInput.OnTouchPadDown += ProcessTouchPadDown;
    }

    private void Start()
    {
        SetLineColor();

    }

    private void OnDestroy()
    {
        PlayerInput.OnControllerSource -= UpdateOrigin;
        PlayerInput.OnTouchPadDown -= ProcessTouchPadDown;
    }

    private void Update()
    {
        Vector3 hitPoint = UpdateLine();
    }

    private Vector3 UpdateLine()
    {
        //Create ray
        RaycastHit hit = CreateRaycast(m_EverythingMask);

        //Default end
        Vector3 endPosition = m_currentOrigin.position + (m_currentOrigin.forward * m_Distance);

        //Check hit
        if (hit.collider != null)
            endPosition = hit.point;

        //Set Position
        m_LineRenderer.SetPosition(0, m_currentOrigin.position);
        m_LineRenderer.SetPosition(1, endPosition);

        return Vector3.zero;
    }

    private void UpdateOrigin(OVRInput.Controller controller, GameObject controllerObject)
    {
        //Set Origin of Pointer
        m_currentOrigin = controllerObject.transform;

        //Is the laser visible
        if(controller == OVRInput.Controller.Touchpad)
        {
            m_LineRenderer.enabled = false;
        }
        else
        {
            m_LineRenderer.enabled = true;
        }
    }

    private RaycastHit CreateRaycast(int layer)
    {
        RaycastHit hit;
        Ray ray = new Ray(m_currentOrigin.position, m_currentOrigin.forward);
        Physics.Raycast(ray, out hit, m_Distance, layer);

        return hit;
    }

    private void SetLineColor()
    {
        if (!m_LineRenderer)
            return;

        Color endColor = Color.white;
        endColor.a = 0f;

        m_LineRenderer.endColor = endColor;
    }

    private void ProcessTouchPadDown()
    {

    }

}
