using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] float fallThreshold = -30f; // Giá trị y mà khi player rơi xuống dưới, sẽ chuyển đến GameOverScene

    void Update()
    {
        CheckFall();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("nextlevel"))
        {
            LoadNextScene();
        }
        else if (collision.gameObject.CompareTag("Cup"))
        {
            LoadYouWinScene();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("nextlevel"))
        {
            LoadNextScene();
        }
        else if (other.CompareTag("Cup"))
        {
            LoadYouWinScene();
        }
    }

    void CheckFall()
    {
        if (transform.position.y < fallThreshold)
        {
            LoadGameOverScene();
        }
    }

    void LoadNextScene()
    {
        // Load scene có index cao hơn hiện tại
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    void LoadYouWinScene()
    {
        // Load scene "YouWin"
        SceneManager.LoadScene("YouWin");
    }

    void LoadGameOverScene()
    {
        // Load scene "GameOver"
        SceneManager.LoadScene("GameOverMenu");
    }
}