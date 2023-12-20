using Day5;

var lines = File.ReadLines(@"C:\Daten\repos\AdventOfCode_2023\Day5\easyInput.txt").ToList();
var seeds = lines[0].Split(':')[1].Trim().Split(' ').Select(x => long.Parse(x)).ToArray();
var seedScopes = new List<SeedScope>();
var allMappings = Enumerable.Range(0, 7).Select(_ => new List<Map>()).ToArray();


bool seedToSoilMapFound = false,
    soilToFertilizerMapFound = false,
    fertilizerToWaterMapFound = false,
    waterToLightMapFound = false,
    lightToTemperatureMapFound = false,
    temperatureToHumidityMapFound = false,
    humidityToLocationMapFound = false;

//create all the mappings
foreach (var line in lines)
{
    if (seedToSoilMapFound)
    {
        if (string.IsNullOrEmpty(line))
        {
            seedToSoilMapFound = false;
            continue;
        }

        var split = line.Split(' ');
        allMappings[0].Add(new Map("seed-to-soil-map", long.Parse(split[0]), long.Parse(split[1]), long.Parse(split[2])));
    }

    if (soilToFertilizerMapFound)
    {
        if (string.IsNullOrEmpty(line))
        {
            soilToFertilizerMapFound = false;
            continue;
        }

        var split = line.Split(' ');
        allMappings[1].Add(new Map("soil-to-fertilizer-map", long.Parse(split[0]), long.Parse(split[1]), long.Parse(split[2])));

    }

    if (fertilizerToWaterMapFound)
    {
        if (string.IsNullOrEmpty(line))
        {
            fertilizerToWaterMapFound = false;
            continue;
        }

        var split = line.Split(' ');
        allMappings[2].Add(new Map("fertilizer-to-water-map", long.Parse(split[0]), long.Parse(split[1]), long.Parse(split[2])));
    }

    if (waterToLightMapFound)
    {
        if (string.IsNullOrEmpty(line))
        {
            waterToLightMapFound = false;
            continue;
        }

        var split = line.Split(' ');
        allMappings[3].Add(new Map("water-to-light-map", long.Parse(split[0]), long.Parse(split[1]), long.Parse(split[2])));

    }

    if (lightToTemperatureMapFound)
    {
        if (string.IsNullOrEmpty(line))
        {
            lightToTemperatureMapFound = false;
            continue;
        }

        var split = line.Split(' ');
        allMappings[4].Add(new Map("light-to-temperature-map", long.Parse(split[0]), long.Parse(split[1]), long.Parse(split[2])));

    }

    if (temperatureToHumidityMapFound)
    {
        if (string.IsNullOrEmpty(line))
        {
            temperatureToHumidityMapFound = false;
            continue;
        }

        var split = line.Split(' ');
        allMappings[5].Add(new Map("temperature-to-humidity-map", long.Parse(split[0]), long.Parse(split[1]), long.Parse(split[2])));

    }

    if (humidityToLocationMapFound)
    {
        if (string.IsNullOrEmpty(line))
        {
            humidityToLocationMapFound = false;
            continue;
        }

        var split = line.Split(' ');
        allMappings[6].Add(new Map("humidity-to-location-map", long.Parse(split[0]), long.Parse(split[1]), long.Parse(split[2])));
    }

    switch (line)
    {
        case "seed-to-soil map:":
            seedToSoilMapFound = true;
            break;
        case "soil-to-fertilizer map:":
            soilToFertilizerMapFound = true;
            break;
        case "fertilizer-to-water map:":
            fertilizerToWaterMapFound = true;
            break;
        case "water-to-light map:":
            waterToLightMapFound = true;
            break;
        case "light-to-temperature map:":
            lightToTemperatureMapFound = true;
            break;
        case "temperature-to-humidity map:":
            temperatureToHumidityMapFound = true;
            break;
        case "humidity-to-location map:":
            humidityToLocationMapFound = true;
            break;
    }
}

//create the initial seed scopes
for (var i = 0; i < seeds.Length - 1; i += 2)
    seedScopes.Add(SeedScope.CreateSeedScope(seeds[i], seeds[i + 1]));


var currentSeedScopes = new List<SeedScope>(seedScopes);

//iterate over all mappings (f.e. the seed to soil map)
foreach (var maps in allMappings)
{
    var newlyCalculatedScopes = new List<SeedScope>();
    
    //iterate over the maps entries (one line from the mapping)
    foreach (var mappingEntry in maps)
    {
        //now all seed scopes gets processed
        //we need to loop from last to first entry, cauze there will be some deletions
        for (var i = currentSeedScopes.Count - 1; i >= 0; i--)
        {
            var currentSeedScope = currentSeedScopes[i];
            
            //if the current scope somehow overlaps with the mapping we're looking at,
            //it's relevant and we most likely wanna manipulate it
            if (currentSeedScope.StartOfScope < mappingEntry.SourceEnd 
                && currentSeedScope.EndOfScope > mappingEntry.SourceStart)
            {
                currentSeedScopes.RemoveAt(i);
                
                //calculate the boundaries of the new seed scope
                //we wanna narrow down the new scope to the point where we only have values, which are relevant
                //for the start of the new scope we use either the start of the mapping or or the start of the scope (which is bigger)
                var newSeedScopeStart = Math.Max(mappingEntry.SourceStart, currentSeedScope.StartOfScope);
                //the end of the new scope will be the smaller of the two: mapping end or the end of the seed scope
                var newSeedScopeEnd = Math.Min(mappingEntry.SourceEnd, currentSeedScope.EndOfScope);

                
                //imagine the current scope starts at 50 and the newly calculated scope starts at 55
                if (currentSeedScope.StartOfScope < newSeedScopeStart)
                {
                    //we create a new scope starting at 50
                    newlyCalculatedScopes.Add(SeedScope.CreateSeedScope(currentSeedScope.StartOfScope, newSeedScopeStart - 1));
                }

                if (currentSeedScope.EndOfScope > newSeedScopeEnd)
                {
                    newlyCalculatedScopes.Add(SeedScope.CreateSeedScope(newSeedScopeEnd + 1, currentSeedScope.EndOfScope));
                }

                newlyCalculatedScopes.Add(SeedScope.CreateSeedScope(newSeedScopeStart + mappingEntry.Offset, newSeedScopeEnd + mappingEntry.Offset));
            }
        }
    } 
    
    currentSeedScopes.AddRange(newlyCalculatedScopes);
    
}

Console.WriteLine(currentSeedScopes.Min(x => x.StartOfScope));

