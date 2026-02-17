class Player
{
    // fields
    private int health;
    private Inventory inventory;
    
    // auto property
    public Room CurrentRoom { get; set; }
    
    // constructor
    public Player()
    {
        health = 100;
        CurrentRoom = null;
        inventory = new Inventory(10); // Max weight 10 kilo
    }
// methods
public void Damage(int amount)
{
health = health - amount;
}

public void Heal(int amount)
{
health = health + amount;
}

public bool IsAlive()
{
return health > 0;
}

    public string GetHealth()
    {
        return "Health: " + health;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    // Pak een item uit de kist van de kamer
    public bool TakeFromChest(string itemName, Room room)
    {
        // Stap 1: Haal item uit de kist
        Item item = room.Chest.Get(itemName);
        
        // Stap 2: Kijk of het item echt bestaat
        if (item == null)
        {
            return false; // Item bestaat niet
        }
        
        // Stap 3: Probeer het in je backpack te doen
        bool success = inventory.Put(itemName, item);
        
        // Stap 4: Als het niet paste, zet het terug in de kist
        if (!success)
        {
            room.Chest.Put(itemName, item);
            return false; // Te zwaar!
        }
        
        return true; // Succes!
    }

    // Zet een item uit je backpack in de kist van de kamer
    public bool DropToChest(string itemName, Room room)
    {
        // Stap 1: Pak het item uit je backpack
        Item item = inventory.Get(itemName);
        
        // Stap 2: Kijk of je het item echt had
        if (item == null)
        {
            return false; // Je hebt het niet
        }
        
        // Stap 3: Zet het in de kist
        room.Chest.Put(itemName, item);
        return true; // Succes!
    }

    // Gebruik een item uit je inventory
    public string Use(string itemName)
    {
        // Stap 1: Pak het item uit je inventory
        Item item = inventory.Get(itemName);
        
        // Stap 2: Controleer of je het item hebt
        if (item == null)
        {
            return "Je hebt " + itemName + " niet!";
        }
        
        // Stap 3: Je hebt het item nu gebruikt (verwijderd)
        // Stap 4: Geef feedback
        return "Je hebt gebruikt: " + itemName;
    }
}
