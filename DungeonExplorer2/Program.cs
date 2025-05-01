using DungeonExplorer2;
using DungeonExplorer2.Tests;
using System;

class Program
{
    /// <summary>
    /// The entry point of the Dungeon Explorer application.
    /// </summary>
    static void Main()
    {
        DungeonExplorerTests.RunTests(); // Optional tests
        Game.Start(); // Start the game loop
    }
}

