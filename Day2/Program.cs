var lines = File.ReadLines(@"E:\Dev\AdventOfCode_2023\Day2\input.txt");


var result = 0;

foreach (var line in lines)
{
   

    var gameId = int.Parse(line.Split(":")[0][5..]);
    var gameFailed = false;

    var game = line.Split(":")[1].Split(";");

    var countRedCubes = 12;
    var countGreenCubes = 13;
    var countBlueCubes = 14;
    
    foreach (var draw in game)
    {
     
        
        var reds = ParseDraw(draw.Split(","), "red");
        var greens = ParseDraw(draw.Split(","), "green");
        var blue = ParseDraw(draw.Split(","), "blue");

        countRedCubes -= reds;
        countGreenCubes -= greens;
        countBlueCubes -= blue;

        if (countRedCubes < 0 || countGreenCubes < 0 || countBlueCubes < 0)
        {
            gameFailed = true;
            break;
        }
    }

    if (!gameFailed)
        result += gameId;
}

Console.WriteLine(result);


int ParseDraw(IEnumerable<string> draw, string color)
{
    var ret = int.TryParse(draw.FirstOrDefault(x => x.Contains(color))?.Replace(" " + color, ""), out var result)
        ? result
        : 0;
    return ret;
}