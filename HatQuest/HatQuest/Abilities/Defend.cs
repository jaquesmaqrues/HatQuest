using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatQuest.Abilities
{
    class Defend:Ability
    {
        public Defend() : base(0, false, "Defend")
        {
        }

        public override void Activate(Entity attacker, Entity defender)
        {
            if (((Player)attacker).CurrentMP + 5 < ((Player)attacker).MaxMP)
            {
                ((Player)attacker).CurrentMP += 5;
            }
            else
            {
                ((Player)attacker).CurrentMP = ((Player)attacker).MaxMP;
            }
        }
    }
}
