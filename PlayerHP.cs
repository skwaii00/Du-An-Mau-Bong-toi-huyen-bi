using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealController : MonoBehaviour
{
    public Slider slider;

    // Scene name for the Game Over scene
    public string gameOverMenu = "GameOverMenu";

    private void Start()
    {
        slider.value = 10f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Monster"))
        {
            Debug.Log("Trap");
            DecreaseSliderValue(1);
        }

    }


    void Update()
    {
        // Check if slider value reaches 0
        if (slider.value <= 0)
        {
            // Load the Game Over scene
            SceneManager.LoadScene(gameOverMenu);
        }
    }

    // Decrease slider value by specified amount
    private void DecreaseSliderValue(float amount)
    {
        slider.value = Mathf.Max(slider.value - amount, 0f);
    }
}