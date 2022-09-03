using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject [] Object;
   

    void Start()
    {
      //  if (PhotonNetwork.LocalPlayer.IsMasterClient) GetComponent<PhotonView>().RPC("InstantiateObject", RpcTarget.AllBufferedViaServer, 0, new Vector3(transform.position.x, 40, transform.position.z));
       // else GetComponent<PhotonView>().RPC("InstantiateObject", RpcTarget.AllBufferedViaServer, 1, new Vector3(transform.position.x, transform.position.y, transform.position.z));
        if (!PhotonNetwork.LocalPlayer.IsMasterClient) PhotonNetwork.Instantiate(Object[1].name, GetComponentInChildren<Transform>().position, Quaternion.identity);
       // else PhotonNetwork.Instantiate(Object[1].name, GetComponentInChildren<Transform>().position, Quaternion.identity);
    }

    [PunRPC]
    public void InstantiateObject (int ID,Vector3 _position) 
    {
        Debug.Log("Obj: ");
        Debug.Log("Ok");
        Instantiate(Object[ID], _position, Quaternion.identity);
    }

    void Update()
    {
        
    }



}
