using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public float score;
    public TextMeshProUGUI scoreText; // Assign this in the Inspector

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Update()
    {
        if(score == 20)
        {
            SceneManager.LoadScene("Scene17");
        }
    }

    public void AddScore(float value)
    {
        score += value;
        UpdateScoreUI();
    }

    public void SubtractScore(float value)
    {
        score -= value;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString("F2");
        }
    }
}
