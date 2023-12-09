using Day3;

var lines = File.ReadLines(@"E:\Dev\AdventOfCode_2023\Day3\Input.txt").ToList();

var charArray = new char[lines.Count, lines.Select(str => str.Length).Max()];
for (var i = 0; i < lines.Count; i++)
    for (var j = 0; j < lines[i].Length; j++)
        charArray[i, j] = lines[i][j];

var relevantEntries = new List<SchemaEntry>();

for (var i = 0; i < charArray.GetLength(0); i++)
{
    var currentNumber = string.Empty;
    var schemaEntry = new SchemaEntry();

    for (var j = 0; j < charArray.GetLength(1); j++)
    {
        var entry = charArray[i, j];

        if (char.IsDigit(entry))
        {
            if (currentNumber == string.Empty)
                schemaEntry.Start = (i, j);

            currentNumber += entry;

            //check if the current number is finished
            var currentNumberIsFinished = false;
            if (j + 1 < charArray.GetLength(1))
            {
                if (!char.IsDigit(charArray[i, j + 1]))
                    currentNumberIsFinished = true;
            }
            else
                currentNumberIsFinished = true;
            
            if (currentNumberIsFinished)
            {
                schemaEntry.End = (i, j);
                schemaEntry.Entry = currentNumber;
                relevantEntries.Add(schemaEntry);
                
                currentNumber = string.Empty;
                schemaEntry = new SchemaEntry();
            }
        }

        if (entry == '*')
        {
            schemaEntry.Start = (i, j);
            schemaEntry.Entry = "*";
            relevantEntries.Add(schemaEntry);
            schemaEntry = new SchemaEntry();
        }
    }
}

var sum = relevantEntries.Where(x => x.IsGear)
    .Select(gear => relevantEntries.Where(x => !x.IsGear)
        .Where(x => x.Start.startICoordinate == gear.Start.startICoordinate || 
                    x.Start.startICoordinate == gear.Start.startICoordinate - 1 ||
                    x.Start.startICoordinate == gear.Start.startICoordinate + 1)
        .Where(x => x.Start.startJCoordinate == gear.Start.startJCoordinate ||
                    x.Start.startJCoordinate == gear.Start.startJCoordinate - 1 ||
                    x.Start.startJCoordinate == gear.Start.startJCoordinate + 1 ||
                    x.End!.Value.endJCoordinate == gear.Start.startJCoordinate ||
                    x.End!.Value.endJCoordinate == gear.Start.startJCoordinate - 1 ||
                    x.End!.Value.endJCoordinate == gear.Start.startJCoordinate + 1)
        .Select(x => int.Parse(x.Entry))
        .ToList())
    .Where(adjacentNumbers => adjacentNumbers.Count == 2)
    .Sum(adjacentNumbers => adjacentNumbers[0] * adjacentNumbers[1]);

Console.WriteLine(sum);