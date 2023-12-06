using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class RGB2
    {
        public int red = 0, green = 0, blue = 0;
    }
    public class RGBGame2 : IRGBGame2
    {
        private List<RGB2> list;
        private int minRed = 0, minBlue = 0, minGreen = 0;
        public RGBGame2(List<RGB2> list)
        {
            this.list = list;
        }

        private bool SolveGames()
        {
            foreach(var rgb in list)
            {
                if (rgb.red > minRed)
                    minRed = rgb.red;
                if (rgb.blue > minBlue)
                    minBlue = rgb.blue;
                if (rgb.green > minGreen)
                    minGreen = rgb.green;
            }
            return true;
        }
        public int GetPower()
        {
            SolveGames();
            return minRed * minBlue * minGreen;
        }
    }

    public class RGBGameCollection2: IRGBGameCollection2
    {
        private List<RGBGame2> games;
        public RGBGameCollection2(List<string> input)
        {
            games = ParseInput(input);
        }
        private List<RGBGame2> ParseInput(List<string> stringList)
        {
            List<RGBGame2> games = new List<RGBGame2>();
            
            foreach (string s in stringList)
            {
                var list = new List<RGB2>();
                var stringWithoutGameBit = Regex.Replace(s, @"[Game]+[ ]([0-9])*[:][ ]", "");
                var splitList = new List<string>(stringWithoutGameBit.Split(";", StringSplitOptions.RemoveEmptyEntries));

                foreach (var split in splitList)
                {
                    var colorList = new List<string>(split.Split(",", StringSplitOptions.RemoveEmptyEntries));
                    var rgb = new RGB2();
                    foreach (var color in colorList)
                    {
                        if (color.Contains("green"))
                        {
                            rgb.green = Convert.ToInt32(color.Replace(" green", ""));
                        }
                        else if (color.Contains("blue"))
                        {
                            rgb.blue = Convert.ToInt32(color.Replace(" blue", ""));
                        }
                        else if (color.Contains("red"))
                        {
                            rgb.red = Convert.ToInt32(color.Replace(" red", ""));
                        }
                    }
                    list.Add(rgb);
                }
                games.Add(new RGBGame2(list));
            }

            return games;
        }

        private int SolveGames()
        {
            int sumOfPowers = 0;

            for(int i = 0; i < games.Count; i++)
            {
                sumOfPowers += games[i].GetPower();
            }

            return sumOfPowers;
        }

        public int GetSumOfPowers()
        {
            return SolveGames();
        }
    }

    public class Day2Problem2 : IProblem
    {
        private IRGBGameCollection2 collection;

        public string ID()
        {
            return "Day 2 Problem 2";
        }
        public Day2Problem2()
        {
            var input = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "../../../inputs/Day2Problem1Input.txt")).ToList();
            collection = new RGBGameCollection2(input);
        }

        public string SolveProblem()
        {
            int result = collection.GetSumOfPowers();
            return result.ToString();
        }
    }
}
