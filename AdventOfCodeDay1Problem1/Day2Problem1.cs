using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class RGB: IRGB
    {
        public int red = 0, green = 0, blue = 0;
        private const int maxRed = 12, maxGreen = 13, maxBlue = 14;

        public bool CanBeSolved()
        {
            if(red <= maxRed && green <= maxGreen && blue <= maxBlue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class RGBGame : IRGBGame
    {
        private List<RGB> list;
        public RGBGame(List<RGB> list)
        {
            this.list = list;
        }

        public bool CanBeSolved()
        {
            foreach(var rgb in list)
            {
                if(!rgb.CanBeSolved())
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class RGBGameCollection: IRGBGameCollection
    {
        private List<RGBGame> games;
        public RGBGameCollection(List<string> input)
        {
            games = ParseInput(input);
        }
        private List<RGBGame> ParseInput(List<string> stringList)
        {
            List<RGBGame> games = new List<RGBGame>();
            
            foreach (string s in stringList)
            {
                var list = new List<RGB>();
                var stringWithoutGameBit = Regex.Replace(s, @"[Game]+[ ]([0-9])*[:][ ]", "");
                var splitList = new List<string>(stringWithoutGameBit.Split(";", StringSplitOptions.RemoveEmptyEntries));

                foreach (var split in splitList)
                {
                    var colorList = new List<string>(split.Split(",", StringSplitOptions.RemoveEmptyEntries));
                    var rgb = new RGB();
                    foreach (var color in colorList)
                    {
                        if (color.Contains("green"))
                        {
                            rgb.green = Convert.ToInt16(color.Replace(" green", ""));
                        }
                        else if (color.Contains("blue"))
                        {
                            rgb.blue = Convert.ToInt16(color.Replace(" blue", ""));
                        }
                        else if (color.Contains("red"))
                        {
                            rgb.red = Convert.ToInt16(color.Replace(" red", ""));
                        }
                    }
                    list.Add(rgb);
                }
                games.Add(new RGBGame(list));
            }

            return games;
        }

        public int GetSumOfIDs()
        {
            int count = 0;

            for(int i = 0; i < games.Count; i++)
            {
                if (games[i].CanBeSolved())
                {
                    count += (i+1);
                }
            }

            return count;
        }
    }

    public class Day2Problem1 : IProblem
    {
        private IRGBGameCollection collection;
        public string ID()
        {
            return "Day 2 Problem 1";
        }
        public Day2Problem1()
        {
            var input = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "../../../inputs/Day2Problem1Input.txt")).ToList();
            collection = new RGBGameCollection(input);
        }

        public string SolveProblem()
        {
            return collection.GetSumOfIDs().ToString();
        }
    }
}
