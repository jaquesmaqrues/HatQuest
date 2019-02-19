using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/* Iain Davis
 * This is the Base Entity class that the player and enemies are children of
 * There are no known issues */

namespace HatQuest
{
    class Entity
    {
        protected Texture2D texture;
        protected Rectangle position;
        protected int currentHealth;
        protected int maxHealth;
        protected int def;

        /// <summary>
        /// The Entity's Current Health
        /// </summary>
        public int Health
        {
            get
            {
                return currentHealth;
            }
        }

        /// <summary>
        /// The constructor defines the Position and Texture of the Entity
        /// </summary>
        /// <param name="texture">The Entity's Texture</param>
        /// <param name="position">The Top-Left corner of the Entity</param>
        /// <param name="width">How wide you want the Entity</param>
        /// <param name="height">How tall you want the Entity</param>
        public Entity(Texture2D texture, Point position, int width, int height)
        {
            this.position = new Rectangle(position.X, position.Y, width, height);
            this.texture = texture;
        }

        /// <summary>
        /// This draws the entity by passing a SpriteBatch
        /// </summary>
        /// <param name="sb">The SpriteBatch that Draws the Entity</param>
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }

        /// <summary>
        /// This allows the players and enemies to take an amount of damage reduced by their defense from abilities
        /// </summary>
        /// <param name="damage">The damage provided by the ability</param>
        public void TakeDamage(int damage)
        {
            if (damage - def > 0)
            {
                currentHealth -= (damage -= def);
            }
        }
    }
}
