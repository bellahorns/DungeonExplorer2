using DungeonExplorer2.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer2.Characters
{
    /// <summary>
    ///Represents a base class for all monsters in the game.
    ///Inherits from the Creature class and provides shared functionality for all monsters.
    ///Then subclasses define specific monster types.
    /// </summary>
    public abstract class Monster : Creature
    {
        // Constructor
        protected Monster(string name, int health, int strength) : base(name, health, strength) { }

        // the player attempts to flee from the monster
        public virtual bool AttemptToFlee()
        {
            if (Health > 15)
            {
                Console.WriteLine($"{Name} blocks your escape!");
                return false; // the monster is too strong to flee
            }
            else
            {
                Console.WriteLine($"{Name} is too weak to stop you!");
                Console.WriteLine("You escape!");
                return true; // the monster is too weak to stop you
            }
        }
    }


    // subclasses of monster, defines name, health, and strength as well as attack method
    // witch class
    public class Witch : Monster
    {
        // new instance
        public Witch() : base("Witch", 50, 14) { }
        // witch attack method
        public override void Attack(Creature target)
        {
            Console.WriteLine("The Witch casts a dark spell!");
            target.TakeDamage(Strength); // take damage from the target (attack)
            Console.WriteLine($"The Witch's spell leave you with {target.Health} health.");
        }

        // override the flee method
        public override bool AttemptToFlee()
        {
            Console.WriteLine($"{Name} cackles and blocks your escape!");
            return false; // the witch is too strong to flee
        }
    }
    // vampire class
    public class Vampire : Monster
    {
        //new instance
        public Vampire() : base("Vampire", 50, 6) { }
        // vampire attack method
        public override void Attack(Creature target)
        {
            Console.WriteLine("The Vampire bites viciously!");
            target.TakeDamage(Strength); // take damage from the target (attack)
            Console.WriteLine($"The Vampire's bite leaves you with {target.Health} health.");
        }
    }
    // ghost class
    public class Ghost : Monster
    {
        // new instance
        public Ghost() : base("Ghost", 20, 8) { }
        // ghost attack method
        public override void Attack(Creature target)
        {
            Console.WriteLine("The Ghost haunts your soul!");
            target.TakeDamage(Strength); // take damage from the target (attack)
            Console.WriteLine($"The Ghost's haunting leaves you with {target.Health} health.");
        }
    }
    // zombie class
    public class Zombie : Monster
    {
        // new instance
        public Zombie() : base("Zombie", 40, 4) { }
        // zombie attack method
        public override void Attack(Creature target)
        {
            Console.WriteLine("The Zombie swings its decaying arm!");
            target.TakeDamage(Strength); // take damage from the target (attack)
            Console.WriteLine($"The Zombie's swing leaves you with {target.Health} health."); 
        }
    }
}