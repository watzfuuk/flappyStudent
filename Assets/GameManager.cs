using UnityEngine;
using UnityEngine.SceneManagement; // Обов'язково для перезапуску
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Текст очок у грі")]
    public TextMeshProUGUI scoreText;

    [Header("Меню Game Over")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText; // Текст фінального рахунку

    private int score = 0;

    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    // Цей метод викликається через 1.5 сек після смерті
    public void ShowGameOver()
    {
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > savedHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
        if (finalScoreText != null)
        {
            finalScoreText.text = "Score: " + score.ToString();
        }

        gameOverPanel.SetActive(true);

        if (scoreText != null) scoreText.gameObject.SetActive(false);

        Time.timeScale = 0f;
    }

    // Метод для кнопки Restart
    public void RestartGame()
    {
        // 1. Обов'язково розморожуємо час перед перезапуском!
        Time.timeScale = 1f;

        // 2. Перезавантажуємо поточну сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Метод для кнопки Menu (якщо треба)
    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }
}