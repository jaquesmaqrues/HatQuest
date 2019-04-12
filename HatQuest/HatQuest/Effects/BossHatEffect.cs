using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatQuest.Effects
{
    class BossHatEffect : StatusEffect
    {
        Random r;
        public BossHatEffect(Entity target) : base(target)
        {
            r = new Random(5);
            name = "BossHatEffectName";
        }
        protected override void Apply()
        {
            base.Apply();
            target.AttackPreEvent += Trigger;
        }

        protected override void Trigger(Entity attacker, Entity defender)
        {
            if (r.Next(1, 51) == 50)
            {
                defender.Health = 0;
            }
        }

        protected override void Remove()
        {
            target.AttackPreEvent -= Trigger;
            base.Remove();
        }
    }
}
