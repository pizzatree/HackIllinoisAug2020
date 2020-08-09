using UnityEngine;

public class Divide : iQuestion
{
    int x = 0, y = 0, z;

    public bool Answer(string input)
    {
        if(!int.TryParse(input, out int result))
            return false;
        return result == x;
    }

    public string Question(int difficulty)
    {
        int lower = 0, upper = 10;

#pragma warning disable CS0642
        if(difficulty <= 2)
            ;
#pragma warning restore CS0642

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

        while(x == 0)
            x = Random.Range(lower, upper);
        while(y == 0)
            y = Random.Range(lower, upper);

        z = x * y;

        return z + "\n"
              + "/ " + y;
    }
}
