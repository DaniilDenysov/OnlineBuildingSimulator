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
        FindObjectOfType<AdminInventory>().LearnNewPlan(ID);
        if (photonView.IsMine) PhotonNetwork.Destroy(gameObject);
       // photonView.RPC(nameof(buildManager.LearnNewPlan), RpcTarget.AllBufferedViaServer, ID);
        //buildManager.LearnNewPlan(_reference);
    }

    [PunRPC]
    public void CollectResource (int Amount)
    {
        FindObjectOfType<BuildManager>().ResourcesData[ID].ResourceCount += Amount;
        if (photonView.IsMine) PhotonNetwork.Destroy(gameObject);
        // photonView.RPC(nameof(buildManager.LearnNewPlan), RpcTarget.AllBufferedViaServer, ID);
        //buildManager.LearnNewPlan(_reference);
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other) transform.position = Vector3.Lerp(transform.position, other.gameObject.transform.position, speed += 0.05f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8) photonView.RPC(Method, RpcTarget.All, Variable);
    }

}
