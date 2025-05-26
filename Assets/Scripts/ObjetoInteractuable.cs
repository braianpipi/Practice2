using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjetoInteractuable : MonoBehaviour
{
    //[Header("Info Basica")]
    //public string objectName;

    //private MeshRenderer meshRend;

    //private void Awake()
    //{
    //    meshRend = GetComponent<MeshRenderer>();
    //}

    ////Obtengo el material del objeto interactuable
    //public Material GetMaterialInstance()
    //{
    //    if (meshRend == null)
    //       meshRend = GetComponent<MeshRenderer>();

    //        return meshRend != null ? meshRend.material : null;

    //}    //[Header("Info Basica")]
    //public string objectName;

    //private MeshRenderer meshRend;

    //private void Awake()
    //{
    //    meshRend = GetComponent<MeshRenderer>();
    //}

    ////Obtengo el material del objeto interactuable
    //public Material GetMaterialInstance()
    //{
    //    if (meshRend == null)
    //       meshRend = GetComponent<MeshRenderer>();

    //        return meshRend != null ? meshRend.material : null;

    //}

    [Header("Seteado de Objetos")]
    public objSO[] objetos;//Lista de objetos scriptable

    private int currentIndex = 0;

    //Materiales
    [HideInInspector] public GameObject currentInstance;
    [HideInInspector] public Material[] currenteMaterials;
    [HideInInspector] public List<Material> editableMaterials = new List<Material>();


    public Transform spawnPoint; //Punto donde aparecen los objetos

    private objSO currentObjectSelected;

    public void Start()
    {
        this.gameObject.GetComponent<Renderer>().enabled = false;

        SpawnCurrentObject();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            CambiarObjeto(-1);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            CambiarObjeto(1);
    }
    public void CambiarObjeto(int direccion)
    {
        if (objetos == null || objetos.Length == 0) return;
        currentIndex += direccion;

        if (currentIndex >= objetos.Length) currentIndex = 0;

        if(currentIndex < 0 ) currentIndex = objetos.Length - 1;

        SpawnCurrentObject();

    }
    void SpawnCurrentObject()
    {
        if (spawnPoint == null) return;
        //Destruir instancia anterior
        if(currentInstance != null)
            Destroy(currentInstance);

        //Instanciar un nuevo objeto
        objSO obj = objetos[currentIndex];
        currentInstance = Instantiate(obj.prefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);

    }

    public void Interactuar()
    {
      //  Debug.Log("Interactuar");
      UIManager.Instance.ShowInteractionCanvas(this);
    }
}
