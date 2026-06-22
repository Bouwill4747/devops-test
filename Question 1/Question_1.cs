using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        List<Item> items = new List<Item>();
	
        // On ajoute 1 table, 2 paddles et 3 paquets de balles
        items.Add(new Table());
        items.Add(new Paddle());
        items.Add(new Paddle());
        items.Add(new Balls());
        items.Add(new Balls());
	    items.Add(new Balls());

        // On parcourt chaque item et on affiche son nom et son prix avec taxe
        foreach (Item item in items)
        {
            Console.WriteLine($"{item.Name}: ${item.getFullPrice():F2}");
        }
                
        Console.ReadLine();

    }

}

public abstract class Item
{
    protected string name;
    protected int price;
    protected float weight;
    // Constructeur de base: chaque sous-classe envoie ses valeurs ici
    public Item(string name, int price, float weight)
    {
        this.name = name;
        this.price = price;
        this.weight = weight;
    }

    public abstract float getFullPrice();

    public float Weight { get { return weight; } }
    public string Name { get { return name; } }
}

public class Table : Item
{
    // Envoie le nom, le prix et le poids au constructeur de base
    public Table() : base("Table", 100, 50) { }
     // Retourne le prix avec une taxe de 20% (prix x 1.20)
    public override float getFullPrice()
    {
        return price * 1.2f;
    }
}

public class Paddle : Item
{
    public Paddle() : base("Paddle", 10, 1) { }

    public override float getFullPrice()
    {
        return price * 1.2f;
    }
}

public class Balls : Item
{
    public Balls() : base("Pack of Balls", 2, 0.5f) { }

    public override float getFullPrice()
    {
        return price * 1.2f;
    }
}


