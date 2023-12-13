var lines = File.ReadLines(@"E:\Dev\AdventOfCode_2023\Day4\easyInput.txt").ToList();

int sum = 0;

//key = card number
//value = amount one by card
var dict = new Dictionary<int, int>();

for (var i = 0; i < lines.Count; i++)
{
    dict.Add(i + 1, CountWinningNumbers(GetWinningNumbers(lines[i]), GetNumbersOnCard(lines[i])));
}


for (var i = 0; i < lines.Count; i++)
{
    sum++;
    CardWinningRecursion(i, CountWinningNumbers(GetWinningNumbers(lines[i]), GetNumbersOnCard(lines[i])));
}

Console.WriteLine(sum);
return;


void CardWinningRecursion(int currentLine, int cardsWon)
{
    for (var i = 1; i <= cardsWon; i++)
    {
        Console.WriteLine(currentLine + i);
        var amountWon = dict[currentLine + i];

        sum += amountWon;
        if (amountWon == 0)
            break;

        CardWinningRecursion(currentLine + 1, amountWon);
    }
}

int CountWinningNumbers(IEnumerable<int> a, IEnumerable<int> b) => a.Intersect(b).Count();

int[] GetWinningNumbers(string line) => line.Split(": ")[1].Split(" | ")[0].Trim().Split(' ')
    .Where(x => x != string.Empty).Select(x => int.Parse(x)).ToArray();

int[] GetNumbersOnCard(string line) => line.Split(": ")[1].Split(" | ")[1].Trim().Split(' ')
    .Where(x => x != string.Empty).Select(x => int.Parse(x)).ToArray();