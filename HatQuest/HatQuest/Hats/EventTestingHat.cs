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
        public EventTestingHat(string name, string description, Texture2D texture, HatRarity rarity, Ability ability = null, int maxHealth = 0, int def = 0, int atk = 0, int maxMana = 0) : 
                          base(name, description, texture, rarity, ability, maxHealth, def, atk, maxMana)
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
