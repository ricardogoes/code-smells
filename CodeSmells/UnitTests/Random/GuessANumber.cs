using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodeSmells.UnitTests.Random
{
    public class GuessANumber
    {
        private readonly int _maxNumber;

        private readonly IRandom _random;

        public GuessANumber(int maxNumber, IRandom random)
        {
            this._random = random;
            this._maxNumber = maxNumber;
        }

        public GuessANumber(int maxNumber) : this(maxNumber, new SystemRandom())
        {
        }

        public bool IsGuessCorrect(int guess)
        {
            var chosenNumber = _random.Next(_maxNumber+1);

            return guess == chosenNumber;
        }
    }

    public interface IRandom
    {
        int Next(int maxValue);
    }

    [TestFixture]
    public class GuessANumberIsGuessCorrectShould
    {
        [Test]
        public void ReturnTrueWhenGuessEqualsChosenNumber()
        {
            var testRandom = new TestRandom(1);
            var guessANumber = new GuessANumber(2, testRandom);

            var result = guessANumber.IsGuessCorrect(1);

            Assert.IsTrue(result, "Expected true when I guess 1 and the system chooses 1.");

        }
    }

    public class TestRandom : IRandom
{
        private readonly int _numberToReturn;

        public TestRandom(int numberToReturn)
        {
            this._numberToReturn = numberToReturn;
        }

        public int Next(int maxValue)
    {
        return _numberToReturn;
    }
}
}
