class Command
{
	public string CommandWord { get; init; }
	public string SecondWord { get; init; }
	public string ThirdWord { get; init; }
	
	// Maak een command object. Eerste, tweede en derde woord moeten geleverd worden, maar
	// een ervan (of alle) kunnen null zijn. Zie Parser.GetCommand()
	public Command(string first, string second, string third)
	{
		CommandWord = first;
		SecondWord = second;
		ThirdWord = third;
	}

	
	// Geef true terug als dit commando niet begrepen was.
	public bool IsUnknown()
	{
		return CommandWord == null;
	}

	
	// Geef true terug als het commando een tweede woord heeft.
	public bool HasSecondWord()
	{
		return SecondWord != null;
	}

	// Geef true terug als het commando een derde woord heeft.
	public bool HasThirdWord()
	{
		return ThirdWord != null;
	}
}
