using System.Collections.Generic;

class CommandLibrary
{
	// Een Lijst die alle geldige commando woorden bevat
	private readonly List<string> validCommands;

	// Constructor - initialiseer de commando woorden.
	public CommandLibrary()
	{
		validCommands = new List<string>();

		validCommands.Add("help");
		validCommands.Add("go");
		validCommands.Add("quit");
		validCommands.Add("look");
		validCommands.Add("up");
		validCommands.Add("down");
		validCommands.Add("status");
		validCommands.Add("take");
		validCommands.Add("drop");
		validCommands.Add("inventory");
		validCommands.Add("use");
	}

	// Controleer of een gegeven string een geldig commando woord is.
	// Geef true terug als het is, false als het niet is.
	public bool IsValidCommandWord(string instring)
	{
		return validCommands.Contains(instring);
	}

	// Geeft een lijst van geldige commando woorden terug als een komma-gescheiden string.
	public string GetCommandsString()
	{
		return String.Join(", ", validCommands);
	}
}
