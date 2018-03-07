namespace CodeSmells.UnitTests.Random
{
    public class SystemRandom : IRandom
    {
        public int Next(int maxValue)
        {
            return new System.Random().Next(maxValue);
        }
    }
}