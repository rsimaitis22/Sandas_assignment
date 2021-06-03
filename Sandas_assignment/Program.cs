using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandas_assignment
{
    public class Program
    {
        static void Main(string[] args)
        {
            //input select assignment 1 or 2
            LongestWord("bbbb");
            CSVService csvS = new CSVService();

            Console.WriteLine("Enter filename");

            string input;
            while ((input = Console.ReadLine()) != null)
            {
                var tmp = csvS.GetWorkerDataFromFile(input);
                if (tmp != null)
                {
                    csvS.WriteWorkerDataToFile("test1", tmp);
                    csvS.WriteSplitWorkerDataToFile("test2", tmp);
                }
            }
        }

        public static int LongestWord(string input)
        {
            int maxConstonantLength = 2;
            string currentStr = "";
            string maxStr = "";
            var inputChars = input.ToCharArray();
            char[] vowels = new char[] { 'a', 'e', 'y', 'u', 'i', 'o' };
            char[] constonants = new char[] { 'q', 'w', 'r', 't', 'p', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm' };
            List<char> usedChars = new List<char>();

            int constonant = 0;
            bool vowel = false;

            for (int i = 0; i < inputChars.Length; i++)
            {
                for (int count = i; count < inputChars.Length; count++)
                {
                    vowel = false;
                    if (usedChars.Contains(inputChars[count]))
                    {
                        usedChars.Clear();
                        currentStr = "";
                        constonant = 0;
                        break;
                    }

                    usedChars.Add(inputChars[count]);

                    foreach (var item in vowels)
                    {
                        if (item == inputChars[count])
                        {
                            currentStr = $"{currentStr}{inputChars[count]}";
                            vowel = true;
                        }
                    }
                    if (currentStr.Length > maxStr.Length)
                        maxStr = currentStr;

                    if (vowel)
                        break;
                    else if (constonant > 1)
                    {
                        usedChars.Clear();
                        currentStr = "";
                        constonant = 0;
                        break;
                    }
                    else
                    {
                        currentStr = $"{currentStr}{inputChars[count]}";
                        constonant++;
                        if (currentStr.Length > maxStr.Length)
                            maxStr = currentStr;
                    }
                }
            }

            return maxStr.Length;
        }
    }
}
