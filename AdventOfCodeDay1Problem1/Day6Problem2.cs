using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day6Problem2
{
    public class Race
    {
        public double Time;
        public double Distance;

        public Race(string time, string distance)
        {
            Time = Convert.ToDouble(time);
            Distance = Convert.ToDouble(distance);
        }

        public double CalculateRace()
        {
            double positivePow = ((Time * - 1) + Math.Sqrt((Math.Pow(Time, 2) - 4 * (Distance - 1)))) / 2;
            double negativePow = ((Time * - 1) - Math.Sqrt((Math.Pow(Time, 2) - 4 * (Distance - 1)))) / 2;

            var threshold1 = Math.Abs(Convert.ToInt64(negativePow) + (negativePow % 1 != 0 ? 1 : 0));
            var threshold2 = Math.Abs(Convert.ToInt64(positivePow) - (positivePow % 1 != 0 ? 1 : 0));

            var bigThreshold = (threshold1 > threshold2 ? threshold1 : threshold2);
            var smallThreshold = (threshold2 < threshold1 ? threshold2 : threshold1);

            var wins = Time - (Time - bigThreshold) - smallThreshold + 1;

            return wins;
        }
    }
    public class Day6Problem2 : IProblem
    {
        private List<string> input;
        private List<Race> races;

        public string ID()
        {
            return "Day 6 Problem 2";
        }

        public Day6Problem2()
        {
            races = new List<Race>();
            input = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "../../../inputs/Day6Problem2Input.txt")).ToList();
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
