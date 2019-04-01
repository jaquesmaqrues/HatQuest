using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatQuest.Abilities
{
    class LifeSiphon : Ability
    {
        public LifeSiphon(Entity user) : base(3, true, "Life Siphon", "An attack that drains the life of your enemies", user)
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
