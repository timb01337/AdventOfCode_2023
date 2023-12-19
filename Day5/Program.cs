var lines = File.ReadLines(@"C:\Daten\repos\AdventOfCode_2023\Day5\easyInput.txt").ToList();

var originalSeeds = lines[0].Split(':')[1].Trim().Split(' ').Select(seed => long.Parse(seed)).ToList();
var allSeeds = new List<long>();

for (var i = 0; i < originalSeeds.Count; i++)
{
    if (i % 2 != 0)
    {
        for (var j = 0; j < originalSeeds[i]; j++)
            allSeeds.Add(originalSeeds[i - 1] + j);
    }
}


List<(long destinationRangeStart, long sourceRangeStart, long rangeLength)> seedToSoilMap = new();
List<(long destinationRangeStart, long sourceRangeStart, long rangeLength)> soilToFertilizerMap = new();
List<(long destinationRangeStart, long sourceRangeStart, long rangeLength)> fertilizerToWaterMap = new();
List<(long destinationRangeStart, long sourceRangeStart, long rangeLength)> waterToLightMap = new();
List<(long destinationRangeStart, long sourceRangeStart, long rangeLength)> lightToTemperatureMap = new();
List<(long destinationRangeStart, long sourceRangeStart, long rangeLength)> temperatureToHumidityMap = new();
List<(long destinationRangeStart, long sourceRangeStart, long rangeLength)> humidityToLocationMap = new();

bool seedToSoilMapFound = false,
    soilToFertilizerMapFound = false,
    fertilizerToWaterMapFound = false,
    waterToLightMapFound = false,
    lightToTemperatureMapFound = false,
    temperatureToHumidityMapFound = false,
    humidityToLocationMapFound = false;

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
        seedToSoilMap.Add((long.Parse(split[0]), long.Parse(split[1]), long.Parse(split[2])));
    }
    
    if (soilToFertilizerMapFound)
    {
        if (string.IsNullOrEmpty(line))
        {
            soilToFertilizerMapFound = false;
            continue;
        }

        var split = line.Split(' ');
        soilToFertilizerMap.Add((long.Parse(split[0]), long.Parse(split[1]), long.Parse(split[2])));
    }
    
    if (fertilizerToWaterMapFound)
    {
        if (string.IsNullOrEmpty(line))
        {
            fertilizerToWaterMapFound = false;
            continue;
        }

        var split = line.Split(' ');
        fertilizerToWaterMap.Add((long.Parse(split[0]), long.Parse(split[1]), long.Parse(split[2])));
    }
    
    if (waterToLightMapFound)
    {
        if (string.IsNullOrEmpty(line))
        {
            waterToLightMapFound = false;
            continue;
        }

        var split = line.Split(' ');
        waterToLightMap.Add((long.Parse(split[0]), long.Parse(split[1]), long.Parse(split[2])));
    }
    
    if (lightToTemperatureMapFound)
    {
        if (string.IsNullOrEmpty(line))
        {
            lightToTemperatureMapFound = false;
            continue;
        }

        var split = line.Split(' ');
        lightToTemperatureMap.Add((long.Parse(split[0]), long.Parse(split[1]), long.Parse(split[2])));
    }
    
    if (temperatureToHumidityMapFound)
    {
        if (string.IsNullOrEmpty(line))
        {
            temperatureToHumidityMapFound = false;
            continue;
        }

        var split = line.Split(' ');
        temperatureToHumidityMap.Add((long.Parse(split[0]), long.Parse(split[1]), long.Parse(split[2])));
    }
    
    if (humidityToLocationMapFound)
    {
        if (string.IsNullOrEmpty(line))
        {
            humidityToLocationMapFound = false;
            continue;
        }

        var split = line.Split(' ');
        humidityToLocationMap.Add((long.Parse(split[0]), long.Parse(split[1]), long.Parse(split[2])));
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

var minLocation = long.MaxValue;

//part 1
// foreach (var seed in originalSeeds)
// {
//     //is my seed affected by changes made in the map
//     //check if any source range start + range length contains my seed
//     var seedToSoilResult = CalculateConversion(seedToSoilMap, seed);
//     var soilToFertilizerResult = CalculateConversion(soilToFertilizerMap, seedToSoilResult);
//     var fertilizerToWaterResult = CalculateConversion(fertilizerToWaterMap, soilToFertilizerResult);
//     var waterToLightResult = CalculateConversion(waterToLightMap, fertilizerToWaterResult);
//     var lightToTemperatureResult = CalculateConversion(lightToTemperatureMap, waterToLightResult);
//     var temperateToHumidityResult = CalculateConversion(temperatureToHumidityMap, lightToTemperatureResult);
//     var humidityToLocationResult = CalculateConversion(humidityToLocationMap, temperateToHumidityResult);
//
//     minLocation = humidityToLocationResult < minLocation ? humidityToLocationResult : minLocation;
// }


//we'll never loop over the whole thing! we stop as soon as we found the lowest possible entry
for (var i = 0; i < allSeeds.Count; i++)
{
    
     var locationToHumidityResult = CalculateConversion(humidityToLocationMap, 79);

     Console.WriteLine(locationToHumidityResult);
     
     // var temperateToHumidityResult = CalculateConversion(temperatureToHumidityMap, lightToTemperatureResult);
     // var lightToTemperatureResult = CalculateConversion(lightToTemperatureMap, waterToLightResult);
     // var waterToLightResult = CalculateConversion(waterToLightMap, fertilizerToWaterResult);
     // var fertilizerToWaterResult = CalculateConversion(fertilizerToWaterMap, soilToFertilizerResult);
     // var soilToFertilizerResult = CalculateConversion(soilToFertilizerMap, seedToSoilResult);
     //     var seedToSoilResult = CalculateConversion(seedToSoilMap, seed);
}



Console.WriteLine(minLocation);

long CalculateConversion(IEnumerable<(long destinationRangeStart, long sourceRangeStart, long rangeLength)> sourceMap, long destinationValue)
{
    var howToConversionEntry = sourceMap.FirstOrDefault(x => x.destinationRangeStart <= destinationValue
                                                            && x.destinationRangeStart + x.rangeLength >= destinationValue);

    long sourceValue;
    
    if (!howToConversionEntry.Equals(default))
    {
        var delta = howToConversionEntry.sourceRangeStart - howToConversionEntry.destinationRangeStart;
        sourceValue = destinationValue + delta;
    }
    else
        sourceValue = destinationValue;

    return sourceValue;
}