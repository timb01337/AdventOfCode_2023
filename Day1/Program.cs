var lines = File.ReadLines(@"E:\Dev\AdventOfCode_2023\Day1\input.txt");
var sum = 0;

foreach (var line in lines)
{
    var firstDigit = "";
    var lastDigit = "";
    var firstDigitFound = false;

    for (var i = 0; i < line.Length; i++)
    {
        if (char.IsDigit(line[i]))
        {
            if (!firstDigitFound)
            {
                firstDigit = line[i].ToString();
                firstDigitFound = true;
            }

            lastDigit = line[i].ToString();
        }
    }
    
    var lineResult = int.Parse(firstDigit + lastDigit);
    sum += lineResult;

}

Console.WriteLine(sum);