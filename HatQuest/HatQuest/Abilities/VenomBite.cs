using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HatQuest.Effects;
using Microsoft.Xna.Framework;

namespace HatQuest.Abilities
{
    /// <summary>
    /// Elijah
    /// </summary>
    class VenomBite : Ability
    {
        public VenomBite(Entity user) : base(3, true, "Venom Bite", "A quick stab with a poisoned weapon", user, Color.Purple)
        {

        }

        public override void Activate(Entity target)
        {
            target.TakeDamage((int)(user.Atk * 0.25));
            new PoisonEffect(target, Math.Max(user.Atk / 5, 1));
        }
    }
}
