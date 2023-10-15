using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private Rigidbody2D playerRigidbody;
    private Vector3 moveDir;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();

        playerRigidbody.angularVelocity = 0;
    }

    [SerializeField]
    private float baseSpeed;

    private void HandleMovement()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveX = +1f;
        }

        moveDir = new Vector3(moveX, moveY).normalized;
    }

    private void Update()
    {
        HandleMovement();
    }
    void FixedUpdate()
    {
        playerRigidbody.MovePosition(transform.position + moveDir * baseSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger!");
    }
}
