using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoInteractuable : MonoBehaviour
{
    [Header("Info Basica")]
    public string objectName;

    private MeshRenderer meshRend;

    private void Awake()
    {
        meshRend = GetComponent<MeshRenderer>();
    }

    //Obtengo el material del objeto interactuable
    public Material GetMaterialInstance()
    {
        if (meshRend == null)
           meshRend = GetComponent<MeshRenderer>();

            return meshRend != null ? meshRend.material : null;
        
    }
    public void Interactuar()
    {
      //  Debug.Log("Interactuar");
      UIManager.Instance.ShowInteractionCanvas(this);
    }
}
