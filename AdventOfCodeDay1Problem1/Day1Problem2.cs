using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day1Problem2 : IProblem
    {
        private List<string> input;
        private Dictionary<string, int> dictionary;

        public string ID()
        {
            return "Day 1 Problem 2";
        }
        public Day1Problem2()
        {
            this.input = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "../../../inputs/Problem1Input.txt")).ToList();

            dictionary = new Dictionary<string, int>
            {
                {"one", 1 },
                {"two", 2 },
                {"three", 3 },
                {"four", 4 },
                {"five", 5 },
                {"six", 6 },
                {"seven", 7 },
                {"eight", 8 },
                {"nine", 9 }
            };
        }

        private int GetSum(List<string> list)
        {
            int sum = 0;
            foreach (string s in list)
            {
                var translatedString = TranslateString(s);
                sum += GetNumber(translatedString);
            }

            return sum;
        }

        private string TranslateString(string word)
        {
            var result = "";
            foreach (char c in word)
            {
                result += c;
                foreach (string s in dictionary.Keys.ToList())
                {
                    if (result.Contains(s))
                    {
                        result = result.Replace(s, dictionary[s].ToString()) + c;
                    }
                }
            }
            return result;
        }

        private int GetNumber(string word)
        {
            int number = 0;
            int buffer = -1;
            for (int x = 0; x < word.Length; x++)
            {
                var numberX = Convert.ToInt16(char.GetNumericValue(word[x]));
                if (numberX != -1)
                {
                    if (number != 0)
                    {
                        buffer = numberX;
                    }
                    else
                    {
                        number += numberX;
                    }
                }
            }

            if (buffer != -1)
            {
                number = number * 10 + buffer;
            }
            else
            {
                number = number + number * 10;
            }

            return number;
        }

        public string SolveProblem()
        {
            return GetSum(input).ToString();
        }
    }
}
