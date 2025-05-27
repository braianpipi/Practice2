using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    // el header es para la interfa
    [Header("UI")] //
    [SerializeField] private Canvas interactionCanvas; //serializeField permite editar un atributo privado desde el inspector de unity / aca tengo el canvas
    [SerializeField] private TMP_Text objectNameText; //Aca tengo el nombre del objeto 

    [Header("UI Panels")]
    [SerializeField] private GameObject textureNavigationPanel;
    [SerializeField] private GameObject meshesNavigaationPanel;

    [Header("Slider Colores")]
    [SerializeField] private Slider rSlider, gSlider, bSlider;
    [SerializeField] private TMP_Text rValueText, gValueText, bValueText;


    private ObjetoInteractuable currentObject;
    public bool isUIOpen { get; private set; } = false; // Esto para que el canvas se abra y obligo a cerrarla para que pueda interactuar con otro objeto


    void Awake()
    {
        Singleton();

        rSlider.onValueChanged.AddListener(OnSliderValueChanged);
        gSlider.onValueChanged.AddListener(OnSliderValueChanged);
        bSlider.onValueChanged.AddListener(OnSliderValueChanged);
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
        /*
        //Mostrando info nombre
        // objectNameText.text = "Interactuando con : " + target.objectName;

        //Cambio de colores

        //    Material mat = target.GetMaterialInstance();
        //    if (mat != null)
        //    {
        //        Color currentColor = mat.color;

        //        rSlider.SetValueWithoutNotify(currentColor.r);
        //        gSlider.SetValueWithoutNotify(currentColor.g);
        //        bSlider.SetValueWithoutNotify(currentColor.b);

        //        //Mostrar los valores de RGB en texto en pantalla (0-250)

        //        rValueText.text = Mathf.RoundToInt(currentColor.r * 255).ToString();
        //        gValueText.text = Mathf.RoundToInt(currentColor.g * 255).ToString();
        //        bValueText.text = Mathf.RoundToInt(currentColor.b * 255).ToString();

        //    }
        //Cambio de colores
        */

        //Trabajar con las texturas (Si es que las tiene)
        bool hasTextures = currentObject.objetos != null && currentObject.objetos.Length > 0 && currentObject.objetos[currentObject.currentIndex].isTextured && currentObject.objetos[currentObject.currentIndex].availableTextures != null && currentObject.objetos[currentObject.currentIndex].availableTextures.Count>0;

        //si encuentra algun panel de textura se activa el panel de texturas
        if(textureNavigationPanel != null)
        {
            textureNavigationPanel.SetActive(hasTextures);
        }

        bool hasMultipleMeshes = currentObject.objetos != null && currentObject.objetos.Length > 1;

        if(meshesNavigaationPanel != null)
        {
            meshesNavigaationPanel.SetActive(hasMultipleMeshes);
        }

        if (currentObject.editableMaterials != null && currentObject.editableMaterials.Count > 0)
        {
            Color currentColor = currentObject.editableMaterials[0].color;
            
                rSlider.SetValueWithoutNotify(currentColor.r);
                gSlider.SetValueWithoutNotify(currentColor.g);
                bSlider.SetValueWithoutNotify(currentColor.b);

                //Mostrar los valores de RGB en texto en pantalla (0-250)

                rValueText.text = Mathf.RoundToInt(currentColor.r * 255).ToString();
                gValueText.text = Mathf.RoundToInt(currentColor.g * 255).ToString();
                bValueText.text = Mathf.RoundToInt(currentColor.b * 255).ToString();
        }
    }

    public void HideInteractionCanvas()
    {
        interactionCanvas.gameObject.SetActive(false);
        isUIOpen = false;

        objectNameText.text = "";
        currentObject = null;

        textureNavigationPanel.SetActive(false);
        //meshesNavigaationPanel.SetActive(false);
    }

    public void OnSliderValueChanged(float value)
    {
        if (currentObject == null || currentObject.currentMaterials == null) return;

        Color newColor = new Color(rSlider.value, gSlider.value, bSlider.value);

        foreach(Material mat in currentObject.editableMaterials)
        {
            if(mat != null)
            {
                mat.color = newColor;
            }
        }

        //if (currentObject == null) return;

        //Color newColor = new Color(rSlider.value, gSlider.value, bSlider.value);

        //Renderer rend = currentObject.GetComponent<Renderer>();

        //if (rend != null)
        //{
        //    rend.material.color = newColor;
        //}
        //rValueText.text = Mathf.RoundToInt(rSlider.value * 255).ToString();
        //gValueText.text = Mathf.RoundToInt(gSlider.value * 255).ToString();
        //bValueText.text = Mathf.RoundToInt(bSlider.value * 255).ToString();
    }


    //public void NextObject()
    //{
    //    if (currentObject != null)
    //    {
    //        currentObject.CambiarObjeto(1);
    //    }
    //}

    public void SiguienteObjeto() => currentObject?.CambiarObjeto(1);

    public void AnteriorObjeto() => currentObject?.CambiarObjeto(-1);

    public void OnNextTextureClicked() => currentObject?.NextTexture();
    public void OnPreviousTextureClicked() => currentObject?.PreviousTexture();
}




//ASSETS DESCARGADOS PARA UI
//Simple Pie Menu | Radial Menu Asset
//Easy UI Panel Manager
//Sleek essential UI pack
//Dark Theme UI
//Interior House Assets | URP