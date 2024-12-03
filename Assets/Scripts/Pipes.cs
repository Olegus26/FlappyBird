using UnityEngine;

public class Pipes : MonoBehaviour
{
    [Header("Pipe Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gap = 3f;

    [Header("Pipe References")]
    [SerializeField] private Transform top;
    [SerializeField] private Transform bottom;

    private float leftEdge;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;

        AdjustPipePositions();
    }

    private void Update()
    {
        MovePipes();
        CheckPipeOutOfBounds();
    }

    private void AdjustPipePositions()
    {
        if (top != null && bottom != null)
        {
            top.position += Vector3.up * gap / 2;
            bottom.position += Vector3.down * gap / 2;
        }
        else
        {
            Debug.LogWarning("Top or Bottom pipe reference is missing!");
        }
    }

    private void MovePipes()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    private void CheckPipeOutOfBounds()
    {
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }

    public void SetGap(float newGap)
    {
        gap = newGap;
        AdjustPipePositions();
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
