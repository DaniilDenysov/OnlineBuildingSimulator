using Photon.Pun;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject [] Tree;
    [SerializeField] private int Amount;
   // private BoxCollider colider;
    private float x , z;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            x = GetComponent<BoxCollider>().size.x / 2;
            z = GetComponent<BoxCollider>().size.z / 2;
            for (int i = Amount; Amount > 0; Amount--)
            {
                PhotonNetwork.InstantiateRoomObject(Tree[Random.Range(0,Tree.Length - 1)].name, new Vector3(Random.Range(x, -x), 0f, Random.Range(z, -z)), Quaternion.identity);
            }
        }
    }
}
