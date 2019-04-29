using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HatQuest.Init;
using HatQuest.Effects;
using Microsoft.Xna.Framework;
//Iain
namespace HatQuest.Abilities
{
    class Defend:Ability
    {
        /// <summary>
        /// An ability that temporarily increases your defense, and also gives you mana if you have a bucket hat
        /// </summary>
        /// <param name="user">The Entity that has the ability</param>
        public Defend(Entity user) : base(0, false, "Defend", "You defend, like a self-preserving nerd", user, Color.LightYellow)
        {
        }

        public override void Activate(Entity target)
        {
            if (user.Hats.Contains(HatsDirectory.BUCKETHAT))
            {
                if (((Player)user).CurrentMP + 5 < ((Player)user).MaxMP)
                {
                    ((Player)user).CurrentMP += 5;
                }
                else
                {
                    ((Player)user).CurrentMP = ((Player)user).MaxMP;
                }
            }
            if (user.Hats.Contains(HatsDirectory.KNIGHTHAT))
            {
                new DefendEffect(user, user.Def / 2);
            }
            else
            {
                new DefendEffect(user, user.Def / 3);
            }
        }


    }
}
