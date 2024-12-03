using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOverPanel;

    private int score;

    private void Awake()
    {
        HandleSingleton();
    }

    private void Start()
    {
        PauseGame();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void HandleSingleton()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PauseGame()
    {
        SetGameState(false, 0f);
    }

    public void StartGame()
    {
        ResetGame();
        SetGameState(true, 1f);
        ClearPipes();
    }

    public void EndGame()
    {
        playButton.SetActive(true);
        gameOverPanel.SetActive(true);
        PauseGame();
    }

    public void IncreaseScore()
    {
        score++;
        UpdateScoreText();
    }

    private void ResetGame()
    {
        score = 0;
        UpdateScoreText();
        playButton.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    private void ClearPipes()
    {
        foreach (var pipe in FindObjectsOfType<Pipes>())
        {
            Destroy(pipe.gameObject);
        }
    }

    private void SetGameState(bool isActive, float timeScale)
    {
        player.enabled = isActive;
        Time.timeScale = timeScale;
    }
}
