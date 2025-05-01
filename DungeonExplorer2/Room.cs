using DungeonExplorer2.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonExplorer2.Interfaces;
using DungeonExplorer2.Items;
using System.Xml.Linq;

namespace DungeonExplorer2
{
    /// <summary>
    /// Defines a room in the game.
    /// Enter funcition handles the logic for entering the room, including checking if the room is locked,
    /// and interacting with monsters and items.
    /// </summary>
    public class Room
    {
        /// Properties
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsLocked { get; set; }
        public string RequiredKeyName { get; set; }
        public Monster Monster { get; set; }
        public ICollectible Item { get; set; }

        // Constructor
        public Room(string name, string description)
        {
            Name = name;
            Description = description;
            IsLocked = false;
        }

        // Method to enter the room
        public void Enter(Player player)
        {
            Console.Clear();

            // Check if the room is locked
            var key = player.Inventory.OfType<Key>().FirstOrDefault(k => k.Name == RequiredKeyName);
            if (key == null && IsLocked == true) // if its locked and the user doesn't have the key they cant enter and they skip that room
            {
                Console.WriteLine($"The {Name} is locked. You need the {RequiredKeyName}.");
                Console.WriteLine("You can search the room for a key but instead fina a secret entence to another room");
                Console.WriteLine($"What is in the {Name} will remain a mystery...");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return; //exit the method
            }
            else if (key != null) // if the user has the key they can enter
            {
                Console.WriteLine($"You use the {key.Name} to unlock the {Name}.");
                Console.WriteLine($"The {Name} is now unlocked, but they key got stuck in the lock.\nYou cannot put it back in your inventory.\n");
                IsLocked = false; // Unlock the room
                player.RemoveItem(key); // remove the key from the inventory
            }

            // display room to the user
            Console.WriteLine($"You enter the {Name}:");
            Console.WriteLine(Description);

            if (Monster == null) //if the room has no monster
            {
                Console.WriteLine("The room seems safe, you are free to search it.");
            }
            if (Monster != null && Monster.Health > 0) //if the room has a monster
            {
                Console.WriteLine($"\nA wild {Monster.Name} appears!");
                while (player.Health > 0 && Monster.Health > 0)
                {
                    // ask the user for input
                    Console.WriteLine("Choose an action:");
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine("|  1. Attack            2. Use a health potion   |");
                    Console.WriteLine("|  3. Attempt to flee                            |");
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine("Enter '1', '2', or '3':");
                    string input = Console.ReadLine();

                    // ATTACK METHOS
                    if (input == "1")
                    {
                        player.Attack(Monster);
                        if (Monster.Health > 0)
                            Monster.Attack(player);

                        Console.WriteLine("Press any key to continue this fight...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    // HEALTH POTION METHOD
                    else if (input == "2")
                    {
                        var gotOne = player.Inventory.FirstOrDefault(i => i.Name.Equals("potion", StringComparison.OrdinalIgnoreCase));
                        if (gotOne == null) //if you try and use a potion but dont have one
                        {
                            Console.WriteLine("You feel around for a potion in your pocket but find none...");
                            Console.WriteLine("While you search, the monster attacks you!");
                            Monster.Attack(player); //get attacked by the monster
                        }
                        else // else the user has one
                        {
                            int NumberOfPotions = player.Inventory.Count(i => i.Name.Equals("potion", StringComparison.OrdinalIgnoreCase));
                            Console.WriteLine($"You have {NumberOfPotions} potions in your inventory"); //tell the user how man potions they have
                            Console.WriteLine("Are you sure you want to use a health potion? (y/n)");
                            string useItem = Console.ReadLine();
                            if (useItem.ToLower() == "y")
                            {
                                player.UseItem("Potion"); //use the potion, calls players UseItem method
                            }
                            else
                            {
                                Console.WriteLine("You chose not to use a health potion.");
                            }
                        }

                        Console.WriteLine("Press any key to continue this fight..."); //reatard the fight squence
                        Console.ReadKey();
                        Console.Clear();
                    }
                    // FLEE METHOD
                    else if (input == "3")
                    {
                        bool fleeSuccess = Monster.AttemptToFlee(); // call the monster flee method, returns true if successful
                        if (fleeSuccess)
                        {
                            Console.WriteLine($"You successfully fled the {Name}!");
                            break; // exit the loop
                        }
                        else
                        {
                            Console.WriteLine($"While trying to escape {Monster.Name} attacked you!");
                            Monster.Attack(player); // get attacked by the monster

                            Console.WriteLine("Press any key to continue this fight...");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid action."); //if the user enters something other than 1 or 2
                    }
                }

                // player death message
                if (player.Health <= 0)
                {
                    Console.WriteLine("You died.");
                    Environment.Exit(0);
                }
                // monster death message
                else if (Monster.Health <= 0)
                {
                    Console.WriteLine($"You defeated the {Monster.Name}!");
                    Monster = null;
                    Console.WriteLine($"With the Monster dead you can search the room!");
                }
                else if (Monster.Health <= 15)
                {
                    Monster = null;
                    if (Item != null)
                    {
                        Console.WriteLine($"On your way out the room you spot something...");
                        Console.WriteLine($"You act quickly!.");
                    }
                }
            }
            // once the monster is dead or if there was no monster
            // if the room has an item
            if (Item != null)
            {
                player.AddItem(Item); // add the item to the players inventory
                Item = null;
            }
            else
            {
                Console.WriteLine("You rummage around but find nothing of worth."); // message if the room has no item
            }
        }
    }
}