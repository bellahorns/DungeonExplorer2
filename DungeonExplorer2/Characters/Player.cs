using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DungeonExplorer2.Interfaces;
using DungeonExplorer2.Items;

namespace DungeonExplorer2.Characters
{
    /// <summary>
    /// Represents the player character in the game.
    /// Inherits from the Creature class and implements the IDamageable interface. Defines player-specific properties and methods.
    /// </summary>
    public class Player : Creature
    {
        // define inventory
        public List<ICollectible> Inventory { get; private set; }
        public int MaxInventorySize { get; private set; } = 5;

        public Player(string name) : base(name, 100, 10)
        {
            Inventory = new List<ICollectible>();
        }

        // picking up items 
        public bool AddItem(ICollectible item)
        {
            if (Inventory.Count >= MaxInventorySize) // check if inventory is full
            {
                Console.WriteLine("Your inventory is full.");
                return false; // do not add item
            }
            // add item to inventory
            Inventory.Add(item);
            Console.WriteLine($"After a little searching you found a {item.Name}!");
            Console.WriteLine($"{item.Name} is now in your inventory.");
            return true;
        }

        // removing items from inventory
        public void RemoveItem(ICollectible item)
        {
            Inventory.Remove(item);
            Console.WriteLine($"You discarded {item.Name}.");
        }

        // using items in inventory
        public void UseItem(string name)
        {
            var item = Inventory.FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (item != null && name.Contains("Key")) //if the item is a key use but dont remove it
            {
                item.Use(this);
            }
            else if (item != null) //if the item is not a key use and remove it
            {
                item.Use(this);
                RemoveItem(item);
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }

        // override the attack method to include weapon damage
        public override void Attack(Creature target)
        {
            int totalDamage = Strength;
            var weapon = Inventory.OfType<Weapon>().OrderByDescending(w => w.Damage).FirstOrDefault(); // look for the most powerful weapon
            if (weapon != null)
            {
                totalDamage += weapon.Damage;
                Console.WriteLine($"You use {weapon.Name} for +{weapon.Damage} damage!"); // add weapon damage to the attack
            }

            Console.WriteLine($"You attack {target.Name} for {totalDamage} damage!");
            target.TakeDamage(totalDamage);
            Console.WriteLine($"{target.Name} has {target.Health} health left.");
        }

        // display player stats
        public void DisplayStats()
        {
            // calculate total strength including weapon damage (if any)
            int totalStrength = Strength;
            var weapon = Inventory.OfType<Weapon>().OrderByDescending(w => w.Damage).FirstOrDefault();
            if (weapon != null)
            {
                totalStrength += weapon.Damage;
            }

            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Health: {Health}");
            Console.WriteLine($"Strength: {totalStrength}");

            // display inventory size and items
            Console.WriteLine($"Inventory Size: {Inventory.Count}/{MaxInventorySize}");
            if (Inventory.Count != 0)
            {
                // display inventory items
                Console.WriteLine("Inventory:");
                foreach (var item in Inventory)
                {
                    Console.WriteLine($"- {item.Name}");
                }

                // ask if the player wants to use an item
                Console.WriteLine("Would you like to use any of your items? (y/n)");
                string input = Console.ReadLine();
                if (input.ToLower() == "y") //use item if the user wants to
                {
                    Console.WriteLine("Enter the name of the item you want to use:");
                    string itemName = Console.ReadLine();
                    UseItem(itemName);
                }
                else //if the user does not want to use an item game continues
                {
                    Console.WriteLine("You choose not to use any items.");
                }
            }
        }
    }
}