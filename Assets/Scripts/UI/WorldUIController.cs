using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUIController : MonoBehaviour
{
    private Camera camera;
    private Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.rotation;
        camera = FindObjectOfType<Camera>();
    }
    void Update()
    {
        if (camera == null)
        {
            camera = FindObjectOfType<Camera>();
            return;
        }    
        transform.rotation = camera.transform.rotation * originalRotation;
    }
}
