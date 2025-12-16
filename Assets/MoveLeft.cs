using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        // Рухаємо об'єкт вліво
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Видаляємо трубу, якщо вона далеко зліва (оптимізація пам'яті для Android)
        if (transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }
}