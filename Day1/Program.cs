using System.IO;
using System;

var lines = File.ReadLines(@"E:\Dev\AdventOfCode_2023\Day1\input.txt");


var sum = 0;



foreach (var line in lines)
{
    var firstDigit = "";
    var lastDigit = "";

    for (var i = 0; i < line.Length; i++)
    {
        if (char.IsDigit(line[i]))
        {
            firstDigit = line[i].ToString();
            break;
        }
    }


    for (var i = line.Length - 1; i >= 0; i--)
    {
        if (char.IsDigit(line[i]))
        {
            lastDigit = line[i].ToString();
            break;
        }
    }

    var lineResult = firstDigit + lastDigit;
    var lineResultAsNumber = int.Parse(lineResult);
    sum += lineResultAsNumber;

}

Console.WriteLine(sum);