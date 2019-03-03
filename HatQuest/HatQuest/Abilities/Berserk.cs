using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatQuest.Abilities
{
    class Berserk : Ability
    {
        public Berserk() : base(5, true)
        {
        }

        public override void Activate(Entity attacker, Entity defender)
        {
            attacker.TakeDamage((int)((attacker.Atk + attacker.Def) / 3));
            defender.TakeDamage(attacker.Atk * 2);
        }
    }
}
