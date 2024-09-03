using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCameraController : MonoBehaviour
{
    private GameSettings settings;
    private Camera playerCamera;

    void Start()
    {
        settings = GameObject.Find("GameSettings").GetComponent<GameSettings>();
        playerCamera = Camera.main;    
    }

    void Update()
    {
        var scroll = Input.mouseScrollDelta.y * Time.deltaTime * settings.cameraScrollSpeed;

        if (playerCamera.orthographicSize > 0)
        {
            playerCamera.orthographicSize -= scroll;
        }
        else
        {
            playerCamera.orthographicSize += scroll;
        }
    }
}
