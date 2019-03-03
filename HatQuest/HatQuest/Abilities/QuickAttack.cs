using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatQuest.Abilities
{
    class QuickAttack : Ability
    {
        Random r;

        public QuickAttack() : base(5, true, "Quick Attack")
        {
            this.r = new Random(50);
        }

        public override void Activate(Entity attacker, Entity defender)
        {
            defender.TakeDamage(attacker.Atk + r.Next(-6, 5));
            defender.TakeDamage(attacker.Atk + r.Next(-6, 5));
        }
    }
}
