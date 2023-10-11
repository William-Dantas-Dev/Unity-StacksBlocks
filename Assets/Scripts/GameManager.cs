using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CubeSpawner[] spawners;
    private CubeSpawner currentSpawner;
    private int spawnersIndex;
    public Transform pointToView;

    private void Awake()
    {
        spawners = FindObjectsOfType<CubeSpawner>();
    }


    private void Start()
    {
        
    }

    
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(MovingCube.CurrentCube != null)
            {
                MovingCube.CurrentCube.Stop();
            }
            pointToView.transform.position = new Vector3(pointToView.position.x, pointToView.position.y + 0.1f, pointToView.position.z);
            spawnersIndex = spawnersIndex == 0 ? 1 : 0;
            currentSpawner = spawners[spawnersIndex];
            currentSpawner.SpawnCube();
        }
    }
}
