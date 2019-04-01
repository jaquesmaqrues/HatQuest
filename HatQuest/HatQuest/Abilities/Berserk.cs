using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatQuest.Abilities
{
    class Berserk : Ability
    {
        public Berserk(Entity user) : base(5, true, "Berserk", "A powerful attack that damages you in the process", user)
        {
        }

        public override void Activate(Entity target)
        {
            user.TakeDamage((user.Atk/3) + user.Def);
            target.TakeDamage(user.Atk * 2);
        }
    }
}
