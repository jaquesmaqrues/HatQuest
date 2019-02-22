using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatQuest.Abilities
{
    class Attack: Ability
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Attack()
        {
            //Since the mana cost isnt defined in the Ability class 
            //  it has to be first defined in the constructor
            manaCost = 0;
        }

        public override void Activate(Entity attacker, Entity defender)
        {
            defender.TakeDamage(attacker.Atk);
        }
    }
}
