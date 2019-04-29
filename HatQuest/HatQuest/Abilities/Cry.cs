using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HatQuest.Hats;
using Microsoft.Xna.Framework;
//Iain
namespace HatQuest.Abilities
{
    class Cry:Ability
    {
        /// <summary>
        /// An ability that gives you health if you have the bucket hat
        /// </summary>
        /// <param name="user">The Entity that has the ability</param>
        public Cry(Entity user) : base(0, false, "Cry", "You cry, like a loser", user, Color.LightBlue)
        {
        }

        public override void Activate(Entity target)
        {
            foreach ( Hat h in user.Hats)
            {
                if (h.Name == "Bucket Hat")
                {
                    if (user.Health + 5 < user.MaxHealth)
                    {
                        user.Health += 5;
                    }
                    else
                    {
                        user.Health = user.MaxHealth;
                    }
                }
            }
        }
    }
}
