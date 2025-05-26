using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.DeviceSimulation;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 2f;
    public float minVerticalAngle = -25f;
    public float maxVerticalAngle = 10f;

    private Vector2 StartTouchPosition;
    private bool isDragging = false;
    private float currentRotationX = 0f;

    void Update()
    {
        if (UIManager.Instance.isUIOpen)
            return;
            //mobile input para tocar los toques de la pantalla
        if(Input.touchCount > 0)
        {
            //Guardo el primer toque en una variable
            Touch touch = Input.GetTouch(0);

            //Siginifa que el toque que acabo de hacer en la pantalla corresponde a que yo empece a interactuar con la pantalla.
            if(touch.phase == UnityEngine.TouchPhase.Began)
            {
                StartTouchPosition = touch.position;
                isDragging = false;
            }else if(touch.phase == UnityEngine.TouchPhase.Moved)
            { 
                isDragging = true;
                RotarCamara(touch.deltaPosition);
            }
        }

        //Detectar el input de PC 
        if (Input.GetMouseButtonDown(0))
        {

            StartTouchPosition = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            //isDragging = true;
            RotarCamara((Vector2)Input.mousePosition - StartTouchPosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    void RotarCamara(Vector2 delta)
    {
        float rotX = delta.x * rotationSpeed * Time.deltaTime;
        float rotY = delta.y * rotationSpeed * Time.deltaTime;
    
        //Rotar de forma horizontal (EJE Y)

        transform.Rotate(Vector3.up, -rotX, Space.World);

        //Clampear la camara 
        currentRotationX -= rotY;
        currentRotationX  = Mathf.Clamp(currentRotationX, minVerticalAngle, maxVerticalAngle);

        transform.localRotation = Quaternion.Euler(-currentRotationX, transform.eulerAngles.y, 0);
    }
}
