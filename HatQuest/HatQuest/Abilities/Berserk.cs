using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

// Iain
namespace HatQuest.Abilities
{
    class Berserk : Ability
    {
        /// <summary>
        /// An attack that does extra damage at the price of health
        /// </summary>
        /// <param name="user">The Entity that has the ability</param>
        public Berserk(Entity user) : base(5, true, "Berserk", "A powerful attack that damages you in the process", user, Color.IndianRed)
        {
        }

        public override void Activate(Entity target)
        {
            user.TakeDamage((user.Atk/3) + user.Def);
            target.TakeDamage(user.Atk * 2);
        }
    }
}
