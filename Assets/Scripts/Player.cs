using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float jumpStrength = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float tiltFactor = 5f;
    [SerializeField] private float animationInterval = 0.15f;

    private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    private int spriteIndex;
    private bool isAlive = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        ResetPlayerPosition();
        ResetDirection();
        isAlive = true;
        InvokeRepeating(nameof(AnimateSprite), animationInterval, animationInterval);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(AnimateSprite));
    }

    private void Update()
    {
        if (!isAlive) return;

        HandleInput();
        ApplyGravity();
        UpdatePosition();
        UpdateRotation();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * jumpStrength;
        }
    }

    private void ApplyGravity()
    {
        direction.y += gravity * Time.deltaTime;
    }

    private void UpdatePosition()
    {
        transform.position += direction * Time.deltaTime;
    }

    private void UpdateRotation()
    {
        float tilt = Mathf.Clamp(direction.y * tiltFactor, -90f, 45f);
        transform.eulerAngles = new Vector3(0, 0, tilt);
    }

    private void AnimateSprite()
    {
        if (sprites.Length == 0) return;

        spriteIndex = (spriteIndex + 1) % sprites.Length;
        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void ResetPlayerPosition()
    {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }

    private void ResetDirection()
    {
        direction = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            HandleGameOver();
        }
        else if (collision.CompareTag("Scoring"))
        {
            GameManager.Instance.IncreaseScore();
        }
    }

    private void HandleGameOver()
    {
        isAlive = false;
        GameManager.Instance.EndGame();
    }
}
