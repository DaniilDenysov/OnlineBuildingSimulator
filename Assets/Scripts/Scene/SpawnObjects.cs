using UnityEngine;
using Photon.Pun;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] private GameObject[] Object;
    [SerializeField] private Transform _position;

    void Start()
    {
        Invoke("Spawn", 10);
    }

    void Update()
    {

    }

    public void Spawn()
    {
        if (GetComponent<PhotonView>().IsMine) PhotonNetwork.InstantiateRoomObject(Object[Random.Range(0, Object.Length - 1)].name, _position.position, Quaternion.identity).transform.SetParent(transform);
    }

 
}
