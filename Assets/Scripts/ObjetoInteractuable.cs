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
    [HideInInspector] public Material[] currentMaterials;
    [HideInInspector] public List<Material> editableMaterials = new List<Material>();

    public Vector3 offset;

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
        if (currentInstance != null)
            Destroy(currentInstance);

        //Instanciar un nuevo objeto
        objSO obj = objetos[currentIndex];
        currentInstance = Instantiate(obj.prefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);


        Renderer rend = currentInstance.GetComponent<Renderer>();

        if (rend != null)
        {
            currentObjectSelected = objetos[currentIndex];
            //Clonar los materiales del objeto instanciado
            Material[] originalMats = rend.sharedMaterials;
            currentMaterials = new Material[originalMats.Length];
            //Estoy guardando todos los mateariles del objeto que estoy spawneando los estoy guardando para volvera utilizar, una vez guardado 

            for(int i=0; i< originalMats.Length; i++)
            {
                if(originalMats[i] != null)
                {
                //Clonando los materiales originales y los guardo 
                currentMaterials[i] = new Material(originalMats[i]);
                }else
                {
                    currentMaterials[i] = null;    //No tiene materiales originales
                }
            }

            //Guardar referencia a los materiales originales
            rend.materials = currentMaterials;
            //Esto es todo los materiales que sean editables los voy a borrar porque los quiero editar desde cero;
            editableMaterials.Clear();

            if (currentObjectSelected.customMaterialsToEdit != null && currentObjectSelected.customMaterialsToEdit.Count > 0) 
            {
                foreach(Material matToEdit in currentObjectSelected.customMaterialsToEdit)
                {
                    foreach( Material clonado in currentMaterials)
                    {
                        if (clonado.name.StartsWith(matToEdit.name))
                        {
                            editableMaterials.Add(clonado);
                        }
                    }
                }
            }else
            {
                editableMaterials.AddRange(currentMaterials);
            }
        }
    }

    public void Interactuar()
    {
      //  Debug.Log("Interactuar");
      UIManager.Instance.ShowInteractionCanvas(this);
    }
}
