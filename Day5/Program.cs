var lines = File.ReadLines(@"C:\Daten\repos\AdventOfCode_2023\Day5\easyInput.txt").ToList();

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

long v1start = 515785082, v1end = v1start + 87905039;
long v2start = 2104518691, v2end = v2start + 503149843;
long v3start = 720333403, v3end = v3start + 385234193;
long v4start = 1357904101, v4end = v4start + 283386167;
long v5start = 93533455, v5end = v5start + 128569683;
long v6start = 2844655470, v6end = v6start + 24994629;
long v7start = 3934515023, v7end = v7start + 67327818;
long v8start = 2655687716, v8end = v8start + 8403417;
long v9start = 3120497449, v9end = v9start + 107756881;
long v10start = 4055128129, v10end = v10start + 9498708;


//just loop and check from location to seed until we found the lowest location
for (long i = 4153809; i < long.MaxValue; i++)
{
    var locationToHumidityResult = CalculateConversion(humidityToLocationMap, i);
    var humidityToTemperatureResult = CalculateConversion(temperatureToHumidityMap, locationToHumidityResult);
    var temperatureToLightResult = CalculateConversion(lightToTemperatureMap, humidityToTemperatureResult);
    var lightToWaterResult = CalculateConversion(waterToLightMap, temperatureToLightResult);
    var waterToFertilizerResult = CalculateConversion(fertilizerToWaterMap, lightToWaterResult);
    var fertilizerToSoilResult = CalculateConversion(soilToFertilizerMap, waterToFertilizerResult);
    var seed = CalculateConversion(seedToSoilMap, fertilizerToSoilResult);

    if (seed >= v1start && seed <= v1end ||
        seed >= v2start && seed <= v2end ||
        seed >= v3start && seed <= v3end ||
        seed >= v4start && seed <= v4end ||
        seed >= v5start && seed <= v5end ||
        seed >= v6start && seed <= v6end ||
        seed >= v7start && seed <= v7end ||
        seed >= v8start && seed <= v8end ||
        seed >= v9start && seed <= v9end ||
        seed >= v10start && seed <= v10end)
    {
        Console.WriteLine($"RESULT SEED: {seed} AT LOCATION {i}");
        break;
    }

    Console.WriteLine(i);
}


long CalculateConversion(IEnumerable<(long destinationRangeStart, long sourceRangeStart, long rangeLength)> sourceMap, long destinationValue)
{
    var howToConversionEntry = sourceMap.FirstOrDefault(x => x.destinationRangeStart <= destinationValue
                                                             && x.destinationRangeStart + x.rangeLength >= destinationValue);
    
    if (!howToConversionEntry.Equals(default))
        return destinationValue + howToConversionEntry.sourceRangeStart - howToConversionEntry.destinationRangeStart;

    return destinationValue;
}