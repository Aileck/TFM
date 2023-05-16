using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRMovment : MonoBehaviour
{
   
    // Movment
    public Transform vrCamera;
 
    public SteamVR_Action_Vector2 touchpadAction;

    public float movementSpeed = 1.0f;
    public float trackpadSensitivity = 1.0f;

    public ForceMode movmentMode;

    //Rotation
    //private SteamVR_TrackedObject trackedObj;
    //private SteamVR_Controller.Device device;

    private void Start()
    {
    }
    // Actualizamos el script en cada fotograma
    void Update()
    {
        Movment();
        Rotation();

    }

    private void Movment() {
        // Comprobamos si el touchpad derecho ha sido pulsado
        Vector2 touchpadValue = touchpadAction.GetAxis(SteamVR_Input_Sources.Any);

        if (touchpadValue != Vector2.zero)
        {
            Vector3 cameraPosition = vrCamera.position;

            // Obtenemos el vector de entrada del touchpad del controlador derecho

            // Escalamos la entrada del touchpad por la sensibilidad del touchpad
            touchpadValue *= trackpadSensitivity;

            // Movemos la cámara VR en la dirección de la entrada del touchpad
            //cameraPosition += vrCamera.transform.forward * touchpadValue.y * movementSpeed;
            //cameraPosition += vrCamera.transform.right * touchpadValue.x * movementSpeed;

            //// Establecemos la nueva posición de la cámara VR
            //vrCamera.position = cameraPosition;

            Vector3 forceDirection = vrCamera.transform.forward * touchpadValue.y + vrCamera.transform.right * touchpadValue.x;
            forceDirection.Normalize();

            //Debug.Log(forceDirection);

            // Aplicamos la fuerza en la dirección calculada
            this.GetComponent<Rigidbody>().AddForce(forceDirection * movementSpeed, movmentMode);

            //this.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);


        }
    }


    private void Rotation() {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ChangeScene>() != null) {
            other.GetComponent<ChangeScene>().SwitchScene();
        }
    }
}
