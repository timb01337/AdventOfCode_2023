var lines = File.ReadLines(@"E:\Dev\AdventOfCode_2023\Day4\input.txt").ToList();

double sum = 0;
foreach (var line in lines)
{
   var winningNumbers = line.Split(": ")[1].Split(" | ")[0].Trim().Split(' ').Where(x => x != string.Empty).Select(x => int.Parse(x)).ToArray();
   var numbersOnCard = line.Split(": ")[1].Split(" | ")[1].Trim().Split(' ').Where(x => x != string.Empty).Select(x => int.Parse(x)).ToArray();
   var countWinningNumbers = numbersOnCard.Intersect(winningNumbers).Count();
   if (countWinningNumbers == 1)
       sum += 1;
   else if (countWinningNumbers == 2)
       sum += 2;
   else if (countWinningNumbers > 2)
       sum += Math.Pow(2,countWinningNumbers - 1);
}

Console.WriteLine(sum);