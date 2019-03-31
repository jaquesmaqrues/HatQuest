using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatQuest.Abilities
{
    /// <summary>
    /// Elijah
    /// </summary>
    class Attack: Ability
    {
        /// <summary>
        /// The base attack ability
        /// </summary>
        public Attack(Entity user) : base(0, true, "Attack", "A basic attack", user)
        {
        }

        /// <summary>
        /// The attack ability just deals damage equal to the attacker's Atk stat to the defender
        /// </summary>
        /// <param name="attacker">The attacking Entity</param>
        /// <param name="defender">The defending Entity</param>
        public override void Activate(Entity attacker, Entity defender)
        {
            defender.TakeDamage(attacker.Atk);
        }
    }
}
