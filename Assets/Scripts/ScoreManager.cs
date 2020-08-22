﻿using System.Collections;
using System.Collections.Generic;
using Bases;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Inst;

    public int NumSuccessful { get; private set; } = 0;

    private TextMeshProUGUI scoreUI;
    private ulong           currentScore = 0;

    private void Awake()
    {
        if(Inst != null && Inst != this)
        {
            Destroy(this);
            return;
        }

        Inst = this;
        DontDestroyOnLoad(this);
    }

    public void ResetScore()
    {
        currentScore  = 0;
        NumSuccessful = 0;
    }

    public void AddScore(int amount)
    {
        currentScore += (ulong) amount;
        ++NumSuccessful;

        if(scoreUI == null)
            scoreUI = GameObject.FindGameObjectWithTag("ScoreUI").GetComponent<TextMeshProUGUI>();
        scoreUI.text = currentScore.ToString();

        if(currentScore % Constants.POINTS_TO_REVIVE == 0)
            BaseManager.Inst?.RestoreBases();
    }

    public void PostScore()
    {
        if(GetHighScore() < currentScore)
        {
            PlayerPrefs.SetString("HighScore", currentScore.ToString());
            PlayerPrefs.Save();
        }
    }

    public ulong GetHighScore()
    {
        var scoreString = PlayerPrefs.GetString("HighScore");
        ulong.TryParse(scoreString, out ulong curHigh);

        return curHigh;
    }
}