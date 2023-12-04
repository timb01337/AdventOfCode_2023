var lines = File.ReadLines(@"E:\Dev\AdventOfCode_2023\Day2\input.txt");

var result = 0;

foreach (var line in lines)
{
    var game = line.Split(":")[1].Split(";");
    int maxRedsInGame = 0, maxGreensInGame = 0, maxBluesInGame = 0;
    
    foreach (var draw in game)
    {
        var reds = ParseDraw(draw.Split(","), "red");
        if (reds > maxRedsInGame)
            maxRedsInGame = reds;
        
        var greens = ParseDraw(draw.Split(","), "green");
        if (greens > maxGreensInGame)
            maxGreensInGame = greens;
        
        var blues = ParseDraw(draw.Split(","), "blue");
        if (blues > maxBluesInGame)
            maxBluesInGame = blues;
    }
    
    result += (maxRedsInGame * maxGreensInGame * maxBluesInGame);
}

Console.WriteLine(result);

int ParseDraw(IEnumerable<string> draw, string color)
{
    var ret = int.TryParse(draw.FirstOrDefault(x => x.Contains(color))?.Replace(" " + color, ""), out var result) ? result : 0;
    return ret;
}