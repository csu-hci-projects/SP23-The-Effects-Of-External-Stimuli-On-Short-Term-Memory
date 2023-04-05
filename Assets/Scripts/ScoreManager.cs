using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI roundScore;
    public TextMeshProUGUI highRoundScore;

    public int score = 0;
    public int highScore = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore", 0);
        roundScore.text = "SCORE: " + score.ToString();
        highRoundScore.text = "HIGHSCORE: " + highScore.ToString();
    }

    // Update is called once per frame
    public void addPoint()
    {
        score += 1;
        roundScore.text = "SCORE: " + score.ToString();
    }

    public void clearPoints()
    {
        score = 0;
        roundScore.text = "SCORE: " + score.ToString();

        if (highScore < score)
            PlayerPrefs.SetInt("highscore", score);
    }
}
