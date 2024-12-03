using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Parallax Settings")]
    [SerializeField] private float animationSpeed = 1f;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (meshRenderer != null)
        {
            Vector2 offset = meshRenderer.material.mainTextureOffset;
            offset.x += animationSpeed * Time.deltaTime;
            meshRenderer.material.mainTextureOffset = offset;
        }
    }

    public void SetAnimationSpeed(float newSpeed)
    {
        if (newSpeed >= 0f)
        {
            animationSpeed = newSpeed;
        }
        else
        {
            Debug.LogWarning("Animation speed must be non-negative.");
        }
    }
}
