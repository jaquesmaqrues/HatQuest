using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatQuest.Abilities
{
    class Abduct : Ability
    {
        Random r;
        public Abduct() : base(10, true, "Abduct")
        {
            r = new Random(30);
        }

        public override void Activate(Entity Attacker, Entity Defender)
        {
            if(Attacker.Atk + r.Next(0, 101) >= 75)
            {
                Defender.Health = 1;
            }
        }
    }
}
