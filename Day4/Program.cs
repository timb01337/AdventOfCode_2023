var lines = File.ReadLines(@"E:\Dev\AdventOfCode_2023\Day4\Input.txt").ToList();

//Idee: die karte 1 gewinnt jedes mal die karten 1 - 5
//-> die karte 2 gewinnt jedes mal die karten 3 und 4


//key = card number
//value = amount one by card
var singleCardValue = new Dictionary<int, int>();
var totalCardValue = new Dictionary<int, int>();

for (var i = 0; i < lines.Count; i++)
{
    singleCardValue.Add(i + 1, CountWinningNumbers(GetWinningNumbers(lines[i]), GetNumbersOnCard(lines[i])));
    totalCardValue.Add(i + 1, 0);
}


//populate the dictionary
var cardCopyCountDict = singleCardValue.ToDictionary(entry => entry.Key, entry => 1);



//loop over every single card
//key = card (as integer 1-6)
//value = value of a single card
foreach (var entry in singleCardValue)
{
    //check how many cards we won
    var amountOfCardsWon = singleCardValue[entry.Key];

    if (amountOfCardsWon > 0)
    {
        //loop over the amount of copies we have
        for (var j = 0; j < cardCopyCountDict[entry.Key]; j++)
        {
            var nextIndex = entry.Key + 1;
            //the next X cards (amountOfCardsWon) we need to count the copies
            for (var i = nextIndex; i < nextIndex + amountOfCardsWon; i++) 
                //increment copy count by one for each card we won
                cardCopyCountDict[i]++;
        }
    }
}


var sum = cardCopyCountDict.Sum(x => x.Value);


Console.WriteLine($"Ich zähle die kopien, denn ich bin ein Kopierer: {sum}");
return;


int CountWinningNumbers(IEnumerable<int> a, IEnumerable<int> b) => a.Intersect(b).Count();

int[] GetWinningNumbers(string line) => line.Split(": ")[1].Split(" | ")[0].Trim().Split(' ')
    .Where(x => x != string.Empty).Select(x => int.Parse(x)).ToArray();

int[] GetNumbersOnCard(string line) => line.Split(": ")[1].Split(" | ")[1].Trim().Split(' ')
    .Where(x => x != string.Empty).Select(x => int.Parse(x)).ToArray();