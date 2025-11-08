using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private int scoreReduceAmount;
    public int score = 1000;
    [SerializeField] TMP_Text scoreText;
    public bool countdown = true;

    void Start()
    {
        StartCoroutine(StartReducing());
    }

    // Reduce score every 10th of a second
    IEnumerator StartReducing()
    {
        while (countdown)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            score -= scoreReduceAmount;
            scoreText.text = "Score: " + score;
        }
    }

    public void IncreaseScore(int increaseAmount)
    {
        score += increaseAmount;
        scoreText.text = "Score: " + score;
    }
}
