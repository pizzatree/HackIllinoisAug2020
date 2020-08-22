using UnityEngine;

namespace Questions.Arithmetic
{
    public class Multiply : IQuestion
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
            var lower = 2 + (difficulty / 10);
            var upper = 5 + (difficulty / 5);

            x = Random.Range(lower, upper);
            y = Random.Range(lower, upper);

            z = x * y;

            return x               + "\n"
                            + "* " + y;
        }
    }
}