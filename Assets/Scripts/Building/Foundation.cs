using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation : MonoBehaviour
{
    public bool space = true;
    public Vector3 position;
    public Build Properties;
    [SerializeField] private Material enoughSpace, notEnoughSpace, defaultMaterial;
    // Start is called before the first frame update
    MeshRenderer render;


    void Start()
    {
        
        render = GetComponentInChildren<MeshRenderer>();
        render.material = defaultMaterial;
    }

   
    void Update()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
    }

    void Colide (Collider other)
    {
        if (other.gameObject.layer != 3)
        {
            render.material = notEnoughSpace;
            space = false;
        }
        else
        {
            render.material = enoughSpace;
            space = true;
        }
    }

 

    private void OnTriggerStay(Collider other)
    {
        Colide(other);
    }

}
