using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
//Iain
namespace HatQuest.Abilities
{
    class LifeSiphon : Ability
    {
        /// <summary>
        /// An ability that drains your opponents life
        /// </summary>
        /// <param name="user">The Entity that has the ability</param>
        public LifeSiphon(Entity user) : base(3, true, "Life Siphon", "An attack that drains the life of your enemies", user, Color.LawnGreen)
        {

        }

        public override void Activate(Entity target)
        {
            target.TakeDamage(user.Atk / 2);
            if(user.Health + user.Atk < user.MaxHealth)
            {
                user.Health += user.Atk;
            }
            else
            {
                user.Health = user.MaxHealth;
            }
        }
    }
}
