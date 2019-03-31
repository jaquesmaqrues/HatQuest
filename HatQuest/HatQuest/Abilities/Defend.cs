using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HatQuest.Init;
using HatQuest.Effects;

namespace HatQuest.Abilities
{
    class Defend:Ability
    {
        public Defend(Entity user) : base(0, false, "Defend", "You defend, like a self-preserving nerd", user)
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
            if (attacker.Hats.Contains(HatsDirectory.KNIGHTHAT))
            {
                new DefendEffect(attacker, attacker.Def / 2);
            }
            else
            {
                new DefendEffect(attacker, attacker.Def / 3);
            }
        }


    }
}
