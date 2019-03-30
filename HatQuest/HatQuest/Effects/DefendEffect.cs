using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatQuest.Effects
{
    class DefendEffect: StatusEffect
    {
        //fields
        private int defenseGiven;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="target">Target of the effect</param>
        /// <param name="defenseGiven">The ammount of defense given to the target</param>
        public DefendEffect(Entity target, int defenseGiven): base(target)
        {
            this.defenseGiven = defenseGiven;
        }

        protected override void Apply()
        {
            target.Def += defenseGiven;
            base.Apply();
            target.TurnStartEvent += Trigger;
        }

        protected override void Trigger()
        {
            target.Def -= defenseGiven;
            Remove();
        }

        //Used to hook up to the target's TurnStartEvent
        protected void Trigger(Entity attacker, Entity defender)
        {
            Trigger();
        }
    }
}
