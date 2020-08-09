using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Inst;

    private TextMeshProUGUI scoreUI;

    private int currentScore = 0;

    private void Awake()
    {
        if(Inst != null)
            Destroy(this);

        Inst = this;
        DontDestroyOnLoad(this);
    }

    public void AddScore(int amount)
    {
        currentScore += amount;

        if(scoreUI == null)
            scoreUI = GameObject.FindGameObjectWithTag("ScoreUI").GetComponent<TextMeshProUGUI>();
        scoreUI.text = currentScore.ToString();
    }
}
