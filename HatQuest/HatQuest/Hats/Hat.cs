﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HatQuest.Init;

namespace HatQuest.Hats
{
    enum HatRarity { Common, Uncommon, Rare, Epic, Developer };

    class Hat
    {
        //Fields
        protected int maxHealth;
        protected int def;
        protected int atk;
        protected int maxMana;
        protected string name;
        protected string description;
        protected HatRarity rarity;
        protected Texture2D texture;
        protected Ability ability;

        //Properties
        public String Name
        {
            get
            {
                return name;
            }
        }

        public HatRarity Rarity
        {
            get
            {
                return rarity;
            }
        }

        /// <summary>
        /// Hats modify the stats of the Entity it's equipped to, but you must call the Equip method to actually apply
        /// the modifications
        /// </summary>
        /// <param name="texture">The Hat's texture</param>
        /// <param name="maxHealth">Any modification to the equipped Entity's maximum health</param>
        /// <param name="def">Any modification to the equipped Entity's defense</param>
        /// <param name="atk">Any modification to the equipped Entity's attack</param>
        /// <param name="maxMana">If the Entity's a player, this modifies their maximum mana</param>
        public Hat(string name, string description, Texture2D texture, HatRarity rarity, Ability ability = null, int maxHealth = 0, int def = 0, int atk = 0, int maxMana = 0)
        {
            this.texture = texture;
            this.maxHealth = maxHealth;
            this.def = def;
            this.atk = atk;
            this.maxMana = maxMana;
            this.name = name;
            this.description = description;
            this.rarity = rarity;
            this.ability = ability;
        }

        /// <summary>
        /// Equips the Hat to an Entity and modifies their stats accordingly
        /// </summary>
        /// <param name="entity">The Entity that the Hat will be equipped to</param>
        public virtual void Equip(Entity entity)
        {
            if (entity is Player)
            {
                ((Player)entity).MaxMP += maxMana;
                ((Player)entity).CurrentMP += maxMana;
            }
            entity.MaxHealth += maxHealth;
            entity.Health += maxHealth;
            entity.Def += def;
            entity.Atk += atk;
            entity.Hats.Add(this);
        }

        /// <summary>
        /// Draws the hat
        /// </summary>
        /// <param name="sb">The SpriteBatch that draws the hat</param>
        public void Draw(SpriteBatch sb, Entity wearer, int hatNumber)
        {
            sb.Draw(texture, 
                    new Rectangle(new Point(wearer.Position.Location.X, (wearer.Position.Location.Y - 50) - (50 * hatNumber)), 
                                  new Point(100, 75)), 
                    Color.White);
        }
    }
}
