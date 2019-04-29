using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
//Iain
namespace HatQuest.Abilities
{
    class QuickAttack : Ability
    {
        Random r;

        /// <summary>
        /// An ability that lets you do a random amount of damage, twice
        /// </summary>
        /// <param name="user">The Entity that has the ability</param>
        public QuickAttack(Entity user) : base(5, true, "Quick Attack", "A series of light attacks whose damage is determined by chance", user, Color.LightSlateGray)
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
