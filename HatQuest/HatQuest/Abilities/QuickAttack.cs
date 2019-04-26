using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace HatQuest.Abilities
{
    class QuickAttack : Ability
    {
        Random r;

        public QuickAttack(Entity user) : base(5, true, "Quick Attack", "A series of light attacks whose damage is determined by chance", user, Color.LightGray)
        {
            this.r = new Random(50);
        }

        public override void Activate(Entity target)
        {
            target.TakeDamage(user.Atk/3 + r.Next(-6, 5));
            target.TakeDamage(user.Atk/3 + r.Next(-6, 5));
        }
    }
}
