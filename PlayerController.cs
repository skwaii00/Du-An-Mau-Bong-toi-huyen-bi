using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] int maxJumps = 2; // Số lần nhảy tối đa (1 lần nhảy đôi)
    [SerializeField] GameObject arrowPrefab; // Prefab cho mũi tên
    [SerializeField] Transform arrowSpawnPoint; // Vị trí xuất hiện của mũi tên
    [SerializeField] float arrowSpeed = 10f; // Tốc độ của mũi tên
    [SerializeField] Transform spawnPoint; // Điểm xuất phát của nhân vật

    Vector2 moveInput;
    Rigidbody2D myRi;
    Animator myA;
    bool isGrounded;
    int jumpCount; // Đếm số lần nhảy

    void Start()
    {
        myRi = GetComponent<Rigidbody2D>();
        myA = GetComponent<Animator>();
        transform.position = spawnPoint.position; // Đặt nhân vật tại điểm xuất phát ban đầu
    }

    void Update()
    {
        Run();
        FlipSprite();
        CheckGrounded();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && (isGrounded || jumpCount < maxJumps))
        {
            Jump();
        }
    }

    void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            ShootArrow();
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRi.velocity.y);
        myRi.velocity = playerVelocity;
        bool hasHori = Mathf.Abs(myRi.velocity.x) > Mathf.Epsilon;
        myA.SetBool("isRunning", hasHori);
    }

    void FlipSprite()
    {
        bool hasHori = Mathf.Abs(myRi.velocity.x) > Mathf.Epsilon;
        if (hasHori)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRi.velocity.x), 1f);
        }
    }

    void Jump()
    {
        if (jumpCount == 0)
        {

            myA.SetBool("isDoubleJump", false);
        }
        else if (jumpCount == 1)
        {
            myA.SetBool("isDoubleJump", true);
        }

        myRi.velocity = new Vector2(myRi.velocity.x, jumpForce);
        jumpCount++;

    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (isGrounded)
        {
            jumpCount = 0;
            myA.SetBool("isGrounded", true);
            myA.SetBool("isDoubleJump", false);
        }
        else
        {
            myA.SetBool("isGrounded", false);
        }

        myA.SetBool("isGrounded", isGrounded);

    }

    void ShootArrow()
    {
        myA.SetTrigger("PlayerAttack");

        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);
        Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();

        float direction = Mathf.Sign(transform.localScale.x);
        arrowRb.velocity = new Vector2(direction * arrowSpeed, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            transform.position = spawnPoint.position; // Quay lại vị trí xuất phát
            myRi.velocity = Vector2.zero; // Reset vận tốc
            Debug.Log("Player hit a monster and returned to spawn point.");
        }
    }
}