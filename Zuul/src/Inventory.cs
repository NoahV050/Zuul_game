using System;
using System.Collections.Generic;

// De Inventory klasse
// Je kunt items erin doen en eruit halen
class Inventory
{
    // Velden
    private int maxWeight;                              // Hoeveel kilo je max kan dragen
    private Dictionary<string, Item> items;             // Een woordenboek met alle items

    // Constructor - maak een lege rugzak aan
    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        this.items = new Dictionary<string, Item>();
    }

    // Voeg een item toe aan je inventaris
    public bool Put(string itemName, Item item)
    {
        // Stap 1: Controleer of je dit item al hebt
        if (items.ContainsKey(itemName))
        {
            return false;
        }

        // Stap 2: Controleer of het item niet te zwaar is
        if (TotalWeight() + item.Weight > maxWeight)
        {
            return false;
        }

        // Stap 3: Voeg item toe aan je rugzak
        items[itemName] = item;
        return true;
    }

    // Haal een item uit je inventaris
    public Item Get(string itemName)
    {
        // Stap 1: Zoek het item
        if (items.ContainsKey(itemName))
        {
            // Stap 2: Pak het item
            Item item = items[itemName];
            
            // Stap 3: Verwijder het uit je rugzak
            items.Remove(itemName);
            
            // Stap 4: Geef het terug
            return item;
        }

        // Item niet gevonden
        return null;
    }

    // Bereken totaal gewicht van alle items in je rugzak
    public int TotalWeight()
    {
        int total = 0;

        // Loop door alle items en tel hun gewichten op
        foreach (Item item in items.Values)
        {
            total += item.Weight;
        }

        return total;
    }

    // Hoeveel ruimte heb je nog?
    public int FreeWeight()
    {
        return maxWeight - TotalWeight();
    }

    // Geef een lijst van alles wat je draagt
    public string GetItems()
    {
        if (items.Count == 0)
        {
            return "Je inventaris is leeg.";
        }

        string itemList = "Je draagt:\n";
        foreach (string name in items.Keys)
        {
            itemList += "  - " + name + "\n";
        }
        return itemList;
    }

    // Toon alles wat in deze inventory zit (gebruikt voor "status" commando)
    public string Show()
    {
        if (items.Count == 0)
        {
            return "De inventory is leeg.";
        }

        string result = "";
        foreach (string name in items.Keys)
        {
            result += "  - " + name + "\n";
        }
        return result;
    }
}
