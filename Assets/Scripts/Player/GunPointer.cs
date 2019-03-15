using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GunPointer : MonoBehaviour
{
    [Header("Control Panel")]
    [SerializeField] private GameObject controlPanel = null;

    [Header("Pointer Variables")]
    public float m_Distance = 13f;
    public LineRenderer m_LineRenderer = null;
    public LayerMask m_EverythingMask = 0;
    public LayerMask m_InteractableMask = 0;
    public UnityAction<Vector3, GameObject> onPointerUpdate = null;

    [Header("Gun Properties")]
    [SerializeField] private int maxAmmoAmount;
    [SerializeField] private float reloadSpeed;
    [SerializeField] private GunSound gunSound;
    [SerializeField] private GunAnimationHandler left_Gun;
    [SerializeField] private GunAnimationHandler right_Gun;

    [Header("Gun UI")]
    //Reticle Sided UI
    [SerializeField] private Slider reticleReloadSlider;
    [SerializeField] private Slider reticleAmmoSlider;
    //Controller Sided UI
    [SerializeField] private Slider left_ReloadSlider;
    [SerializeField] private Text left_AmmoText;
    [SerializeField] private Slider right_ReloadSlider;
    [SerializeField] private Text right_AmmoText;


    //Pointer Variables
    private Transform m_CurrentOrigin = null;
    private GameObject m_CurrentObject = null;
    //End Pointer Variables

    //Gun UI Variables
    private bool triggerReload = false;
    private float reloadPercentage;
    private int ammoAmount;
    //End GunUI Variables

    //Gun Sound

    //End Gun Sound



    //Properties
    public int P_AmmoAmount
    {
        get
        {
            return ammoAmount;
        }
        set
        {
            ammoAmount = value;

            UpdateGunAmmoUI(value);

            if (ammoAmount <= 0)
            {
                triggerReload = true;

                AnimateGunReload();
            }

        }
    }
    public float P_ReloadPercentage
    {
        get
        {
            return reloadPercentage;
        }
        set
        {
            reloadPercentage = value;

            UpdateGunReloadSliderUI(value);

            if (reloadPercentage >= 100)
            {
                reloadPercentage = 0;

                P_AmmoAmount = maxAmmoAmount;

                triggerReload = false;
            }
        }
    }

    private void Awake()
    {
        PlayerInput.OnControllerSource += UpdateOrigin;

        //Input Functions
        PlayerInput.OnTouchPadDown += ProcessTouchPadDown;
        PlayerInput.OnTouchPadUp += ProcessTouchPadUp;
        PlayerInput.OnTriggerDown += ProcessTriggerDown;
    }

    private void Start()
    {
        SetLineColor();

        //Gun variables Initialize
        reticleReloadSlider.maxValue = 100;
        reticleAmmoSlider.maxValue = maxAmmoAmount;

        left_AmmoText.text = maxAmmoAmount.ToString();
        right_AmmoText.text = maxAmmoAmount.ToString();
        left_ReloadSlider.maxValue = 100;
        right_ReloadSlider.maxValue = 100;

        P_ReloadPercentage = 100;
        P_AmmoAmount = maxAmmoAmount;
    }

    private void OnDestroy()
    {
        PlayerInput.OnControllerSource -= UpdateOrigin;

        //Input Functions
        PlayerInput.OnTouchPadDown -= ProcessTouchPadDown;
        PlayerInput.OnTouchPadUp -= ProcessTouchPadUp;
        PlayerInput.OnTriggerDown -= ProcessTriggerDown;
    }

    private void Update()
    {
        //Create line and rays
        Vector3 hitPoint = UpdateLine();

        m_CurrentObject = UpdatePointerStatus();

        if (onPointerUpdate != null)
            onPointerUpdate(hitPoint, m_CurrentObject);

        //Animate Reload
        if (triggerReload)
            P_ReloadPercentage += reloadSpeed * Time.deltaTime;
    }


    //Process Pointer and Ray
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
        if (controller == OVRInput.Controller.Touchpad)
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

        Color endColor = Color.white;
        endColor.a = 0f;

        m_LineRenderer.endColor = endColor;
    }
    ///End Process Pointer and Ray


    //Process Player Inputs
    private void ProcessTouchPadDown()
    {
        //Get the camera transform
        Transform mainCamera = Camera.main.transform;

        //Set control panel in front of camera and look at the camera
        controlPanel.SetActive(true);
        controlPanel.transform.position = mainCamera.position + (mainCamera.forward * 5);
        controlPanel.transform.rotation = Quaternion.LookRotation(controlPanel.transform.position - mainCamera.position);
    }

    private void ProcessTouchPadUp()
    {
        if(m_CurrentObject.tag == "Button") //Check if Button to avoid simulating a shooting interaction
        {
            //Call Interactable Trigger Function on the button
            IInteractable interactable = m_CurrentObject.GetComponent<IInteractable>();
            interactable.OnOVRTriggerPressed();
        }
        else
        {
            controlPanel.SetActive(false);
        }
            
    }

    private void ProcessTriggerDown()
    {
        if (P_AmmoAmount > 0)
        {
            //Animate Gun
            AnimateGunFire();

            //Play Sound
            gunSound.playShotClip();

            //Decrease Ammo
            P_AmmoAmount -= 1;

            //Check if ray hits an object and trigger the interactable
            if (!m_CurrentObject)
                return;

            //Call Interactable Trigger Function
            IInteractable interactable = m_CurrentObject.GetComponent<IInteractable>();
            interactable.OnOVRTriggerPressed();
        }
    }
    ///End Process Player Inputs


    //Process GunUI
    private void UpdateGunReloadSliderUI(float value)
    {
        reticleReloadSlider.value = value;

        //Update the active reload UI
        if (right_ReloadSlider.IsActive())
            right_ReloadSlider.value = value;

        if (left_ReloadSlider.IsActive())
            left_ReloadSlider.value = value;
    }

    private void UpdateGunAmmoUI(float value)
    {
        reticleAmmoSlider.value = value;

        //Update the active ammo UI
        if (right_AmmoText.IsActive())
            right_AmmoText.text = value.ToString();

        if (left_AmmoText.IsActive())
            left_AmmoText.text = value.ToString();
    }
    //End Process GunUI

    //Process Gun Animation
    private void AnimateGunFire()
    {
        //Animate the active Gun
        if (right_Gun.enabled == true)
            right_Gun.AnimateGunFire();

        if (left_Gun.enabled == true)
            left_Gun.AnimateGunFire();
    }

    private void AnimateGunReload()
    {
        //Animate the active Gun
        if (right_Gun.enabled == true)
            right_Gun.AnimateGunReload();

        if (left_Gun.enabled == true)
            left_Gun.AnimateGunReload();
    }
    //End Process GunUI
}
