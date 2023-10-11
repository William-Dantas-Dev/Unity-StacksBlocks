using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform pointToView;
    [SerializeField] private Vector3 offset;
    private Vector3 speed;
    void Start()
    {
        //transform.LookAt(pointToView);
    }

    
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, pointToView.position + offset, ref speed, 0.5f);
    }
}
