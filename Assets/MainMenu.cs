using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Для тексту

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText; // Сюди перетягнеш текст рекорду

    void Start()
    {
        // При запуску меню читаємо рекорд з пам'яті
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Виводимо його на екран
        bestScoreText.text = "Best Score: " + highScore.ToString();
    }

    public void PlayGame()
    {
        // Завантажуємо ігрову сцену
        // ВАЖЛИВО: Перевір, щоб ім'я сцени тут співпадало з твоєю (наприклад "SampleScene")
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Debug.Log("Вихід з програми!");
        Application.Quit();
    }
}