var input = new KeyValuePair<long, long>(56717999, 334113513502430);
var times = new int[56717999];
long count = 0;

for (var i = 1; i < times.Length; i++)
{
    var timeButtonWasPressed = i + 1;
    var timeLeftAfterButtonIsReleased = input.Key - timeButtonWasPressed;
    var distanceTravelled = timeLeftAfterButtonIsReleased * timeButtonWasPressed;

    if (distanceTravelled > input.Value)
        count++;
}

Console.WriteLine($"The result is {count}");