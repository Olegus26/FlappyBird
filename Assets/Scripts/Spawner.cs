using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Pipe Settings")]
    [SerializeField] private Pipes prefab;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private float minHeight = -1f;
    [SerializeField] private float maxHeight = 2f;
    [SerializeField] private float verticalGap = 3f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        if (prefab == null)
        {
            Debug.LogWarning("Pipe prefab is not assigned!");
            return;
        }

        Vector3 spawnPosition = transform.position + Vector3.up * Random.Range(minHeight, maxHeight);
        Pipes pipesInstance = Instantiate(prefab, spawnPosition, Quaternion.identity);

        pipesInstance.SetGap(verticalGap);
    }

    public void SetSpawnRate(float newRate)
    {
        if (newRate > 0f)
        {
            spawnRate = newRate;
            CancelInvoke(nameof(Spawn));
            InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
        }
        else
        {
            Debug.LogWarning("Spawn rate must be greater than zero.");
        }
    }

    public void SetVerticalGap(float newGap)
    {
        verticalGap = newGap;
    }
}
