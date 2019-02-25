using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/* Iain Davis
 * The Enemy class that each enemy will inherit from
 * THIS IS WHAT YOU ARE FIGHTING
 * There are no known issues */

namespace HatQuest
{
    class Enemy : Entity
    {
        /// <summary>
        /// A reference to the player for attacks
        /// </summary>
        protected Player player;
        private string name;

        //Properties
        public string Name { get { return name; } }


        /// <summary>
        /// A modified ctor from Entity that takes the player in as a parameter
        /// </summary>
        /// <param name="texture">The Enemy's Texture</param>
        /// <param name="position">The top left corner of the enemy's sprite</param>
        /// <param name="width">The width of the enemy</param>
        /// <param name="height">The height of the enemy</param>
        /// <param name="player">The player</param>
        public Enemy(EnemyType type, Point position, int width, int height, Player player) : base(type.Sprite, position, width, height)
        {
            abilities = type.Abilities;
            name = type.Name;
            maxHealth = currentHealth = type.Health;
            atk = type.Attack;
            def = type.Defense;
            this.player = player;
        }

        /// <summary>
        /// This allows the monster to attack the player
        /// </summary>
        /// <param name="ability">The ability to be used to attack the player</param>
        public void AttackPlayer(Ability ability)
        {
            ability.Activate(this, player);
        }
    }
}
