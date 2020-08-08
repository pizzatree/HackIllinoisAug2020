using UnityEngine;

public class Subtract : iQuestion
{
    private int x, y, z;

    public bool Answer(string input)
    {
        return int.Parse(input) == z;
    }

    public string Question(int difficulty)
    {
        int lower = 0, upper = 10;

        if(difficulty <= 2)
            ;
        else if(difficulty <= 4)
            upper = 100;
        else if(difficulty <= 6)
            upper = 1000;
        else if(difficulty >= 8)
        {
            lower = 100;
            upper = 1000;
        }

        x = Random.Range(lower, upper);
        y = Random.Range(lower, upper);

        z = x - y;

        return x + "\n"
              + "- " + y;
    }
}
