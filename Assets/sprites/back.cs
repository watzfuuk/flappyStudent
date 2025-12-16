using UnityEngine;

public class back : MonoBehaviour
{
    private float length; // Довжина однієї картинки
    public float speed = 1f;

    void Start()
    {
        // 1. Отримуємо ширину картинки через колайдер
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        // Враховуємо розтягнення (Scale)
        length = collider.size.x * transform.localScale.x;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        // 2. Рухаємо фон вліво
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // 3. ЯКЩО фон виїхав за екран вліво на свою повну довжину...
        if (transform.position.x < -length)
        {
            // ...перекидаємо його вперед на ДВІ довжини (стає в хвіст черги)
            // Використовуємо Vector3, щоб не збити висоту Y
            transform.position = new Vector3(transform.position.x + length * 2, transform.position.y, transform.position.z);
        }
    }
}