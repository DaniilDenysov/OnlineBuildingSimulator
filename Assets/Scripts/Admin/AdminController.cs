using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AdminController : MonoBehaviour
{
    [SerializeField] private float MovementSpeed;
    [SerializeField] private GameObject camera;

    private void Awake()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Destroy(camera);
            Destroy(this);
        }
    }

    void Update()
    {
            transform.Translate(transform.forward * MovementSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
            transform.Translate(transform.right * MovementSpeed * Input.GetAxis("Horizontal") * Time.deltaTime);

    }

}
