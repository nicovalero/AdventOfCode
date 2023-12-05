using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day4Problem1
{
    public class Number
    {
        public string Value { get; set; }

        public Number(string value)
        {
            Value = value.Trim();
        }

        public override bool Equals(object? number)
        {
            var numberValue = number as Number;
            return numberValue.Value == this.Value;
        }

        public override int GetHashCode()
        {
            int result = Value.GetHashCode();
            return result;
        }
    }

    public class Day4Problem1 : IProblem
    {
        private List<string> input;
        private List<HashSet<Number>> prizedNumbers;
        private List<List<Number>> playedNumbers;

        public Day4Problem1()
        {
            input = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "../../../inputs/Day4Problem1Input.txt")).ToList();
            prizedNumbers = new List<HashSet<Number>>();
            playedNumbers = new List<List<Number>>();
            ParseInput(input);
        }

        private void ParseInput(List<string> input)
        {
            foreach (string s in input)
            {
                var stringWithoutCardBit = Regex.Replace(s, @"[Card]+([ ])*([0-9])*[:][ ]", "");
                var splitList = new List<string>(stringWithoutCardBit.Split("|", StringSplitOptions.RemoveEmptyEntries));

                var prizedNumbersSplit = new List<string>(splitList[0].Split(" ", StringSplitOptions.RemoveEmptyEntries));
                var playedNumbersSplit = new List<string>(splitList[1].Split(" ", StringSplitOptions.RemoveEmptyEntries));

                var prizeSet = new HashSet<Number>();
                var playedList = new List<Number>();

                foreach(var number in prizedNumbersSplit)
                {
                    prizeSet.Add(new Number(number));
                }

                foreach (var number in playedNumbersSplit)
                {
                    playedList.Add(new Number(number));
                }

                prizedNumbers.Add(prizeSet);
                playedNumbers.Add(playedList);
            }
        }

        private double GetPoints()
        {
            double sum = 0;

            for(int i = 0; i < playedNumbers.Count; i++)
            {
                var matches = 0;
                foreach(var number in playedNumbers[i])
                {
                    if (prizedNumbers[i].Contains(number))
                    {
                        matches++;
                    }
                }
                if (matches != 0)
                {
                    sum += (Math.Pow(2, matches - 1));
                }
            }

            return sum;
        }

        public string SolveProblem()
        {
            var result = GetPoints();
            return result.ToString();
        }
    }
}
