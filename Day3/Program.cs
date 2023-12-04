//var lines = File.ReadLines(@"E:\Dev\AdventOfCode_2023\Day3\input.txt");

var lines = File.ReadLines(@"E:\Dev\AdventOfCode_2023\Day3\easyInput.txt").ToList();

var charArray = new char[lines.Count, lines.Select(str => str.Length).Max()];

for (var i = 0; i < lines.Count; i++)
{
    for (var j = 0; j < lines[i].Length; j++)
        charArray[i, j] = lines[i][j];
}


for (var i = 0; i < charArray.GetLength(0); i++)
{
    for (var j = 0; j < charArray.GetLength(1); j++)
    {
        var entry = charArray[i,j];
    }
}
