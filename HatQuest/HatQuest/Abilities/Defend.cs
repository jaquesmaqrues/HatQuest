using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HatQuest.Init;

namespace HatQuest.Abilities
{
    class Defend:Ability
    {
        public Defend() : base(0, false, "Defend", "You defend, like a self-preserving nerd")
        {
        }

        public override void Activate(Entity attacker, Entity defender)
        {
            if (attacker.Hats.Contains(HatsDirectory.BUCKETHAT))
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
            attacker.Def += attacker.Def / 3;
        }


    }
}
