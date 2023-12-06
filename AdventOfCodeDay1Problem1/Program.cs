using AdventOfCode;
using AdventOfCode.Day3Problem2;
using AdventOfCode.Day4Problem1;
using AdventOfCode.Day4Problem2;
using AdventOfCode.Day5Problem1;
using AdventOfCode.Day6Problem1;
using AdventOfCode.Day6Problem2;
using System;

public class Program
{
    public static void Main()
    {
        IProblem problem1 = new Day1Problem1();
        IProblem problem2 = new Day1Problem2();
        IProblem problem3 = new Day2Problem1();
        IProblem problem4 = new Day2Problem2();
        IProblem problem5 = new Day3Problem1();
        IProblem problem6 = new Day3Problem2();
        IProblem problem7 = new Day4Problem1();
        IProblem problem8 = new Day4Problem2();
        IProblem problem9 = new Day5Problem1();
        IProblem problem11 = new Day6Problem1();
        IProblem problem12 = new Day6Problem2();

        List<IProblem> problems = new List<IProblem> { problem1, problem2, problem3, problem4, problem5, problem6, problem7, problem8, problem9, problem11, problem12 };

        foreach(var problem in problems)
        {
            Console.WriteLine(String.Concat(problem.ID(), ": ", problem.SolveProblem()));
        }
        Console.WriteLine("Problems finished");
        Console.ReadLine();
    }
}