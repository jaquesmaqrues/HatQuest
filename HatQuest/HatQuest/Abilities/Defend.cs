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

        public override void Activate(Entity target)
        {
            if (user.Hats.Contains(HatsDirectory.BUCKETHAT))
            {
                if (((Player)user).CurrentMP + 5 < ((Player)user).MaxMP)
                {
                    ((Player)user).CurrentMP += 5;
                }
                else
                {
                    ((Player)user).CurrentMP = ((Player)user).MaxMP;
                }
            }
            if (user.Hats.Contains(HatsDirectory.KNIGHTHAT))
            {
                new DefendEffect(user, user.Def / 2);
            }
            else
            {
                new DefendEffect(user, user.Def / 3);
            }
        }


    }
}
