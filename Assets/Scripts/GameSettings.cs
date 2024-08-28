using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour 
{
    [SerializeField] private const int FIELD_HEIGHT = 13;
    [SerializeField] private const int FIELD_WIDTH = 13;
    public float playerMaxSpeed = 200.0f;
    public float cameraScrollSpeed = 200.0f;
}
