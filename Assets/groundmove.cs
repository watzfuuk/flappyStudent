using UnityEngine;

public class groundmove : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    public float speed = 1f; // Швидкість фону (має бути менша за труби)

    void Start()
    {
        // Запам'ятовуємо, де фон стояв на початку
        startPos = transform.position;

        // Вимірюємо половину ширини картинки через колайдер
        repeatWidth = GetComponent<BoxCollider2D>().size.x / 1;
    }

    void Update()
    {
        // Якщо гра на паузі - не рухаємось
        if (Time.timeScale == 0) return;

        // Рухаємо фон вліво
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Якщо фон змістився вліво на половину своєї ширини
        if (transform.position.x < startPos.x - repeatWidth)
        {
            // Телепортуємо його назад на стартову позицію
            transform.position = startPos;
        }
    }
}
