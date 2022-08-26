using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    
    void Start ()
    {

    }

    void Update()
    {
        transform.position = playerTransform.position + offset;
    }
    
}
