using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day1Problem1 : IProblem
    {
        private List<string> input;

        public string ID()
        {
            return "Day 1 Problem 1";
        }
        public Day1Problem1()
        {
            this.input = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "../../../inputs/Problem1Input.txt")).ToList();
        }

        private int GetSum(List<string> list)
        {
            int sum = 0;
            foreach (string s in list)
            {
                sum += GetNumber2(s);
            }

            return sum;
        }

        private int GetNumber(string word)
        {
            int number = 0;
            bool foundX = false;
            bool foundY = false;
            for (int x = 0, y = word.Length - 1; x <= y && (foundX != true || foundY != true);)
            {
                if (x != y || foundY != true)
                {
                    if (word[x] >= '0' && word[x] <= '9' && foundX == false)
                    {
                        var numberX = Convert.ToInt16(char.GetNumericValue(word[x]));
                        if (number != 0)
                        {
                            numberX *= 10;
                        }
                        number += numberX;
                        foundX = true;
                    }
                }

                if (x != y)
                {
                    if (word[y] >= '0' && word[y] <= '9' && foundY == false)
                    {
                        if (number != 0)
                        {
                            number *= 10;
                        }
                        number += Convert.ToInt16(char.GetNumericValue(word[y]));
                        foundY = true;
                    }
                }

                if (!foundX)
                {
                    x++;
                }
                if (!foundY)
                {
                    y--;
                }
            }

            if ((!foundX || !foundY) && (foundX || foundY))
            {
                number += (number * 10);
            }

            return number;
        }

        private int GetNumber2(string word)
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

            if(buffer != -1)
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
