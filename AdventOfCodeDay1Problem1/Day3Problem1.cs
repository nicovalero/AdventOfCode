using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public struct Coordinates
    {
        public int X, Y;
        public Coordinates(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
    public struct NumberCoordinates
    {
        public int minX, maxX, Y;
        public NumberCoordinates(int minX, int maxX, int Y)
        {
            this.minX = minX;
            this.maxX = maxX;
            this.Y = Y;
        }
    }

    public class Number
    {
        private string number;
        private NumberCoordinates coords;
        public NumberCoordinates Coords { get { return coords; } set { coords = value; } }
        public HashSet<Coordinates> AffectedArea { get; private set; }
        public Number(char firstNumber, NumberCoordinates coords)
        {
            Coords = coords;
            AffectedArea = SetAffectedArea(coords);
            number += firstNumber;
        }

        private HashSet<Coordinates> SetAffectedArea(NumberCoordinates coords)
        {
            var affectedArea = new HashSet<Coordinates>();
            for (int y = coords.Y - 1; y <= coords.Y + 1; y++)
            {
                for (int x = coords.minX - 1; x <= coords.maxX + 1; x++)
                {
                    if (y == coords.Y)
                    {
                        if (x == coords.minX - 1 || x == coords.maxX + 1)
                        {
                            affectedArea.Add(new Coordinates(x, y));
                        }
                    }
                    else
                    {
                        affectedArea.Add(new Coordinates(x, y));
                    }
                }
            }

            return affectedArea;
        }

        public bool IsAffected(Coordinates affectedCoords)
        {
            if (affectedCoords.Y == coords.Y && (affectedCoords.X <= coords.maxX && affectedCoords.X >= coords.minX))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateNumber(char c, int x)
        {
            number += c;
            coords.maxX = x;
        }

        public int GetNumberInteger()
        {
            return Convert.ToInt32(number);
        }
    }

    public class Symbol
    {
        public HashSet<Coordinates> AffectedArea { get; private set; }
        public Coordinates Coords { get; set; }
        public Symbol(Coordinates coords)
        {
            Coords = coords;
            AffectedArea = SetAffectedArea(coords);
        }

        private HashSet<Coordinates> SetAffectedArea(Coordinates coords)
        {
            var affectedArea = new HashSet<Coordinates>();
            for (int y = coords.Y - 1; y <= coords.Y + 1; y++)
            {
                for (int x = coords.X - 1; x <= coords.X + 1; x++)
                {
                    if (y == coords.Y)
                    {
                        if (x == coords.X - 1 || x == coords.X + 1)
                        {
                            affectedArea.Add(new Coordinates(x, y));
                        }
                    }
                    else
                    {
                        affectedArea.Add(new Coordinates(x, y));
                    }
                }
            }

            return affectedArea;
        }
    }
    public class Day3Problem1 : IProblem
    {
        private List<string> input;
        private List<Number> numbers;
        private List<Symbol> symbols;

        public Day3Problem1()
        {
            input = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "../../../inputs/Day3Problem1Input.txt")).ToList();
            numbers = new List<Number>();
            symbols = new List<Symbol>();
            ParseInput(input);
        }

        private void ParseInput(List<string> input)
        {
            Number auxNumber;
            for(int y = 0; y < input.Count; y++)
            {
                auxNumber = null;
                for(int x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] < '0' || input[y][x] > '9')
                    {
                        if (auxNumber != null)
                        {
                            numbers.Add(auxNumber);
                            auxNumber = null;
                        }
                    }

                    if (input[y][x] == '.')
                    {
                        
                    }
                    else if (input[y][x] >= '0' && input[y][x] <= '9')
                    {
                        if(auxNumber == null)
                        {
                            var numCoords = new NumberCoordinates(x, x, y);
                            auxNumber = new Number(input[y][x], numCoords);
                        }
                        else
                        {
                            auxNumber.UpdateNumber(input[y][x], x);
                        }
                    }
                    else
                    {
                        var symbol = new Symbol(new Coordinates(x, y));
                        symbols.Add(symbol);
                    }
                }
                if(auxNumber != null)
                {
                    numbers.Add(auxNumber);
                }
            }
        }

        private HashSet<Number> GetAffectedNumbers()
        {
            HashSet<Number> affectedNumbers = new HashSet<Number>();

            foreach(var symbol in symbols)
            {
                foreach(var coord in symbol.AffectedArea)
                {
                    var affected = numbers.Select(x => x)
                                    .Where( x => x.IsAffected(coord))
                                    .ToList();
                    foreach(var number in affected)
                    {
                        if(!affectedNumbers.Contains(number))
                        {
                            affectedNumbers.Add(number);
                        }
                    }
                }
            }

            return affectedNumbers;
        }

        public string SolveProblem()
        {
            var result = GetAffectedNumbers();
            int sum = 0;
            foreach (var n in result)
            {
                sum += n.GetNumberInteger();
            }
            return sum.ToString();
        }
    }
}
