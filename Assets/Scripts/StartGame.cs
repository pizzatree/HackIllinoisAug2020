﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public void OnWithTheShow()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
