using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryCamControl : MonoBehaviour {


    [SerializeField] private float pitchSpeed;
    [SerializeField] private float yawSpeed;

    private float pitch = 0;
    private float yaw = 0;

	// Update is called once per frame
	void LateUpdate ()
    {

        yaw += Input.GetAxisRaw("Mouse X") * yawSpeed;

        pitch -= Input.GetAxisRaw("Mouse Y") * pitchSpeed;

        transform.eulerAngles = new Vector3(pitch, yaw, 0);

	}

    private void OnApplicationFocus(bool focus)
    {
        if (focus == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }

}
