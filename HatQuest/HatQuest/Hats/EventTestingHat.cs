using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HatQuest.Hats
{
    class EventTestingHat : Hat
    {
        /// <summary>
        /// A test hat
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="texture"></param>
        /// <param name="rarity"></param>
        /// <param name="ability"></param>
        /// <param name="maxHealth"></param>
        /// <param name="def"></param>
        /// <param name="atk"></param>
        /// <param name="maxMana"></param>
        public EventTestingHat(string name, string description, Texture2D texture, HatRarity rarity, Ability ability = null, int maxHealth = 0, int def = 0, int atk = 0, int maxMana = 0) : 
                          base(name, description, texture, Color.White, rarity, ability, maxHealth, def, atk, maxMana)
        {

        }

        public override void Equip(Entity entity)
        {
            base.Equip(entity);
            entity.DamageEvent += OnDamageEvent;
        }

        public int OnDamageEvent(Entity defender, int damage)
        {
            //makes the defender take exactly 1 damage every time
            return defender.Def + 1 - damage;
        }
    }
}
