using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatQuest.Abilities
{
    class Berserk : Ability
    {
        public Berserk() : base(5, true, "Berserk", "A powerful attack that damages you in the process")
        {
        }

        public override void Activate(Entity attacker, Entity defender)
        {
            attacker.TakeDamage((int)((attacker.Atk/3) + attacker.Def));
            defender.TakeDamage(attacker.Atk * 2);
        }
    }
}
