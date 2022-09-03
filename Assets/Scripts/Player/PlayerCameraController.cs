using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float sensivityX, sensivityY, xRotation,yRotation;
    [SerializeField] private Transform orientation;
    private PhotonView photonView;
    // Start is called before the first frame update
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (photonView.IsMine == false)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensivityY;
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90,90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
