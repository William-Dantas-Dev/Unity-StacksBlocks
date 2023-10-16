using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Menu TMPro")]
    [SerializeField] private TextMeshProUGUI startGameText;
    [SerializeField] private TextMeshProUGUI restartGameText;

    [Header("Spawn Block")]
    public MovingCube lastCube;
    public MovingCube currentCube;
    public GameObject startCube;

    public GameObject movingCube;
    private bool direction; // false = X : true = Z
    private CameraController cameraController;

    public TextMeshProUGUI scoreText;
    private int score;
    private bool endGame;

    private void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
    }

    
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!endGame)
            {
                if (currentCube.gameObject != startCube)
                {
                    if (currentCube.Stop(lastCube.transform))
                    {
                        restartGameText.gameObject.SetActive(true);
                        endGame = true;
                        return;
                    }
                    score++;
                    scoreText.text = score.ToString();
                    lastCube = currentCube;
                }
                else
                {
                    startGameText.gameObject.SetActive(false);
                }
                Vector3 pointToSpawn = GetNextPointToSpawn();
                Vector3 lastCubeScale = lastCube.transform.localScale;
                InstantiateBlock(lastCubeScale, pointToSpawn, direction);
                direction = !direction;
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void InstantiateBlock(Vector3 lastCubeScale, Vector3 pointToSpawn, bool moveDirection)
    {
        float[] distanceLimit = new float[2];
        if (moveDirection)
        {
            distanceLimit[0] = lastCube.transform.position.z - 1.5f;
            distanceLimit[1] = lastCube.transform.position.z + 1.5f;
            pointToSpawn.z -= distanceLimit[0];
        }
        else
        {
            distanceLimit[0] = lastCube.transform.position.x - 1.5f;
            distanceLimit[1] = lastCube.transform.position.x + 1.5f;
            pointToSpawn.x -= distanceLimit[0];
        }
        currentCube = Instantiate(movingCube, pointToSpawn, Quaternion.identity).GetComponent<MovingCube>();
        currentCube.transform.localScale = lastCubeScale;
        currentCube.moveDirection = moveDirection;
        currentCube.positionLimit = distanceLimit;
    }

    private Vector3 GetNextPointToSpawn()
    {
        Vector3 pointToSpawn = lastCube.transform.position;
        pointToSpawn.y += 0.1f;
        cameraController.SetPointToViewPosition(pointToSpawn);
        return pointToSpawn;
    }
}
