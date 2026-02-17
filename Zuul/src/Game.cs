using System;


class Game
{
    // Private fields
    private Parser parser;
    private Player player; 

    // Constructor
    public Game()
    {
        parser = new Parser();
        player = new Player(); 
        CreateRooms();
    }

	// Initialise the Rooms
	private void CreateRooms()
	{
		// Create the rooms
		Room outside = new Room("outside the main entrance of the university");
		Room theatre = new Room("in a lecture theatre");
		Room pub = new Room("in the campus pub");
		Room lab = new Room("in a computing lab");
		Room office = new Room("in the computing admin office");
		Room attic = new Room("in a dusty attic");
		Room cellar = new Room("in a dark cellar");

		// Initialise room exits
		outside.AddExit("east", theatre);
		outside.AddExit("south", lab);
		outside.AddExit("west", pub);
		outside.AddExit("up", attic);
		outside.AddExit("down", cellar);
		attic.AddExit("down", outside);
		cellar.AddExit("up", outside);
		theatre.AddExit("west", outside);

		pub.AddExit("east", outside);

		lab.AddExit("north", outside);
		lab.AddExit("east", office);

		office.AddExit("west", lab);

		// Create your Items here
		Item key = new Item(5, "a golden key");
		Item book = new Item(3, "a mysterious book");
		Item lamp = new Item(10, "a desk lamp");

		// Add items to the Rooms
		lab.AddItem(key);
		theatre.AddItem(book);
		office.AddItem(lamp);

		// Start game outside
		player.CurrentRoom = outside;
	}

	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		bool finished = false;
		while (!finished)
		{
			// Check if player is still alive
			if (!player.IsAlive())
			{
				Console.WriteLine("You are dead! Game Over.");
				finished = true;
			}
			else
			{
				Command command = parser.GetCommand();
				finished = ProcessCommand(command);
			}
		}
		Console.WriteLine("Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	}

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");										
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine(player.CurrentRoom.GetLongDescription()); 
	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if(command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
		}

		switch (command.CommandWord)
		{
			case "help":
				PrintHelp();
				break;
			case "go":
				GoRoom(command);
				break;
			case "look":
				Console.WriteLine(player.CurrentRoom.GetLongDescription());
				break;
			case "status":
				Console.WriteLine(player.GetHealth());
				Console.WriteLine("\nBackpack:");
				Console.WriteLine(player.GetInventory().Show());
				break;
			case "take":
				Take(command);
				break;
			case "drop":
				Drop(command);
				break;
			case "inventory":
				Console.WriteLine(player.GetInventory().GetItems());
				break;
			case "use":
				UseItem(command);
				break;
			case "quit":
				wantToQuit = true;
				break;
		}
		return wantToQuit;
	}

	// ######################################
	// implementations of user commands:
	// ######################################
	
	// Geef hulp informatie
	private void PrintHelp()
	{
		Console.WriteLine("Je bent verdwaald. Je bent alleen.");
		Console.WriteLine("Je loopt rond op de universiteit.");
		Console.WriteLine();
		Console.WriteLine("Beschikbare commando's:");
		// Laat de parser de commando's zien
		parser.PrintValidCommands();
	}

	private void PrintLook()
	{
	
	}



	private void GoRoom(Command command)
	{
		if(!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;

		// Try to go to the next room.
		Room nextRoom = player.CurrentRoom.GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to "+direction+"!");
			return;
		}

		player.CurrentRoom = nextRoom;
		player.Damage(10);
		Console.WriteLine("You take damage! " + player.GetHealth());
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
		
		// Check if player reached the office (winning condition)
		string roomDesc = player.CurrentRoom.GetShortDescription();
		if (roomDesc == "in the computing admin office")
		{
			Console.WriteLine("You reached the office! You win!");
		}
	}

	// Pak een item uit de kamer en doe het in je inventaris
	private void Take(Command command)
	{
		// Controleer of je hebt gezegd welk item je wilt pakken
		if(!command.HasSecondWord())
		{
			Console.WriteLine("Pak wat?");
			return;
		}

		// Haal de naam van het item
		string itemName = command.SecondWord;
		
		// Vraag de kamer of het item daar ligt
		Item item = player.CurrentRoom.GetItem(itemName);

		// Als het item niet bestaat, zeg het tegen de speler
		if (item == null)
		{
			Console.WriteLine("Dat item is hier niet!");
			return;
		}

		// Probeer het item in je inventaris te doen
		if (player.GetInventory().Put(itemName, item))
		{
			 // Wat die gepakt heeft en hoeveel ruimte er nog vrij is
			Console.WriteLine("Je hebt gepakt: " + itemName);
			Console.WriteLine("Nog " + player.GetInventory().FreeWeight() + " kilo vrij.");
		}
		else
		{
			// Inventaris is vol! Doe het item terug in de kamer
			player.CurrentRoom.AddItem(item);
			Console.WriteLine("Je kunt dat niet dragen! Je rugzak is vol.");
			Console.WriteLine("Nog " + player.GetInventory().FreeWeight() + " kilo vrij.");
		}
	}

	// Laat een item uit je inventaris vallen
	private void Drop(Command command)
	{
		// Controleer of je hebt gezegd welk item je wilt droppen
		if(!command.HasSecondWord())
		{
			Console.WriteLine("Laat wat vallen?");
			return;
		}

		// Haal de naam van het item
		string itemName = command.SecondWord;
		
		// Vraag je inventaris of je dit item hebt
		Item item = player.GetInventory().Get(itemName);

		// Als je het item niet hebt, zeg het tegen de speler
		if (item == null)
		{
			Console.WriteLine("Je hebt dat niet!");
			return;
		}

		// Voeg het item toe aan de kamer
		player.CurrentRoom.AddItem(item);
		
		// Zeg tegen de speler dat het gelukt is
		Console.WriteLine("Je hebt laten vallen: " + itemName);
		Console.WriteLine("Nog " + player.GetInventory().FreeWeight() + " kilo vrij.");
	}

	// Gebruik een item uit je inventory
	private void UseItem(Command command)
	{
		// Controleer of je hebt gezegd welk item je wilt gebruiken
		if(!command.HasSecondWord())
		{
			Console.WriteLine("Gebruik wat?");
			return;
		}

		// Haal de naam van het item
		string itemName = command.SecondWord;
		
		// Vraag de player om het item te gebruiken
		string result = player.Use(itemName);
		
		// Zeg het resultaat tegen de speler
		Console.WriteLine(result);
	}
	}

