using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

//Para hacer un nuevo Layout 
public class SelectorObjetos : MonoBehaviour
{

    [Header("Tiempo requerido para detectar Interaccion")]
    public float holdTimeFreshold = 0.1f; //Tiempo requerido para detectar el click mantenido
    [Header("Movimiento maximo requerido para detectar Interaccion")]
    public float maxHoldMovement = 15f; //Es el parametro para el radio donde se obtiene el click para contemplar el movimiento del dedo

    [Header("Capa con las que podemos interactuar")]
    public LayerMask interactableLayer; //Identificar las capas que son interactivas.

    private Vector2 startTouchPosition;
    private float holdTimer = 0f; //Temporizador para mantener apretando algo 
    private bool isHolding = false;
    //Para la UI()
    //public Text objectName;

    void Update()
    {
        if (UIManager.Instance.isUIOpen)
            return;
        //Detecto si toque la pantalla
        if (Input.touchCount > 0)
        {
            //guardo el primer toque 
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                //Metodo que detecta si toco un objeto interactuable  
                StartHoldDetection(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                //Determina que el movimiento no salga por afuera del radio (para celulares)
                CheckHoldMovement(touch.position);

            }else if(touch.phase == TouchPhase.Stationary)
            {
                //Se actualiza el temporizador si tengo el objeto quieto (Stationary)
                UpdateHoldTimer(touch.position);
            }else if(touch.phase == TouchPhase.Ended)
            {
                //Aca es cuando dejo interactuar con el objeto 
                isHolding = false;
            }

        }
        //PC INPUT

        if (Input.GetMouseButtonDown(0)) //HAGO CLICK
        {
            StartHoldDetection(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0)) //MANTENGO APRETADO
        {
            CheckHoldMovement(Input.mousePosition);
            UpdateHoldTimer(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))//Suelto el click
        {
            isHolding = false;
        }
    }
    void StartHoldDetection(Vector2 pos)
    {
        startTouchPosition = pos; //El lugar donde hice click en la pantalla
        holdTimer = 0f; //Resetea el temporizador que si llega a un segundo abre el menu ui;
        isHolding = true; //esto es para si lo estoy manteniendo;
    }

    void CheckHoldMovement(Vector2 pos)
    {
        if (Vector2.Distance(startTouchPosition, pos) > maxHoldMovement)
        {
            isHolding = false;//Cancela la rececpcion del dedo si arrastra muy lejos del objeto.

        }

    }
    void DetectarObjeto(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, interactableLayer))
        {
            //Debug.Log("Objeto interactuable : "+ hit.transform.gameObject.name);
            ObjetoInteractuable objeto = hit.collider.GetComponent<ObjetoInteractuable>();
            //Crea un rayo y donde colisione vas a obtener el objeto interactuable que vas a tener.

            if (objeto != null)
            {
                objeto.Interactuar();
            }
        }
    }
    //Actualiza el temporizador si mantenemos apretado el dedo sobre un objeto
    void UpdateHoldTimer(Vector2 pos)
    {
        if (!isHolding) return;
        //Contador de tiempo
        holdTimer += Time.deltaTime;

        if (holdTimer >= holdTimeFreshold)
        {
            DetectarObjeto(pos);
            isHolding = false;
        }
    }
}

