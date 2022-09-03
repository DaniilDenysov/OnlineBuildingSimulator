using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerControll : MonoBehaviourPunCallbacks
{

    [SerializeField] private Camera camera;
    [SerializeField] private PhotonView view;
    [SerializeField] private float MovementSpeed;
    [SerializeField] private Transform orientation;
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float groundDrag, jumpForce,jumpCooldown,airMultiplier;
    bool grounded, readyToJump = true;
    public TMP_Text text;
    Rigidbody rigidbody;
    Vector3 direction;
    void Awake()
    {
        if (view.IsMine) GetComponent<MeshFilter>().mesh = null;
        rigidbody = GetComponent<Rigidbody>();
        if (photonView.IsMine) text.text = PhotonNetwork.LocalPlayer.ToString();
    }

   

    private void SpeedControl ()
    {
        Vector3 velocity = new Vector3(rigidbody.velocity.x,0f,rigidbody.velocity.z);
        if (velocity.magnitude > MovementSpeed)
        {
            Vector3 limited = velocity.normalized * MovementSpeed;
            rigidbody.velocity = new Vector3(limited.x,rigidbody.velocity.y,limited.z);
        }
    }

    private void Jump ()
    {
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
        rigidbody.AddForce(transform.up * jumpForce,ForceMode.Impulse);
    }


    public void InputState ()
    {
        if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if (Input.GetKeyUp(KeyCode.B) || Input.GetKeyUp(KeyCode.Mouse0))
        {
            Debug.Log("Pressed");
            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, orientation.forward, out hit,10))
            {
                Debug.Log("Raycast");
                // Debug.DrawLine(camera.transform.position, camera.transform.forward, Color.red);
                if (hit.collider.GetComponent<BuildingController>())
                {
                    Debug.Log("Hitted");
                    BuildingController buildingController = hit.collider.GetComponent<BuildingController>();
                    if (buildingController.state < 100)
                    {
                        buildingController.GetComponent<PhotonView>().RPC(nameof(buildingController.BuildUpdate),RpcTarget.AllBufferedViaServer,10);
                       // buildingController.model.transform.localScale = new Vector3(buildingController.model.transform.localScale.x, buildingController.model.transform.localScale.y + 1, buildingController.model.transform.localScale.z);
                       // buildingController.model.transform.position = new Vector3(buildingController.model.transform.position.x, buildingController.model.transform.position.y + 0.5f, buildingController.model.transform.position.z);
                    }
                }
                if (hit.collider.GetComponent<TreeController>())
                {
                    Debug.Log("Hitted");
                    TreeController treeController = hit.collider.GetComponent<TreeController>();
                    hit.collider.GetComponent<PhotonView>().RPC(nameof(treeController.CutDown), RpcTarget.AllBufferedViaServer, 10);
                }
            }
        }
    }

    void Update()
    {
        if (view.IsMine)
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayers);
            SpeedControl();
            direction = orientation.forward * Input.GetAxisRaw("Vertical") + orientation.right * Input.GetAxisRaw("Horizontal");
            InputState();
            if (grounded)
            {
                rigidbody.drag = groundDrag;              
                rigidbody.AddForce(direction.normalized * 10f, ForceMode.Force);              
            }
            else
            {
                rigidbody.drag = 0;
                rigidbody.AddForce(direction.normalized * 10f * airMultiplier, ForceMode.Force);              
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.layer == 7 && photonView.IsMine)
        {
            other.GetComponent<Item>().InvokeRPC("LearnNewPlan",0);
           //PhotonNetwork.Destroy(other.gameObject);
        }*/
    }

    void ResetJump ()
    {
        readyToJump = true;
    }
}
