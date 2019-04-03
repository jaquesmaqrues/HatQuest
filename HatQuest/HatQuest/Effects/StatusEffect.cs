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
        protected string name;

        /// <summary>
        /// Abstract constructor. All child constructors need to call the Apply() method
        /// </summary>
        /// <param name="target">Target the effect is being applied to</param>
        public StatusEffect(Entity target)
        {
            this.target = target;
            name = "Name";
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

        //Events should override at least one of these events to cause the 
        protected virtual void Trigger(Entity attacker, Entity Defender) { }
        protected virtual void Trigger(Entity defender, int damage) { }

        /// <summary>
        /// Returns the text to display in the entity status box
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name;
        }
    }
}
