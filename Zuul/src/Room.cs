using System.Collections.Generic;

/// <summary>
/// De ROOM klasse - dit vertegenwoordigt een ruimte in het spel
/// Elke room heeft:
/// - Een beschrijving (wat je ziet)
/// - Deuren/uitgangen naar andere kamers
/// - Items die je kunt oppakken
/// </summary>
class Room
{
	// === VELDEN (gegevens van deze kamer) ===
	private string description;           // Wat beschrijft deze kamer? (bijv. "in de keuken")
	private Dictionary<string, Room> exits;  // Dictionary met alle deuren: sleutel=richting, waarde=volgende room
	private List<Item> items;            // Lijst met alle items die in deze kamer liggen

	// === PROPERTY ===
	/// <summary>
	/// De kist van deze kamer - iedereen kan items hier in doen/uit halen
	/// </summary>
	public Inventory Chest { get; }

	/// <summary>
	/// CONSTRUCTOR - wordt aangeroepen als je "new Room(...)" maakt
	/// Dit initialiseert (vult) alle velden met beginwaarden
	/// </summary>
	public Room(string desc)
	{
		description = desc;                    // Sla de beschrijving op
		exits = new Dictionary<string, Room>(); // Maak een lege Dictionary voor deuren
		items = new List<Item>();             // Maak een lege List voor items
		Chest = new Inventory(999999);         // Maak een kist met plaats voor VEEL items (999999)
	}

	/// <summary>
	/// Voeg een DEUR/UITGANG toe aan deze kamer
	/// Voorbeeld: AddExit("norden", anderekamer) = je kunt naar het noorden gaan naar anderekamer
	/// </summary>
	public void AddExit(string direction, Room neighbor)
	{
		// Voeg aan de Dictionary toe: direction (sleutel) -> neighbor (de room waar je heen gaat)
		exits.Add(direction, neighbor);
	}

	/// <summary>
	/// Geef alleen de korte beschrijving van de kamer terug
	/// Voorbeeld output: "in de keuken"
	/// </summary>
	public string GetShortDescription()
	{
		return description; // Geef gewoon de beschrijving terug
	}

	/// <summary>
	/// Geef de VOLLEDIGE beschrijving met alles erop en eraan
	/// Dit wordt getoond aan de speler als hij/zij een kamer betreedt
	/// </summary>
	public string GetLongDescription()
	{
		// Bouw stap voor stap een lange tekst op
		string str = "Je bent ";          // Start met deze tekst
		str += description;                // Voeg de kamer-beschrijving toe
		str += ".\n";                      // Voeg een newline toe (nieuwe regel)
		str += GetExitString();             // Voeg de uitgangen toe
		str += "\n";                       // Voeg een newline toe
		str += GetItemsString();            // Voeg de items toe
		return str;                        // Geef de complete tekst terug
	}

	/// <summary>
	/// Zoek een deur/uitgang in een bepaalde richting
	/// Geeft de volgende Room terug, of null als die richting niet bestaat
	/// </summary>
	public Room GetExit(string direction)
	{
		// Kijk of exits de sleutel "direction" bevat
		if (exits.ContainsKey(direction))
		{
			// Ja! Geef de Room terug die bij deze direction hoort
			return exits[direction];
		}
		// Nee, deze richting bestaat niet
		return null;
	}

	/// <summary>
	/// Helper-methode: maak een tekststring van alle uitgangen
	/// Voorbeeld output: "Uitgangen: norden, zuiden, osten"
	/// </summary>
	private string GetExitString()
	{
		string str = "Uitgangen: ";     // Start met dit label
		str += String.Join(", ", exits.Keys); // Join alle richtingen met ", " ertussen
		return str;                      // Geef de volledige string terug
	}

	/// <summary>
	/// Voeg een ITEM toe aan de items-lijst van deze kamer
	/// Dit item ligt dan in de kamer en kan opgeraapt worden
	/// </summary>
	public void AddItem(Item item)
	{
		items.Add(item); // Voeg het item toe aan de lijst
	}

	/// <summary>
	/// Zoek een item in deze kamer op NAAM en pak het op
	/// Het item wordt uit de kamer VERWIJDERD en teruggegeven
	/// Geeft null terug als het item niet bestaat
	/// </summary>
	public Item GetItem(string itemName)
	{
		// Loop door alle items in deze kamer
		foreach (Item item in items)
		{
			// Kijk of de naam van dit item de zoekterm bevat
			if (item.Description.Contains(itemName))
			{
				// Gevonden! Verwijder het item uit de kamer
				items.Remove(item);
				// Geef het item terug aan wie het opvraagt
				return item;
			}
		}
		// Niet gevonden - geef null terug
		return null;
	}

	/// <summary>
	/// Helper-methode: maak een tekststring van alle items in deze kamer
	/// Voorbeeld output: "Items: zwaard sleutel goudmunt"
	/// </summary>
	public string GetItemsString()
	{
		// Controleer: zijn er items in deze kamer?
		if (items.Count == 0)
		{
			// Nee, geen items
			return "Er zijn geen items hier.";
		}
		// Ja, er zijn items! Maak een string ervan
		string str = "Items: ";
		foreach (Item item in items)
		{
			str += item.Description + " "; // Voeg elk item toe met een spatie
		}
		return str; // Geef de volledige string terug
	}
}
