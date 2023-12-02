using AdventOfCode;
using System;

public class Program
{
    public static void Main()
    {
        string input = "a1c";
        string input2 = "1234";
        string input3 = "abcd";
        string input4 = "1asrgasrgasrg";
        string input5 = "aasrgasrgasrgarg6f";

        //List<string> list = new List<string> { input, input2, input3, input4, input5 };

        List<string> list = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory,"../../../inputs/Problem1Input.txt")).ToList();

        IProblem problem1 = new Day1Problem1(list);

        Console.WriteLine(problem1.SolveProblem());
        Console.ReadLine();
    }
}