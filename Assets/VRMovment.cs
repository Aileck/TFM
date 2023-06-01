using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRMovment : MonoBehaviour
{

    // Movment
    public bool KeyboardMode;
    public Transform vrCamera;
 
    public SteamVR_Action_Vector2 touchpadAction;

    public float movementSpeed = 1.0f;
    public float trackpadSensitivity = 1.0f;

    public ForceMode movmentMode;
    public float rotationY = 0f;

    //Rotation
    //private SteamVR_TrackedObject trackedObj;
    //private SteamVR_Controller.Device device;

    private void Start()
    {
        if (KeyboardMode) {
            Cursor.lockState = CursorLockMode.Locked;

            this.transform.position = new Vector3(this.transform.position.x,
                                                  1,
                                                  this.transform.position.z);
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        }
    }
    // Actualizamos el script en cada fotograma
    void Update()
    {
  
        Movment();
        Rotation();

        if (KeyboardMode)
        {
            KeyboardMovment();
            KeyboardRotation();
        }

    }

    private void Movment() {
        // Comprobamos si el touchpad ha sido pulsado
        Vector2 touchpadValue = touchpadAction.GetAxis(SteamVR_Input_Sources.Any);

        if (touchpadValue != Vector2.zero)
        {
            Vector3 cameraPosition = vrCamera.position;

            touchpadValue *= trackpadSensitivity;

            Vector3 forceDirection = vrCamera.transform.forward * touchpadValue.y + vrCamera.transform.right * touchpadValue.x;
            forceDirection.Normalize();

            // Aplicamos la fuerza en la dirección calculada
            this.GetComponent<Rigidbody>().AddForce(forceDirection * movementSpeed, movmentMode);

        }
    }


    private void Rotation() {
        //Debug.Log(SteamVR.);
    }

    private void KeyboardMovment()
    {
        float touchpadX = Input.GetAxis("Horizontal");
        float touchpadY = Input.GetAxis("Vertical");
        Vector2 touchpadValue = new Vector2(touchpadX, touchpadY);

        Debug.Log(touchpadValue);
        if (touchpadValue != Vector2.zero)
        {
            Vector3 cameraPosition = vrCamera.position;

            touchpadValue *= trackpadSensitivity*0.07f;

            Vector3 forceDirection = vrCamera.transform.forward * touchpadValue.y + vrCamera.transform.right * touchpadValue.x;
            //forceDirection.Normalize();

            GetComponent<Rigidbody>().AddForce(forceDirection * movementSpeed, movmentMode);
        }
    }

    private void KeyboardRotation() {
        float mouseX = Input.GetAxis("Mouse X") * trackpadSensitivity * 100 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * trackpadSensitivity * Time.deltaTime;

        rotationY -= mouseX;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(0f, -rotationY, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ChangeScene>() != null) {
            other.GetComponent<ChangeScene>().SwitchScene();
        }
    }
}
