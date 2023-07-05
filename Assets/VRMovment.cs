using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRMovment : MonoBehaviour
{

    // Movment
    public bool KeyboardMode;
    public bool TestMode;

    public Transform vrCamera;
 
    public SteamVR_Action_Vector2 touchpadAction;

    public float movementSpeed = 1.0f;
    public float trackpadSensitivity = 1.0f;

    public ForceMode movmentMode;
    public float rotationY = 0f;

    public LineRenderer lineRenderer;
    //public float rotationX = 0f;

    //Rotation
    //private SteamVR_TrackedObject trackedObj;
    //private SteamVR_Controller.Device device;

    private void Start()
    {
        if (lineRenderer != null) {
            lineRenderer.SetPosition(0, new Vector3(22, 1, -3));
            lineRenderer.SetPosition(1, new Vector3(22, 1, -3));
            lineRenderer.startWidth = 0.2f;
            lineRenderer.endWidth = 0.2f;

        }


        if (KeyboardMode) {
            //Cursor.lockState = CursorLockMode.Locked;

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

        if(lineRenderer != null)
            UpdateLine();
    }

    private void Movment() {
        // Comprobamos si el touchpad ha sido pulsado
        if (LevelManager.instance == null || (LevelManager.fire || TestMode)) {
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
    }


    private void Rotation() {
        //Debug.Log(SteamVR.);
    }

    private void KeyboardMovment()
    {
        float touchpadX = Input.GetAxis("Horizontal");
        float touchpadY = Input.GetAxis("Vertical");
        Vector2 touchpadValue = new Vector2(touchpadX, touchpadY);

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
        //rotationX -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(0, -rotationY, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ChangeScene>() != null) {
            other.GetComponent<ChangeScene>().SwitchScene();
        }
    }

    void UpdateLine() {

            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount-1, vrCamera.position);
            
        

    }
}
