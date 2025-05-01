using DungeonExplorer2.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer2.Interfaces
{
    /// <summary>
    /// define the ICollectible interface. To be used for items that can be collected by the player.
    /// </summary>
    public interface ICollectible
    {
        string Name { get; }
        void Use(Player player);
    }
}
