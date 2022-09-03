using UnityEngine;
using Photon.Pun;

public class TreeController : MonoBehaviour
{
    public int Durability;
    [SerializeField] private GameObject woodPrefarb;

    [PunRPC]
    public void CutDown (int _durability)
    {
        Durability -= _durability;
        if (Durability <= 0)
        {
            if (PhotonNetwork.IsMasterClient) PhotonNetwork.Instantiate(woodPrefarb.name, transform.position,Quaternion.identity);
          //  if (PhotonNetwork.IsMasterClient) temp.GetComponent<PhotonView>().Ow;
            Destroy(gameObject);
        }
    }
   
}
