namespace Day7;

public class HandCards
{
    private Dictionary<char, int> _cardCountDictionary;

    private static Dictionary<char, string> _cardValues = new()
    {
        {'J', "10"},
        {'2', "11"},
        {'3', "12"},
        {'4', "13"},
        {'5', "14"},
        {'6', "15"},
        {'7', "16"},
        {'8', "17"},
        {'9', "18"},
        {'T', "19"},
        {'Q', "20"},
        {'K', "21"},
        {'A', "22"}
    };

    public HandCards(string allHandCards, int value)
    {
        Id = allHandCards;
        Stake = value;
        Card1Value = int.Parse(_cardValues[allHandCards[0]]);
        Card2Value = int.Parse(_cardValues[allHandCards[1]]);
        Card3Value = int.Parse(_cardValues[allHandCards[2]]);
        Card4Value = int.Parse(_cardValues[allHandCards[3]]);
        Card5Value = int.Parse(_cardValues[allHandCards[4]]);
        //this won't change for part two because it's only used for deciding equal hands
        OrderedHandValue = long.Parse(_cardValues[allHandCards[0]] + _cardValues[allHandCards[1]] +
                                      _cardValues[allHandCards[2]] + _cardValues[allHandCards[3]] +
                                      _cardValues[allHandCards[4]]);

        if (allHandCards.Contains('J'))
            _cardCountDictionary = InitializeCardCountDictWithJoker(allHandCards);
        else
            _cardCountDictionary = InitializeCardCountDict(allHandCards);
    }

    public string Id { get; set; }
    public int Stake { get; set; }
    public int Card1Value { get; set; }
    public int Card2Value { get; set; }
    public int Card3Value { get; set; }
    public int Card4Value { get; set; }
    public int Card5Value { get; set; }

    public long OrderedHandValue { get; set; }

    public int GetStrengthOfHand()
    {
        if (IsFiveOfAKind())
            return 700;
        if (IsFourOfAKind())
            return 600;
        if (IsFullHouse())
            return 500;
        if (IsThreeOfAKind())
            return 400;
        if (IsTwoPair())
            return 300;
        if (IsOnePair())
            return 200;
        if (IsHighCard())
            return 100;

        throw new InvalidOperationException("Should never happen, each hand should be resolved as one" +
                                            $" of the above. Hand causing the error: {Id}");
    }

    private bool IsFiveOfAKind() => _cardCountDictionary.First().Value == 5;

    private bool IsFourOfAKind() => _cardCountDictionary.First().Value == 4;

    private bool IsFullHouse() => _cardCountDictionary.Count == 2;

    private bool IsThreeOfAKind() => _cardCountDictionary.First().Value == 3;

    private bool IsTwoPair() => _cardCountDictionary.First().Value == 2 && _cardCountDictionary.Skip(1).First().Value == 2;

    private bool IsOnePair() => _cardCountDictionary.First().Value == 2 && _cardCountDictionary.Count == 4;

    private bool IsHighCard() => _cardCountDictionary.Count == 5;


    private static Dictionary<char, int> InitializeCardCountDict(string allHandCards)
        => allHandCards
            .Distinct()
            .ToDictionary(entry => entry, entry => allHandCards.Count(x => x == entry))
            .OrderByDescending(x => x.Value)
            .ToDictionary(k => k.Key, v => v.Value);

    private Dictionary<char, int> InitializeCardCountDictWithJoker(string allHandCars)
    {
        var numberOfJokers = allHandCars.Count(x => x == 'J');
        var handWithoutJokers = allHandCars.Replace("J", string.Empty);
        
        var currentHandEvaluation = handWithoutJokers.Distinct()
            .ToDictionary(entry => entry, entry => handWithoutJokers.Count(x => x == entry))
            .OrderByDescending(x => x.Value)
            .ToDictionary(k => k.Key, v => v.Value);

        Console.WriteLine();
        

        throw new NotImplementedException();
    }
    
}