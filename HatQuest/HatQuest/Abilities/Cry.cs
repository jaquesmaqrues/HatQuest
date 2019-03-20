using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HatQuest.Hats;

namespace HatQuest.Abilities
{
    class Cry:Ability
    {
        public Cry() : base(0, false, "Cry", "You cry, like a loser")
        {
        }

        public override void Activate(Entity attacker, Entity defender)
        {
            foreach ( Hat h in attacker.Hats)
            {
                if (h.Name == "Bucket Hat")
                {
                    if (attacker.Health + 5 < attacker.MaxHealth)
                    {
                        attacker.Health += 5;
                    }
                    else
                    {
                        attacker.Health = attacker.MaxHealth;
                    }
                }
            }
        }
    }
}
