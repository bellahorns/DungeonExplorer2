using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using DungeonExplorer2.Characters;
using DungeonExplorer2.Items;

namespace DungeonExplorer2.Tests
{
    /// <summary>
    /// This class contains unit tests for the Dungeon Explorer game.
    /// </summary>
    public static class DungeonExplorerTests
    {
        public static void RunTests()
        {
            Player testPlayer = new Player("Tester");
            //test player can take damage
            int startHealth = testPlayer.Health;
            testPlayer.TakeDamage(20);
            Debug.Assert(testPlayer.Health == startHealth - 20, "TakeDamage should reduce health.");

            //test player can pick up items
            testPlayer.AddItem(new Potion());
            Debug.Assert(testPlayer.Inventory.Count == 1, "Item should be added to inventory.");

            //test player can use items and remove them from inventory
            testPlayer.UseItem("Potion");
            Debug.Assert(testPlayer.Inventory.Count == 0, "Item should be removde after use.");

            //test player can pick up weapons
            testPlayer.AddItem(new Weapon("Test Sword", 10));
            Debug.Assert(testPlayer.Inventory.Exists(i => i.Name == "Test Sword"), "Weapon should be added.");

            Console.WriteLine("All tests passed.");
            Console.WriteLine("Press any key to continue to game start...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}