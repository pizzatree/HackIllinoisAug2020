using UnityEngine;

public class Add : iQuestion
{
    private int x, y, z;

    public bool Answer(string input)
    {
        string[] answer = input.Split('+');

        int inputX = int.Parse(answer[0]);
        int inputY = int.Parse(answer[1]);

        return inputX + inputY == z;
    }

    public string Question(int difficulty)
    {
        if(difficulty <= 0)
        {
            x = Random.Range(0, 10);
            y = Random.Range(0, 10);

            z = x + y;
        }
        else if(difficulty <= 2)
        {
            x = Random.Range(0, 100);
            y = Random.Range(0, 100);

            z = x + y;
        }
        else if(difficulty <= 4)
        {
            x = Random.Range(0, 1000);
            y = Random.Range(0, 1000);

            z = x + y;
        }
        else if(difficulty <= 8)
        {
            x = Random.Range(100, 1000);
            y = Random.Range(100, 1000);

            z = x + y;
        }

        return  " "+x+"\n"
              +"+   "+y;
    }
}
