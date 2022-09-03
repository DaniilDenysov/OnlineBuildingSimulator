using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AdminInventory : MonoBehaviour
{

    [SerializeField] private GameObject[] cell;
    [SerializeField] private GameObject inventoryItem;
    private PhotonView photonView;
    public Build[] reference;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void LearnNewPlan(int ID)
    {
        Debug.Log("RPC recieved");
        photonView.RPC("AddNewPlan",RpcTarget.AllBufferedViaServer,ID);
       
    }

    [PunRPC]
    public void AddNewPlan (int ID)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < cell.Length - 1; i++)
            {
                if (cell[i].transform.childCount == 0)
                {
                    GameObject temp = Instantiate(inventoryItem);
                    temp.GetComponent<RectTransform>().SetParent(cell[i].transform.parent);
                    temp.GetComponent<RectTransform>().anchoredPosition = new Vector3(cell[i].GetComponent<RectTransform>().anchoredPosition.x, cell[i].GetComponent<RectTransform>().anchoredPosition.y,0);
                    temp.GetComponent<ItemController>().objectData = reference[ID];
                    break;
                }
            }
        }
    }  
}
