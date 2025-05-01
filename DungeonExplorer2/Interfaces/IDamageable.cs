using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer2.Interfaces
{
    /// <summary>
    /// Defines the IDamageable interface. To be used for all objects that can take damage. Creature class implements this interface.
    /// </summary>
    public interface IDamageable
    {
        void TakeDamage(int damage);
    }
}
