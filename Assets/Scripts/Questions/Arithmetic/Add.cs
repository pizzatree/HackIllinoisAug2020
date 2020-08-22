using UnityEngine;

namespace Questions.Arithmetic
{
    public class Add : IQuestion
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
            var lower = difficulty       / 10;
            var upper = (difficulty + 1) * 5;

            x = Random.Range(lower, upper);
            y = Random.Range(lower, upper);

            z = x + y;

            return x               + "\n"
                            + "+ " + y;
        }
    }
}