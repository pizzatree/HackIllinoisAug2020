using System.Collections;
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

#pragma warning disable CS0642
        if(difficulty <= 2)
            ;
#pragma warning restore CS0642
        else if(difficulty <= 4)
            upper = 12;
        else if(difficulty <= 10)
            upper = 15;
        else if(difficulty <= 15)
            upper = 25;
        else if(difficulty >= 25)
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
