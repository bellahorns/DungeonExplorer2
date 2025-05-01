using DungeonExplorer2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer2.Characters
{
    /// <summary>
    /// Represents a base class for all creatures in the game. (player and monsters)
    /// uses the IDamageable interface to provide shared functionality for all creatures, i.e. how they take damage and attack.
    /// </summary>
    public abstract class Creature : IDamageable
    {
        public string Name { get; set; }
        public int Health { get; protected set; }
        public int Strength { get; protected set; }

        public Creature(string name, int health, int strength)
        {
            Name = name;
            Health = health;
            Strength = strength;
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
        }

        public abstract void Attack(Creature target);
    }
}