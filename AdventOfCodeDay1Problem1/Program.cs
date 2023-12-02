using AdventOfCode;
using System;

public class Program
{
    public static void Main()
    {
        IProblem problem1 = new Day1Problem1();
        IProblem problem2 = new Day1Problem2();
        IProblem problem3 = new Day2Problem1();
        IProblem problem4 = new Day2Problem2();

        List<IProblem> problems = new List<IProblem> { problem1, problem2, problem3, problem4 };

        for (int i = 0; i < problems.Count; i++)
        {
            Console.WriteLine(String.Concat("Day ", + i / 2 + 1, " / Problem ", i % 2 + 1, ": ", problems[i].SolveProblem()));
        }
        Console.ReadLine();
    }
}