using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewOBJSO", menuName = "Interactable / Object ")]

public class objSO : ScriptableObject
{
    public string objectName;
    public GameObject prefab;

    public List<Material> customMaterialsToEdit;
}
