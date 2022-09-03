using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class BuildManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject foundation;
    [SerializeField] private LayerMask ignoreLayers;
    [SerializeField] private AdminInventory adminInventory;
    private PhotonView photonView;
    public GameObject temp;
    public List<Building> Register = new List<Building>();
    public List<Resource> ResourcesData = new List<Resource>();

    //  public List<Building> Register = new List<Building>();
    [SerializeField] private TMP_Text text;

    [System.Serializable]
    public class Resource
    {
        public string Name;
        public int ID, ResourceCount;
    }

    [System.Serializable]
    public struct Building
    {
        public int ID;
        public Vector3 buildingPosition;
    }



    void Start()
    {
        adminInventory = GetComponent<AdminInventory>();
        photonView = GetComponent<PhotonView>();
     if (PhotonNetwork.IsMasterClient)   text.text = PhotonNetwork.LocalPlayer.ToString();
    }


    void Update()
    {
        if (photonView.IsMine && temp != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ignoreLayers))
            {
                temp.transform.position = hit.point;
                if (Input.GetKeyUp(KeyCode.Mouse0) && temp.GetComponent<Foundation>().space)
                {
                        Foundation foundation = temp.GetComponent<Foundation>();
                        int[] _Resources = new int[ResourcesData.Count];
                        for (int i = 0; i < foundation.Properties.resource.Length - 1; i++)
                        {
                         //_Resources[foundation.Properties.resource[i].ID] = foundation.Properties.resource;
                        }
                        PhotonNetwork.InstantiateRoomObject(adminInventory.reference[temp.GetComponent<Foundation>().Properties.ID].Prefarb.name, hit.point, Quaternion.identity);
                        photonView.RPC("BuildInfo", RpcTarget.All, hit.point, temp.GetComponent<Foundation>().Properties.ID);
                   
                }

            }
        }
    }

    [PunRPC]
    public void BuildInfo (Vector3 position, int ID)
    {
        Building newBuilding = new Building();
        newBuilding.ID = ID;
        newBuilding.buildingPosition = position;
        Register.Add(newBuilding);
        Foundation foundation = temp.GetComponent<Foundation>();
        for (int i = 0; i < foundation.Properties.resource.Length - 1; i++)
        {
            ResourcesData[foundation.Properties.resource[i].ID].ResourceCount--;
        }
        Debug.Log("Copacity: " + Register.Count);
        Debug.Log("Build RPC recieved!");
        //PhotonNetwork.Instantiate(temp.GetComponent<Foundation>().Properties.Prefarb.name, position,Quaternion.identity);
        if (temp != null) Destroy(temp);
    }                                          

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.ToString());
        if (Register.Capacity > 0)
        {
            int [] ID = new int[Register.Count];
            Vector3 [] objectPosition = new Vector3[Register.Count];
            //List<int> ID = new List<int>();
            //List<Vector3> objectPosition = new List<Vector3>();
            if (PhotonNetwork.IsMasterClient)
            {
                for (int i = 0; i < Register.Count; i++)
                {
                    ID[i] = Register[i].ID;
                    objectPosition[i] = Register[i].buildingPosition;
                }
                photonView.RPC(nameof(SyncObjects), RpcTarget.Others,ID,objectPosition, newPlayer.ToString());
            }
        }
    }

    [PunRPC]
    public void SyncObjects (int [] ID,Vector3 [] objectPosition,string client)
    {
        Debug.Log("Sync command recieved! " + "Local player: " + PhotonNetwork.LocalPlayer.ToString() + " : " + client);
        if (PhotonNetwork.LocalPlayer.ToString() == client)
        {
            Debug.Log("Sync!");
            for (int i = 0;i<ID.Length;i++)
            {
                Instantiate(adminInventory.reference[ID[i]].Prefarb, objectPosition[i], Quaternion.identity);
                Building newBuilding = new Building ();
                newBuilding.ID = ID[i];
                newBuilding.buildingPosition = objectPosition[i];
                Register.Add(newBuilding);
            }
        }
    }
}
