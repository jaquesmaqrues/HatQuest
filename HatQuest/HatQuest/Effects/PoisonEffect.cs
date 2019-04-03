using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatQuest.Effects
{
    /// <summary>
    /// Elijah
    /// </summary>
    class PoisonEffect:StatusEffect
    {
        //Fields
        private int stacks;

        //Properties
        public int Stacks
        {
            get { return stacks; }
            set { stacks = value; }
        }

        public PoisonEffect(Entity target, int stacks):base(target)
        {
            this.stacks = stacks;
            name = "Poison";
            Apply();
        }

        protected override void Apply()
        {
            //Add the stacks of this PoisonEffect to a pre-existing PoisonEffect if one exists on the target
            for(int k = 0; k < target.Effects.Count; k++)
            {
                if(target.Effects[k].GetType() == this.GetType())
                {
                    ((PoisonEffect)target.Effects[k]).Stacks += stacks;
                    return;
                }
            }
            //If there is no PoisonEffect already on the target
            target.TurnStartEvent += Trigger;
            base.Apply();
        }

        protected override void Trigger(Entity attacker, Entity defender)
        {
            target.Health -= stacks;
            stacks--;

            if (stacks < 1)
            {
                Remove();
            }
        }

        protected override void Remove()
        {
            target.TurnStartEvent -= Trigger;
            base.Remove();
        }

        public override string ToString()
        {
            return base.ToString() + " " + stacks;
        }
    }
}
