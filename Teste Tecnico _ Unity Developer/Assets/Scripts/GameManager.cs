using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI scoreText;

    public GameObject liveXImg01;
    public GameObject liveXImg02;
    public int lifesCount;

    public GameObject gameOverPanel;

    public int score = 0;
    //private int bubblesRemaining = 10;

    void Awake()
    {
        instance = this;
        Time.timeScale = 1.0f;
    }

    void Start()
    {
        UpdateUI();
    }

    public void Update()
    {
        UpdateUI();

        if (lifesCount == 1)
        {
            liveXImg01.SetActive(true);
        }
        if (lifesCount == 2)
        {
            liveXImg02.SetActive(true);
            GameOver();
        }
    }
    //public void OnBubbleLaunched()
    //{
    //    bubblesRemaining--;
    //    UpdateUI();

    //    if (bubblesRemaining <= 0)
    //    {
    //        GameOver();
    //    }
    //}

    //public void AddScore(int amount)
    //{
    //    score += amount;
    //    UpdateUI();
    //}

    void UpdateUI()
    {
        scoreText.text = "Pontos: " + score;
        //bubbleCountText.text = "Bolhas: " + bubblesRemaining;
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        FindFirstObjectByType<SlingShot>().isGameOver = true;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}
