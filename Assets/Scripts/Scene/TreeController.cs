using UnityEngine;
using Photon.Pun;

public class TreeController : MonoBehaviour
{
    public int Durability;
    [SerializeField] private GameObject woodPrefarb;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    [PunRPC]
    public void CutDown (int _durability)
    {
        Durability -= _durability;
        animator.Play("Tree_Hitted");
        if (Durability <= 0)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                for (int i = (int)transform.localScale.y; i > 0; i--)
                {
                    PhotonNetwork.Instantiate(woodPrefarb.name, new Vector3(transform.position.x, 0 + i, transform.position.z), Quaternion.identity);
                    Debug.Log(0+i);
                }
            }
          //  if (PhotonNetwork.IsMasterClient) temp.GetComponent<PhotonView>().Ow;
            Destroy(gameObject);
        }
    }  
}
