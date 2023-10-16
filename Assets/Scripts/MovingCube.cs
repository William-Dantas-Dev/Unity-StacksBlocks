using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MovingCube : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 1f;
    public float[] positionLimit;
    public bool moveDirection; // false = X : true = Z
    public bool currentDirection;
    private Color color;
    
    private void Start()
    {
        color = GetRandomColor();
        GetComponent<Renderer>().material.color = color;
    }

    private void Update()
    {
        if (moveDirection)
        {
            MoveBlockZ();
        }
        else
        {
            MoveBlockX();
        }
        
    }
    private void MoveBlockZ()
    {
        if (currentDirection)
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
            if (transform.position.z > positionLimit[1])
            {
                currentDirection = !currentDirection;
            }
        }
        else
        {
            transform.position -= transform.forward * Time.deltaTime * moveSpeed;
            if (transform.position.z < positionLimit[0])
            {
                currentDirection = !currentDirection;
            }
        }
    }
    private void MoveBlockX()
    {
        if (currentDirection)
        {
            transform.position += transform.right * Time.deltaTime * moveSpeed;
            if (transform.position.x > positionLimit[1])
            {
                currentDirection = !currentDirection;
            }
        }
        else
        {
            transform.position -= transform.right * Time.deltaTime * moveSpeed;
            if (transform.position.x < positionLimit[0])
            {
                currentDirection = !currentDirection;
            }
        }
    }

    private Color GetRandomColor()
    {
        return new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

    public bool Stop(Transform lastCube)
    {
        moveSpeed = 0f;
        float hangover = GetHangover(lastCube);
        if(CheckIfLose(hangover, lastCube))
        {
            gameObject.AddComponent<Rigidbody>();
            return true;
        }
        SplitCube(hangover, lastCube);
        return false;
    }
    private bool CheckIfLose(float hangover, Transform lastCube)
    {
        float max = moveDirection ? lastCube.transform.localScale.z : lastCube.transform.localScale.x;
        return Mathf.Abs(hangover) >= max;
    }

    private float GetHangover(Transform lastCube)
    {
        if (moveDirection)
        {
            return transform.position.z - lastCube.position.z;
        }
        else
        {
            return transform.position.x - lastCube.position.x;
        }
    }

    private void SplitCube(float hangover, Transform lastCube)
    {
        float direction = hangover > 0 ? 1f : -1f;

        if (moveDirection)
        {
            SplitCubeOnZ(hangover, direction, lastCube);
        }
        else
        {
            SplitCubeOnX(hangover, direction, lastCube);
        }
    }

    private void SplitCubeOnZ(float hangover, float direction, Transform lastCube)
    {
        float newZSize = lastCube.localScale.z - Mathf.Abs(hangover);
        float fallingBlockZSize = transform.localScale.z - newZSize;

        float newZPosition = lastCube.position.z + (hangover / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2f * direction);
        float fallingBlockZPosition = cubeEdge + fallingBlockZSize / 2f * direction;

        SpawnDropCube(fallingBlockZPosition, fallingBlockZSize);
    }

    private void SplitCubeOnX(float hangover, float direction, Transform lastCube)
    {
        float newXSize = lastCube.localScale.x - Mathf.Abs(hangover);
        float fallingBlockXSize = transform.localScale.x - newXSize;

        float newXPosition = lastCube.position.x + (hangover / 2);
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newXSize / 2f * direction);
        float fallingBlockXPosition = cubeEdge + fallingBlockXSize / 2f * direction;

        SpawnDropCube(fallingBlockXPosition, fallingBlockXSize);
    }

    private void SpawnDropCube(float fallingBlockPosition, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (moveDirection)
        {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockPosition);
        }
        else
        {
            cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingBlockPosition, transform.position.y, transform.position.z);
        }

        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = color;
    }
}


