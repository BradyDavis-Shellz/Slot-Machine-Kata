using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextDisplay : MonoBehaviour
{
    private int score = 0;

    [SerializeField]
    private Text scoreText;

    private void setScoreText()
    {
        scoreText.text = String.Format("Score: {0}", score);
    }
    
    void Start()
    {
        if (!scoreText)
        {
            scoreText = GetComponent<Text>();
        }

        if (!scoreText)
        {
            Debug.LogError(String.Format("No Text component found on {0}", gameObject.name));
        }
        setScoreText();
    }

    public void AddPoints(int points)
    {
        score += points;
        setScoreText();
    }
}
