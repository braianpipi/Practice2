using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoInteractuable : MonoBehaviour
{
    [Header("Info Basica")]
    public string objectName;





    public void Interactuar()
    {
      //  Debug.Log("Interactuar");
      UIManager.Instance.ShowInteractionCanvas(this);
    }
}
