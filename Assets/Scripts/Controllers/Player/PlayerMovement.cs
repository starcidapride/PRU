using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
    [SerializeField]
    private LayerMask platform;
    [SerializeField]
    private float baseSpeed;
    [SerializeField]
    private float jumpVelocity;

    private Rigidbody2D playerRigidbody;
    private Vector3 moveDir;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();

        playerRigidbody.angularVelocity = 0;

        animator = GetComponent<Animator>();

        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        Move();
    }

    private bool IsGrounded()
    {
        var rayCastHit2D = Physics2D.BoxCast(
            capsuleCollider.bounds.center,
            capsuleCollider.bounds.size,
            0f,
            Vector2.down * 0.1f,
            1f,
            platform
            );
        Debug.Log(rayCastHit2D.collider);
        return rayCastHit2D.collider != null;
    }

    private void Move()
    {
        
        bool crouch = false;

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidbody.velocity = Vector2.up * jumpVelocity;
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            crouch = true;
        }

        var moveX = Input.GetAxisRaw("Horizontal");

        var velocityY = playerRigidbody.velocity.y;

        playerRigidbody.velocity = new Vector2(moveX * baseSpeed, velocityY);

        PlayerAnimation.Instance.PlayMovingAnimation(moveX, !IsGrounded(), crouch);
    }
}
