using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set;}
    // el header es para la interfa
    [Header("UI")] //
    [SerializeField] private Canvas interactionCanvas; //serializeField permite editar un atributo privado desde el inspector de unity / aca tengo el canvas
    [SerializeField] private TMP_Text objectNameText; //Aca tengo el nombre del objeto 

    private ObjetoInteractuable currentObject;
    public bool isUIOpen { get; private set; } = false; // Esto para que el canvas se abra y obligo a cerrarla para que pueda interactuar con otro objeto


    void Awake()
    {
        Singleton();
        //Esto es para desactivarlo apenas inicia
        interactionCanvas.gameObject.SetActive(false);
    }

    void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;

        }

        Instance = this;
    }
    public void ShowInteractionCanvas(ObjetoInteractuable target)
    {
        currentObject = target;

        interactionCanvas.gameObject.SetActive(true);
        isUIOpen = true;

        objectNameText.text = "Interactuando con : " + target.objectName;


    }

    public void HideInteractionCanvas()
    {
        interactionCanvas.gameObject.SetActive(false);
        isUIOpen=false;

        objectNameText.text = "";
        currentObject = null;
    }
}
