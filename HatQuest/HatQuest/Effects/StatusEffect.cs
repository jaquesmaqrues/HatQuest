using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatQuest.Effects
{
    abstract class StatusEffect
    {
        //Fields
        protected Entity target;

        /// <summary>
        /// Abstract constructor
        /// </summary>
        /// <param name="target">Target the effect is being applied to</param>
        public StatusEffect(Entity target)
        {
            this.target = target;
            Apply();
        }

        /// <summary>
        /// Apply the StatusEffect to its target
        /// </summary>
        protected virtual void Apply()
        {
            target.Effects.Add(this);
        }

        /// <summary>
        /// Remove the StatusEffect from its target
        /// </summary>
        protected virtual void Remove()
        {
            target.Effects.Remove(this);
        }

        protected abstract void Trigger();
        
    }
}
