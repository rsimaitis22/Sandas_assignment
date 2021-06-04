using NUnit.Framework;
using Sandas_assignment;
using System.Collections.Generic;
using System.IO;

namespace Assignment
{
    public class Tests
    {
        CSVService csv;
        List<WorkerData> dataList;

        [SetUp]
        public void Setup()
        {
            csv = new CSVService();
            dataList = new List<WorkerData>();
        }
        [Test]
        public void TryAddWorkerDataFromWrongDataInputString()
        {
            dataList.Clear();

            string testStr = "First name, 0330, ";
            csv.TryAddWorkerDataFromString(testStr, dataList);

            Assert.That(dataList.Count, Is.EqualTo(0).NoClip);
        }
        [Test]
        public void TryReadingFromCorruptFile()
        {
            string testStr = "wrongFilename";

            Assert.That(csv.GetWorkerDataFromFile(testStr), Is.Null);
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