var lines = File.ReadLines(@"E:\Dev\AdventOfCode_2023\Day4\easyInput.txt").ToList();

//Idee: die karte 1 gewinnt jedes mal die karten 1 - 5
//-> die karte 2 gewinnt jedes mal die karten 3 und 4



//key = card number
//value = amount one by card
var singleCardValue = new Dictionary<int, int>();

for (var i = 0; i < lines.Count; i++)
    singleCardValue.Add(i + 1, CountWinningNumbers(GetWinningNumbers(lines[i]), GetNumbersOnCard(lines[i])));

//populate the dictionary
var cardCopyCountDict = singleCardValue.ToDictionary(entry => entry.Key, entry => 1);

foreach (var entry in singleCardValue)
{
    //check how many cards we won
    var amountOfCardsWon = singleCardValue[entry.Key];

    if (amountOfCardsWon > 0)
    {
        //the next cards index where we start iterating
        var nextIndex = entry.Key + 1;

        Console.WriteLine($"We are now looking at card {entry.Key}");
        
        //the next X cards (amountOfCardsWon) we need to count the copies
        for (var i = nextIndex; i < nextIndex + amountOfCardsWon; i++)
        {
            Console.WriteLine($"We added one copy to Card {i}");
            cardCopyCountDict[i] += 1;
        }

        Console.WriteLine("-------------");
    }
}

// you end up with 1 instance of card 1
// 2 instances of card 2
// 4 instances of card 3
// 8 instances of card 4
// 14 instances of card 5
// and 1 instance of card 6

var sum = cardCopyCountDict.Sum(entry => singleCardValue[entry.Key] * entry.Value);

Console.WriteLine($"Ich zähle die kopien, denn ich bin ein Kopierer: {sum}");
return;


int CountWinningNumbers(IEnumerable<int> a, IEnumerable<int> b) => a.Intersect(b).Count();

int[] GetWinningNumbers(string line) => line.Split(": ")[1].Split(" | ")[0].Trim().Split(' ')
    .Where(x => x != string.Empty).Select(x => int.Parse(x)).ToArray();

int[] GetNumbersOnCard(string line) => line.Split(": ")[1].Split(" | ")[1].Trim().Split(' ')
    .Where(x => x != string.Empty).Select(x => int.Parse(x)).ToArray();