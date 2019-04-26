using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace HatQuest.Abilities
{
    class Abduct : Ability
    {
        Random r;
        public Abduct(Entity user) : base(10, true, "Abduct", "You have a small chance to abduct an enemy, leaving them with 1 health", user, Color.LimeGreen)
        {
            r = new Random(30);
        }

        public override void Activate(Entity target)
        {
            if(user.Atk + r.Next(0, 101) >= 75)
            {
                target.TakeDamage((target.Health/2) + target.Def);
            }
        }
    }
}
