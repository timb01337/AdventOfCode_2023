using System.Diagnostics;

namespace Day7;

public class HandCards
{
    private Dictionary<char, int> _cardCountDictionary;

    private static Dictionary<char, string> _cardValues = new()
        {{'J', "10"}, {'2', "11"}, {'3', "12"}, {'4', "13"}, {'5', "14"}, {'6', "15"}, {'7', "16"}, {'8', "17"}, {'9', "18"}, {'T', "19"}, {'Q', "20"}, {'K', "21"}, {'A', "22"}};

    public HandCards(string allHandCards, int value)
    {
        Stake = value;
        OrderedHandValue = long.Parse(_cardValues[allHandCards[0]] + _cardValues[allHandCards[1]] +
                                      _cardValues[allHandCards[2]] + _cardValues[allHandCards[3]] +
                                      _cardValues[allHandCards[4]]);

        _cardCountDictionary = allHandCards.Contains('J') ? InitializeCardCountDictWithJoker(allHandCards) : InitializeCardCountDict(allHandCards);
    }
    public int Stake { get; }
    public long OrderedHandValue { get; }

    public int GetStrengthOfHand() => (int)GetHandType();
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

        //with 4 or 5 jokers we can always do a five of a kind
        if (numberOfJokers == 5 || numberOfJokers == 4)
            return new Dictionary<char, int>() {{'J', 5}};
        
        if (numberOfJokers == 3)
        {
            //with three jokers we can still have a "five of a kind"
            if (currentHandEvaluation.First().Value == 2)
                return new Dictionary<char, int>() {{'J', 5}};
            
            //if we've got 3 jokers a "four of a kind" is the least we can get
            if (currentHandEvaluation.Count() == 2)
                return new Dictionary<char, int>() {{'J', 4}, {'2', 1}};
        }

        //with two jokers things get a little bit more tricky
        if (numberOfJokers == 2)
        {
            //we can have 333JJ -> which is a five of a kind
            if (currentHandEvaluation.First().Value == 3)
                return new Dictionary<char, int>() {{'J', 5}};

            //we can have a one pair: 331JJ --> best we can do here is a four of a kind
            if (currentHandEvaluation.First().Value == 2)
                return new Dictionary<char, int>() {{'J', 4}, {'2', 1}};

            //and we can have a high card: 234JJ --> best we can do here is a three of a kind
            if (currentHandEvaluation.Count() == 3)
                return new Dictionary<char, int>() {{'J', 3}, {'2', 1}, {'3', 1}};
        }

        if (numberOfJokers == 1)
        {
            //with one joker we can still get a five of a kind 4444J
            if (currentHandEvaluation.First().Value == 4)
                return new Dictionary<char, int>() {{'J', 5}};

            //we can also get a four of a kind with a setup like this 333J5
            if (currentHandEvaluation.First().Value == 3)
                return new Dictionary<char, int>() {{'J', 4}, {'2', 1}};

            //2233J is a classic full house angle
            if (currentHandEvaluation.First().Value == 2 &&
                currentHandEvaluation.Skip(1).First().Value == 2)
                return new Dictionary<char, int>() {{'J', 3}, {'2', 2}};

            //three of a kind can be achieved with 22J34
            if (currentHandEvaluation.First().Value == 2)
                return new Dictionary<char, int>() {{'J', 3}, {'2', 1}, {'3', 1}};

            //two pairs don't matter even 2233J always will be a fullhouse

            //i do indeed think the last thing i have to consider is a highcard
            //with one joker best we can do is a one pair (which sucks lulw)
            if (currentHandEvaluation.Count() == 4)
                return new Dictionary<char, int>() {{'J', 2}, {'2', 1}, {'3', 1}, {'4', 1}};
        }

        throw new UnreachableException();
    }

    private HandType GetHandType()
    {
        if (IsFiveOfAKind())
            return HandType.FiveOfAKind;
        if (IsFourOfAKind())
            return HandType.FourOfAKind;
        if (IsFullHouse())
            return HandType.FullHouse;
        if (IsThreeOfAKind())
            return HandType.ThreeOfAKind;
        if (IsTwoPair())
            return HandType.TwoPair;
        if (IsOnePair())
            return HandType.OnePair;
        if (IsHighCard())
            return HandType.HighCard;

        throw new UnreachableException();
    }
}

public enum HandType
{
    FiveOfAKind = 700,
    FourOfAKind = 600,
    FullHouse = 500,
    ThreeOfAKind = 400,
    TwoPair = 300,
    OnePair = 200,
    HighCard = 100
}