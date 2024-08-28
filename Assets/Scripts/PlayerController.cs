using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameSettings settings;

    private float maxSpeed;
    private Rigidbody2D rb;

    void Start()
    {
        settings = GameObject.Find("GameSettings").GetComponent<GameSettings>();
        maxSpeed = settings.playerMaxSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, Input.GetAxis("Vertical") * maxSpeed);
    }
}
