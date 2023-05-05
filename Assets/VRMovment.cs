using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRMovment : MonoBehaviour
{
   
    // Configuramos los objetos y las acciones que vamos a utilizar
    public Transform vrCamera;
 
    public SteamVR_Action_Vector2 touchpadAction;

    public float movementSpeed = 1.0f;
    public float trackpadSensitivity = 1.0f;

    private void Start()
    {
    }
    // Actualizamos el script en cada fotograma
    void Update()
    {
        // Comprobamos si el touchpad derecho ha sido pulsado
        Vector2 touchpadValue = touchpadAction.GetAxis(SteamVR_Input_Sources.Any);

        if (touchpadValue != Vector2.zero) {
            print(touchpadValue);
            Vector3 cameraPosition = vrCamera.position;

            // Obtenemos el vector de entrada del touchpad del controlador derecho

            // Escalamos la entrada del touchpad por la sensibilidad del touchpad
            touchpadValue *= trackpadSensitivity;

            // Movemos la cámara VR en la dirección de la entrada del touchpad
            cameraPosition += vrCamera.transform.forward * touchpadValue.y * movementSpeed;
            cameraPosition += vrCamera.transform.right * touchpadValue.x * movementSpeed;

            // Establecemos la nueva posición de la cámara VR
            vrCamera.position = cameraPosition;
        }
    }
}
