using Day7;

var lines = File.ReadLines("C:\\Daten\\repos\\AdventOfCode_2023\\Day7\\easyInput.txt").ToList();


var handStrengthMapping = new Dictionary<HandCards, int>();

foreach (var line in lines)
{
    var hand = new HandCards(line.Split(' ')[0], int.Parse(line.Split(' ')[1]));
    handStrengthMapping.Add(hand, hand.GetStrengthOfHand());
}

var result = handStrengthMapping
    .OrderBy(x => x.Value)
    .ThenBy(x => x.Key.OrderedHandValue)
    .Select((entry, index) => (index + 1) * entry.Key.Stake).Sum();

Console.WriteLine(result);
