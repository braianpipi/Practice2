using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewOBJSO", menuName = "Interactable / Object ")]

public class objSO : ScriptableObject
{
    public string objectName;
    public GameObject prefab;

    public List<Material> customMaterialsToEdit; //Si esta lista tiene algun material el sistema em va a editar ese material exclusivo
    //Si la lista de materiales esta vacia (Me va a editar todos los materiales que encuentre.

    public bool isTextured = false; //determinar si el objeto utiliza al menos una textura
    public Material texturedMaterial; //Material que va recibir la textured
    public List<Texture> availableTextures; //Listado de texturas disponibles

}
