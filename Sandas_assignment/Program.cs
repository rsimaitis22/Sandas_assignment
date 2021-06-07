using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandas_assignment
{

    public class Program
    {
        static void Main(string[] args)
        {
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
            int maxConstonantLength = 3;
            string currentStr = "";
            string maxStr = "";
            var inputChars = input.ToCharArray();
            char[] vowels = new char[] { 'a', 'e', 'y', 'u', 'i', 'o' };

            List<char> usedChars = new List<char>();

            bool isVowel = false;
            int constonant = 0;

            for (int i = 0; i < inputChars.Length; i++)
            {
                for (int count = i; count < inputChars.Length; count++)
                {
                    isVowel = false;
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
                            constonant = 0;
                            isVowel = true;
                            currentStr = $"{currentStr}{inputChars[count]}";
                            if (currentStr.Length > maxStr.Length)
                                maxStr = currentStr;
                            break;
                        }
                    }
                    if(!isVowel)
                    {
                        constonant++;
                        if (constonant > maxConstonantLength)
                        {
                            usedChars.Clear();
                            currentStr = "";
                            constonant = 0;
                            break;
                        }
                        else
                        {
                            currentStr = $"{currentStr}{inputChars[count]}";
                            if (currentStr.Length > maxStr.Length)
                                maxStr = currentStr;
                        }
                    }
                }
            }

            return maxStr.Length;
        }
    }
}
