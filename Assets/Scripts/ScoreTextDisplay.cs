using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextDisplay : MonoBehaviour
{
    private int score;

    private float timer = 0f;

    [SerializeField]
    private Text scoreText;

    [SerializeField] 
    private float duration = 1.5f;

    private void setScoreText()
    {
        scoreText.text = String.Format("Score: {0}", score);
    }

    private IEnumerator UpdateScore(int target)
    {
        int start = score;
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;

            score = (int) Mathf.Lerp(start, target, progress);
            setScoreText();
            yield return null;
        }

        score = target;
        setScoreText();
    }
    
    public void AddPoints(int points)
    {
        StartCoroutine(UpdateScore(score + points));
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
}
