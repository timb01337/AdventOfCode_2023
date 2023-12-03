var lines = File.ReadLines(@"E:\Dev\AdventOfCode_2023\Day1\input.txt");

var sum = 0;
var digitArr = new [] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};

foreach (var line in lines)
{
    var firstDigit = string.Empty;
    var lastDigit = string.Empty;
    var digits = new Dictionary<string, (int minIndex, int maxIndex)>();

    foreach (var digit in digitArr)
    {
        var minIndex = line.IndexOf(digit, StringComparison.OrdinalIgnoreCase);
        var maxIndex = line.LastIndexOf(digit, StringComparison.OrdinalIgnoreCase);
        digits.Add(digit, (minIndex, maxIndex));
    }

    firstDigit = digits.Where(x=> x.Value.minIndex != -1)
        .MinBy(x => x.Value.minIndex).Key;
    lastDigit = digits.Where(x => x.Value.maxIndex != -1)
        .MaxBy(x => x.Value.maxIndex).Key;

    sum += int.Parse(ParseText(firstDigit) + ParseText(lastDigit));
}

Console.WriteLine(sum);

string ParseText(string input) =>
    input switch
    {
        "one" => "1",
        "two" => "2",
        "three" => "3",
        "four" => "4",
        "five" => "5",
        "six" => "6",
        "seven" => "7",
        "eight" => "8",
        "nine" => "9",
        _ => input
    };

