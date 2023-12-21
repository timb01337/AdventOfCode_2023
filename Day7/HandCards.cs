namespace Day7;

public class HandCards
{
    private Dictionary<char, int> _cardCountDictionary;
    
    private static Dictionary<char, string> _cardValues = new()
    {
        {'2', "10"},
        {'3', "11"},
        {'4', "12"},
        {'5', "13"},
        {'6', "14"},
        {'7', "15"},
        {'8', "16"},
        {'9', "17"},
        {'T', "18"},
        {'J', "19"},
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
        OrderedHandValue = long.Parse(_cardValues[allHandCards[0]] + _cardValues[allHandCards[1]] + 
                                      _cardValues[allHandCards[2]] + _cardValues[allHandCards[3]] +
                                      _cardValues[allHandCards[4]]);
        
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
}