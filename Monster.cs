using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] Transform leftBoundary;
    [SerializeField] Transform rightBoundary;

    private Rigidbody2D myRi;
    private bool movingRight = true;

    void Start()
    {
        myRi = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        CheckDirection();
    }

    void Move()
    {
        if (movingRight)
        {
            myRi.velocity = new Vector2(moveSpeed, myRi.velocity.y);
        }
        else
        {
            myRi.velocity = new Vector2(-moveSpeed, myRi.velocity.y);
        }
    }

    void CheckDirection()
    {
        if (transform.position.x >= rightBoundary.position.x)
        {
            movingRight = false;
            Flip();
        }
        else if (transform.position.x <= leftBoundary.position.x)
        {
            movingRight = true;
            Flip();
        }
    }

    void Flip()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
}