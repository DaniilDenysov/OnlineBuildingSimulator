using Photon.Pun;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject Tree;
    [SerializeField] private int Amount;
   // private BoxCollider colider;
    private float x , z;

    void Start()
    {
        x = GetComponent<BoxCollider>().size.x / 2;
        z = GetComponent<BoxCollider>().size.z / 2;
        for (int i = Amount; Amount > 0; Amount--) PhotonNetwork.InstantiateRoomObject(Tree.name, new Vector3(Random.Range(x, -x), 0f, Random.Range(z, -z)), Quaternion.identity).transform.SetParent(transform);
    }

}
