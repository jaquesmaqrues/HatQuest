using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatQuest.Abilities
{
    class LifeSiphon : Ability
    {
        public LifeSiphon(Entity user) : base(3, true, "Life Siphon", "An attack that drains the life of your enemies", user)
        {

        }

        public override void Activate(Entity attacker, Entity defender)
        {
            defender.TakeDamage((int)(attacker.Atk / 2));
            if(attacker.Health + attacker.Atk < attacker.MaxHealth)
            {
                attacker.Health += attacker.Atk;
            }
            else
            {
                attacker.Health = attacker.MaxHealth;
            }
        }
    }
}
