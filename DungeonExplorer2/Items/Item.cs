using DungeonExplorer2.Characters;
using DungeonExplorer2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer2.Items
{
    /// <summary>
    /// Item class defined as an abstract class.
    /// subclasses define specific item types.
    /// </summary>
    public abstract class Item : ICollectible
    {
        public abstract string Name { get; } //get name
        public abstract void Use(Player player); //use the item
    }

    /// Potion class
    public class Potion : Item
    {
        public override string Name => "Potion";

        public override void Use(Player player)
        {
            Console.WriteLine("You drink the potion and regain 20 health.");
            int MaxHealth = 100;
            player.TakeDamage(-20);
            if (player.Health > MaxHealth)
            {
                int ToRemove = player.Health - MaxHealth;
                player.TakeDamage(ToRemove);
            }
            else
            {
                Console.WriteLine($"You are now at {player.Health} health.");
            }
        }
    }

    /// Key class
    public class Key : Item
    {
        public override string Name { get; }

        public Key(string name)
        {
            Name = name;
        }

        public override void Use(Player player)
        {
            Console.WriteLine($"You wave the key about in the air but there is nothing here to unlock.");
            Console.WriteLine($"You put the {Name} in your inventory.");
        }
    }

    /// Weapon class
    public class Weapon : Item
    {
        public override string Name { get; }
        public int Damage { get; }

        public Weapon(string name, int damage)
        {
            Name = name;
            Damage = damage;
        }

        public override void Use(Player player)
        {
            Console.WriteLine($"You equip the {Name}. It adds +{Damage} to your attacks while in inventory.");
        }
    }
}
