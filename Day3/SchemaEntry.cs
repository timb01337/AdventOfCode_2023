namespace Day3;

public class SchemaEntry
{
    private string _entry = string.Empty;
    
    public (int startICoordinate, int startJCoordinate) Start { get; set; }
    
    public (int endICoordinate, int endJCoordinate)? End { get; set; }

    public string Entry
    {
        get => _entry;
        set
        {
            _entry = value;
            if (value == "*")
                IsGear = true;
        }
    }

    public bool IsGear { get; private set; } = false;
}