using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreView;
    private int scores = 0;

    void Start()
    {
        UpdateScoreView(); 
    }

    public void AddScore(int score)
    {
        scores += score;
        UpdateScoreView();
    }

    private void UpdateScoreView()
    {
        scoreView.text = "Score: " + scores;
    }
}
