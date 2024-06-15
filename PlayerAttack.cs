using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu mũi tên chạm vào đối tượng có tag "Ground"
        if (collision.gameObject.CompareTag("ground"))
        {
            Destroy(gameObject);
        }

        // Kiểm tra nếu mũi tên chạm vào đối tượng có tag "Monster"
        if (collision.gameObject.CompareTag("Monster"))
        {
            Destroy(collision.gameObject); // Destroy the monster
            Destroy(gameObject); // Destroy the arrow
        }
    }
}