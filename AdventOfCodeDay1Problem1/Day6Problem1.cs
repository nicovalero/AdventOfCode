using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day6Problem1
{
    public class Race
    {
        public int Time;
        public int Distance;

        public Race(string time, string distance)
        {
            Time = Convert.ToInt32(time);
            Distance = Convert.ToInt32(distance);
        }

        public int CalculateRace()
        {
            int wins = 0;
            for (int i = 0; i <= Time; i++)
            {
                var speed = i;
                var timeToRun = Time - i;
                if(speed * timeToRun > Distance)
                {
                    wins++;
                }
            }

            return wins;
        }
    }
    public class Day6Problem1 : IProblem
    {
        private List<string> input;
        private List<Race> races;

        public string ID()
        {
            return "Day 6 Problem 1";
        }

        public Day6Problem1()
        {
            races = new List<Race>();
            input = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "../../../inputs/Day6Problem1Input.txt")).ToList();
            ParseInput(input);
        }

        private void ParseInput(List<string> input)
        {

            var times = Regex.Replace(input[0], @"[Time]+[:][ ]", "");
            var distances = Regex.Replace(input[1], @"[Distance]+[:][ ]", "");

            var timeList = times.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList();
            var distanceList = distances.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList();

            for(int i = 0; i < timeList.Count; i++)
            {
                var time = timeList[i].Trim();
                var distance = distanceList[i].Trim();

                races.Add(new Race(time, distance));
            }
        }

        private double GetChances()
        {
            double chances = 1;

            foreach(Race race in races)
            {
                chances *= race.CalculateRace();
            }

            return chances;
        }

        public string SolveProblem()
        {
            var result = GetChances();
            return result.ToString();
        }
    }
}
