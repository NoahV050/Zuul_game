using System;

class Parser
{
	// Bevat alle geldige commando woorden
	private readonly CommandLibrary commandLibrary; 

	// Constructor
	public Parser()
	{
		commandLibrary = new CommandLibrary();
	}

	// Vraag en interpreteer de invoer van de gebruiker. Geef een Command object terug.
	public Command GetCommand()
	{
		Console.Write("> "); // druk prompt af

		string word1 = null;
		string word2 = null;
		string word3 = null;

		// string.Split() geeft een array terug
		string[] words = Console.ReadLine().Split(' ');
		if (words.Length > 0) { word1 = words[0]; }
		if (words.Length > 1) { word2 = words[1]; }
		if (words.Length > 2) { word3 = words[2]; }

		// Controleer nu of dit woord bekend is. Zo ja, maak dan een commando mee.
		if (commandLibrary.IsValidCommandWord(word1)) {
			return new Command(word1, word2, word3);
		}

		// Zoniet, maak een "null" commando (voor onbekend commando).
		return new Command(null, null, null);
	}

	// Print een lijst van geldige commando woorden uit commandLibrary.
	public void PrintValidCommands()
	{
		Console.WriteLine("Your command words are:");
		Console.WriteLine(commandLibrary.GetCommandsString());
	}
}
