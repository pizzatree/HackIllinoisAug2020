﻿using UnityEngine;

namespace Questions.Arithmetic
{
    public class Subtract : IQuestion
    {
        private int x, y, z;

        public bool Answer(string input)
        {
            if(!int.TryParse(input, out int result))
                return false;
            return result == z;
        }

        public string Question(int difficulty)
        {
            var lower = 1 + difficulty / 10;
            var upper = (difficulty + 1) * 5;

            x = Random.Range(lower, upper);
            y = Random.Range(lower, upper);

            while(x == y)
                y = Random.Range(lower, upper);

            if(y > x && difficulty < 100) // avoid negatives until difficulty > 100
            {
                z = x;
                x = y;
                y = z;
            }

            z = x - y;

            return x               + "\n"
                            + "- " + y;
        }
    }
}