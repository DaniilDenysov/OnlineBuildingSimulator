using TMPro;
using Photon.Pun;
using UnityEngine;


public class BuildingController : MonoBehaviourPunCallbacks
{

    public int state = 10;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject material;
    //[SerializeField] private TMP_Text name;
    [SerializeField] private Transform materialsOutput;
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
       // name = 
    }

    void Update()
    {
        
    }
       

    [PunRPC]
    public void BuildUpdate (int increaseCount)
    {
        Debug.Log("RPC recieved!");
        state += increaseCount;
        if (state >= 100 && photonView.IsMine && material != null) InvokeRepeating(nameof(SpawnMaterials),100,100);
        model.transform.localScale = new Vector3(model.transform.localScale.x, model.transform.localScale.y + 1, model.transform.localScale.z);
        model.transform.position = new Vector3(model.transform.position.x, model.transform.position.y + 0.5f, model.transform.position.z);
    }
     
    void SpawnMaterials ()
    {
        PhotonNetwork.InstantiateRoomObject(material.name,materialsOutput.position,Quaternion.identity);
    }

}
