using DungeonExplorer2.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using DungeonExplorer2.Items;
using DungeonExplorer2;

namespace DungeonExplorer2
{
    /// <summary>
    /// The main game class that handles the game loop and player interactions.
    /// </summary>
    public static class Game
    {
        /// Starts the game and initializes the player and dungeon.
        public static void Start()
        {
            // Display the welcome message and prompt for player name
            Console.WriteLine("Welcome to Dungeon Explorer!");
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            //validate name input
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.Write("Please enter a valid name: ");
                name = Console.ReadLine();
            }

            //instantiate player with provided name
            Player player = new Player(name);
            //create a list of rooms for the dungeon and provide descriptions, mosnters, and items
            List<Room> dungeon = new List<Room>
            {
                new Room("Spawn Room", "A cold, damp stone chamber with a torch flickering on the wall."),
                new Room("Cells", "Iron bars, dark corners and very bad vibes.")
                {
                    Monster = new Zombie(),
                    Item = new Key("Library Key")
                },
                new Room("Vampire Nest", "This place is filled with coffins and cobwebs. It reeks of old blood.")
                {
                    Monster = new Vampire(),
                    Item = new Potion()
                },
                new Room("Library", "Shelves of dusty books in an ancient language you have no hope of reading.")
                {
                    IsLocked = true,
                    RequiredKeyName = "Library Key",
                    Monster = new Ghost(),
                    Item = new Potion()
                },
                new Room("Armoury", "Rusted weapons and broken armor lie scattered about.")
                {
                    Item = new Weapon("Rusty Sword", 6)
                },
                new Room("Office", "An ornate desk and some paperwork, perhaps this belonged to a warden.")
                {
                    Item = new Potion()
                },
                new Room("Witch's Lair", "The air smells bad and jars of mysterious things line the walls.\nBut there's a window! You can see sunlight and a door out of this place...")
                {
                    Monster = new Witch()
                },
            };

            //tell player their starting stats
            Console.WriteLine($"You are {player.Name}, a brave adventurer with {player.Health} health and {player.Strength} strength.");
            Console.WriteLine("You have a maximum inventory size of 5 items.");
            Console.WriteLine("Follow the prompts to interact with the game.");
            Console.WriteLine("Good Luck! Press any key to start the game...");
            Console.ReadKey();
            Console.Clear();

            //iterate through the rooms in the dungeon
            foreach (Room room in dungeon)
            {
                // allow the player to enter the room and interact with it
                room.Enter(player);

                // prompt the player to view their stats before entering the next room
                Console.WriteLine("Before you enter the next room, would you like to view your stats? (y/n)");
                string viewStats = Console.ReadLine();
                if (viewStats.ToLower() == "y")
                {
                    player.DisplayStats();
                }
                else
                {
                    Console.WriteLine("You choose not to view your stats.");
                }

                //wait for the player to press a key before entering the next room
                Console.WriteLine("\nPress any key to enter the next room...");
                Console.ReadKey();
            }

            // congratulatory message when the player escapes
            Console.WriteLine("You escape the dungeon! Congratulations!");
        }
    }
}