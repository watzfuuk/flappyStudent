using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class BirdController : MonoBehaviour
{
    [Header("Налаштування фізики")]
    public float jumpForce = 5f;
    public Rigidbody2D rb;
    public Collider2D myCollider; // Змінна для колайдера

    [Header("Налаштування вигляду")]
    public Sprite birdUpSprite;
    public Sprite birdDownSprite;
    public SpriteRenderer spriteRenderer;

    public static bool blockInput = false;

    private bool isDead = false;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (myCollider == null) myCollider = GetComponent<Collider2D>();

        blockInput = false;
    }

    void Update()
    {
        if (isDead || Time.timeScale == 0 || blockInput) return;
        if (IsPointerOverUI()) return;

        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        if (!isDead)
        {
            float angle = Mathf.Clamp(rb.linearVelocity.y * 10f, -90f, 30f);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    } // <--- ПЕРЕВІР, ЩО ТУТ Є ДУЖКА (Закриває Update)

    void Jump()
    {
        rb.linearVelocity = Vector2.up * jumpForce;
        if (spriteRenderer != null && birdDownSprite != null)
        {
            StartCoroutine(FlapWings());
        }
    }

    IEnumerator FlapWings()
    {
        spriteRenderer.sprite = birdDownSprite;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = birdUpSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;
        Die();
    }

    void Die()
    {
        isDead = true;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        if (myCollider != null)
        {
            myCollider.enabled = false;
        }

        StartCoroutine(ShowMenuDelay());
    }

    IEnumerator ShowMenuDelay()
    {
        yield return new WaitForSeconds(2.5f);

        var manager = FindFirstObjectByType<GameManager>();
        if (manager != null)
        {
            manager.ShowGameOver();
        }
    }

    private bool IsPointerOverUI()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
} 