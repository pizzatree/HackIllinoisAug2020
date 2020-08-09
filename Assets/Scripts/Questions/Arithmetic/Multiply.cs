﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiply : iQuestion
{
    int x, y, z;

    public bool Answer(string input)
    {
        if(!int.TryParse(input, out int result))
            return false;
        return result == z;
    }

    public string Question(int difficulty)
    {
        int lower = 0, upper = 10;

        if(difficulty <= 2)
            ;
        else if(difficulty <= 4)
        {
            lower = -12;
            upper = 12;
        }
        else if(difficulty <= 6)
        {
            lower = -15;
            upper = 15;
        }
        else if(difficulty >= 8)
        {
            lower = -25;
            upper = 25;
        }

        x = Random.Range(lower, upper);
        y = Random.Range(lower, upper);

        z = x * y;

        return x + "\n"
              + "* " + y;
    }
}