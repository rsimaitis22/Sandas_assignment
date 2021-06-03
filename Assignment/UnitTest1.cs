using NUnit.Framework;
using Sandas_assignment;

namespace Assignment
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }
        [Test]
        public void ExampleOne()
        {
            string word = "abcabcbb";
            Assert.That(Program.LongestWord(word), Is.EqualTo(3).NoClip);
        }
        [Test]
        public void ExampleTwo()
        {
            string word = "bbbbb";
            Assert.That(Program.LongestWord(word), Is.EqualTo(1).NoClip);
        }

        [Test]
        public void ExampleThree()
        {
            string word = "pwwkew";
            Assert.That(Program.LongestWord(word), Is.EqualTo(3).NoClip);
        }
        [Test]
        public void ExampleFour()
        {
            string word = "";
            Assert.That(Program.LongestWord(word), Is.EqualTo(0).NoClip);
        }
    }
}