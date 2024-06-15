using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Khởi tạo hướng di chuyển ban đầu
        moveDirection = Vector2.right;
    }

    void Update()
    {
        // Di chuyển theo hướng moveDirection với tốc độ moveSpeed
        rb.velocity = moveDirection * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HandlePlayerCollision();
        }
    }

    void HandlePlayerCollision()
    {
        // Xử lý khi Player chạm vào Monster
        // Ví dụ: thay đổi hướng di chuyển của Monster
        moveDirection *= -1; // Đổi hướng di chuyển
    }
}