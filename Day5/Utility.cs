namespace Day5;

public class SeedScope
{
    private SeedScope(long startOfScope, long lengthOfScope)
    {
        StartOfScope = startOfScope;
        LengthOfScope = lengthOfScope;
        EndOfScope = startOfScope + lengthOfScope - 1;
    }
    
    public static SeedScope CreateSeedScope(long startOfScope, long endOfScope) 
        => new(startOfScope, endOfScope - startOfScope + 1);

    public long StartOfScope { get; private set; }
    public long EndOfScope { get; private set; }
    public long LengthOfScope { get; private set; }
}

public class Map
{
    public Map(string name, long destinationStart, long sourceStart, long range)
    {
        Name = name;
        DestinationStart = destinationStart;
        SourceStart = sourceStart;
        Range = range;
    }
    public string Name { get; set; }
    public long DestinationStart { get; set; } 
    public long SourceStart { get; set; }
    public long Range { get; set; }
    public long DestinationEnd => DestinationStart + Range - 1;
    public long SourceEnd => SourceStart + Range - 1;
    public long Offset => DestinationStart - SourceStart;
}