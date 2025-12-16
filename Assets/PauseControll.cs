using System.Collections; // Потрібно для роботи Coroutine (затримки)
using UnityEngine;
using UnityEngine.SceneManagement; // Потрібно для перемикання сцен

public class PauseControll : MonoBehaviour
{
    [Header("Налаштування")]
    [Tooltip("Сюди перетягни панель PausePanel зі сцени")]
    [SerializeField] GameObject pausePanel;

    // Змінна, щоб знати, чи стоїть гра на паузі
    private bool isPaused = false;

    void Start()
    {
        // На старті гри гарантовано ховаємо меню і запускаємо час
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Update()
    {
        // Натискання ESC на комп'ютері або кнопки "BACK" на телефоні
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Метод для кнопки "Pause" (||)
    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true); // Показуємо меню
        Time.timeScale = 0f; // Зупиняємо час (фізика завмирає)
    }

    // Метод для кнопки "Resume" (Продовжити)
    public void ResumeGame()
    {
        // Запускаємо процедуру відновлення з затримкою
        StartCoroutine(ResumeRoutine());
    }

    // Спеціальний метод із затримкою
    IEnumerator ResumeRoutine()
    {
        // 1. Спочатку ховаємо меню, щоб гравець бачив гру
        pausePanel.SetActive(false);

        // 2. Блокуємо стрибок пташки (звертаємось до змінної в BirdController)
        // Це потрібно, щоб палець, який натиснув кнопку, не змусив пташку стрибнути
        BirdController.blockInput = true;

        // 3. Відновлюємо плин часу
        Time.timeScale = 1f;
        isPaused = false;

        // 4. Чекаємо 0.2 секунди (достатньо, щоб палець піднявся з екрану)
        yield return new WaitForSeconds(0.2f);

        // 5. Знову дозволяємо пташці стрибати
        BirdController.blockInput = false;
    }

    // Метод для кнопки "Menu" (Вихід)
    public void LoadMainMenu()
    {
        // Обов'язково повертаємо час у норму перед виходом,
        // інакше в головному меню анімації можуть не працювати
        Time.timeScale = 1f;

        // ВАЖЛИВО: Перевір, щоб назва сцени тут співпадала з твоєю
        SceneManager.LoadScene("MenuScene");
    }
}