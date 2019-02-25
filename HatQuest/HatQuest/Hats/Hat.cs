using System;
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
    class Hat
    {
        protected int maxHealth;
        protected int def;
        protected int atk;
        protected int maxMana;
        protected Texture2D texture;
        protected Entity entity;
        protected Rectangle position;

        /// <summary>
        /// Hats modify the stats of the Entity it's equipped to, but you must call the Equip method to actually apply
        /// the modifications
        /// </summary>
        /// <param name="texture">The Hat's texture</param>
        /// <param name="maxHealth">Any modification to the equipped Entity's maximum health</param>
        /// <param name="def">Any modification to the equipped Entity's defense</param>
        /// <param name="atk">Any modification to the equipped Entity's attack</param>
        /// <param name="maxMana">If the Entity's a player, this modifies their maximum mana</param>
        public Hat(Texture2D texture, int maxHealth = 0, int def = 0, int atk = 0, int maxMana = 0)
        {
            this.texture = texture;
            this.maxHealth = maxHealth;
            this.def = def;
            this.atk = atk;
            this.maxMana = maxMana;
        }

        /// <summary>
        /// Equips the Hat to an Entity and modifies their stats accordingly
        /// </summary>
        /// <param name="entity">The Entity that the Hat will be equipped to</param>
        public void Equip(Entity entity)
        {
            this.entity = entity;
            position = new Rectangle(entity.Position.Location, new Point(entity.Position.Right, (int)(entity.Position.Y - 0.8 * entity.Position.Height)));
            if (entity is Player)
            {
                ((Player)entity).MaxMP += maxMana;
                ((Player)entity).CurrentMP += maxMana;
            }
            entity.MaxHealth += maxHealth;
            entity.Health += maxHealth;
            entity.Def += def;
            entity.Atk += atk;
        }

        /// <summary>
        /// Draws the hat
        /// </summary>
        /// <param name="sb">The SpriteBatch that draws the hat</param>
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }
    }
}
