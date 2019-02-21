using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pointer : MonoBehaviour
{
    [Header("Control Panel")]
    [SerializeField] private GameObject controlPanel = null;

    [Header("Pointer Variables")]
    public float m_Distance = 8f;
    public LineRenderer m_LineRenderer = null;
    public LayerMask m_EverythingMask = 0;
    public LayerMask m_InteractableMask = 0;
    public UnityAction<Vector3, GameObject> onPointerUpdate = null;

    private Transform m_CurrentOrigin = null;
    private GameObject m_CurrentObject = null;

    private void Awake()
    {
        PlayerInput.OnControllerSource += UpdateOrigin;

        //Input Functions
        PlayerInput.OnTouchPadDown += ProcessTouchPadDown;
        PlayerInput.OnTriggerDown += ProcessTriggerDown;
    }

    private void Start()
    {
        SetLineColor();

    }

    private void OnDestroy()
    {
        PlayerInput.OnControllerSource -= UpdateOrigin;

        //Input Functions
        PlayerInput.OnTouchPadDown -= ProcessTouchPadDown;
        PlayerInput.OnTriggerDown -= ProcessTriggerDown;
    }

    private void Update()
    {
        Vector3 hitPoint = UpdateLine();

        m_CurrentObject = UpdatePointerStatus();

        if (onPointerUpdate != null)
            onPointerUpdate(hitPoint, m_CurrentObject);
    }

    private Vector3 UpdateLine()
    {
        //Create ray
        RaycastHit hit = CreateRaycast(m_EverythingMask);

        //Default end
        Vector3 endPosition = m_CurrentOrigin.position + (m_CurrentOrigin.forward * m_Distance);

        //Check hit
        if (hit.collider != null)
            endPosition = hit.point;

        //Set Position
        m_LineRenderer.SetPosition(0, m_CurrentOrigin.position);
        m_LineRenderer.SetPosition(1, endPosition);

        return endPosition;
    }

    private void UpdateOrigin(OVRInput.Controller controller, GameObject controllerObject)
    {
        //Set Origin of Pointer
        m_CurrentOrigin = controllerObject.transform;

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

    private GameObject UpdatePointerStatus()
    {
        //Create ray
        RaycastHit hit = CreateRaycast(m_InteractableMask);
        
        //Check hit
        if (hit.collider)
            return hit.collider.gameObject;

        //return
        return null;
    }

    private RaycastHit CreateRaycast(int layer)
    {
        RaycastHit hit;
        Ray ray = new Ray(m_CurrentOrigin.position, m_CurrentOrigin.forward);
        Physics.Raycast(ray, out hit, m_Distance, layer);

        return hit;
    }

    private void SetLineColor()
    {
        if (!m_LineRenderer)
            return;

        Color endColor = Color.red;
        endColor.a = 0f;

        m_LineRenderer.endColor = endColor;
    }



    //Process Player Inputs
    private void ProcessTouchPadDown()
    {
        //Get the camera transform
        Transform mainCamera = Camera.main.transform;

        //Set control panel in front of camera and look at the camera
        controlPanel.SetActive(true);
        controlPanel.transform.position = mainCamera.position + (mainCamera.forward * 5);
        controlPanel.transform.LookAt(mainCamera);

    }

    private void ProcessTriggerDown()
    {
        if (!m_CurrentObject)
            return;

        IInteractable interactable = m_CurrentObject.GetComponent<IInteractable>();
        interactable.OnOVRTriggerPressed();
    }
    //End Process Player Inputs
}
