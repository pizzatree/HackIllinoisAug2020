namespace Questions
{
    public interface IQuestion
    {
        string Question(int difficulty);
        bool   Answer(string input);
    }
}