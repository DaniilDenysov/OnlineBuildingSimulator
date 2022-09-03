using UnityEngine.Events;
using UnityEngine;
using Photon.Pun;

public class Item : MonoBehaviour
{

    //[SerializeField] private UnityEvent Method;
    [SerializeField] private string Method;
    [SerializeField] private int Variable, ID;
    //private AdminInventory adminInventory;
    private PhotonView photonView;
    private float speed = 0.3f;

    private void Start()
    {    
        photonView = GetComponent<PhotonView>();
    }

    public void InvokeRPC (string name,int ID)
    {
        photonView.RPC(name, RpcTarget.All,ID);
    }

    [PunRPC]
    public void LearnNewPlan (int ID)
    {
        Debug.Log("RPC recieved!");
        FindObjectOfType<AdminInventory>().LearnNewPlan(ID);
        if (PhotonNetwork.IsMasterClient) PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC]
    public void CollectResource (int Amount)
    {
        Debug.Log("RPC recieved!");
        FindObjectOfType<BuildManager>().ResourcesData[ID].ResourceCount += Amount;
        if (PhotonNetwork.IsMasterClient) PhotonNetwork.Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other) transform.position = Vector3.Lerp(transform.position, other.gameObject.transform.position, speed += 0.05f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            photonView.RPC(Method, RpcTarget.All, Variable);
        }
    }

}
