using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform pointToView;
    [SerializeField] private Vector3 offset;
    private GameManager gameManager;
    private Vector3 speed;
    private Vector3 newPosition;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        newPosition = pointToView.position;
    }

    
    void Update()
    {
        pointToView.position = Vector3.SmoothDamp(pointToView.position, newPosition, ref speed, 0.5f);
        transform.position = pointToView.position + offset;
        transform.LookAt(pointToView);
    }

    public void SetPointToViewPosition(Vector3 position)
    {
        newPosition = new Vector3(pointToView.position.x, position.y, pointToView.position.z);
    }
}
