using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HatQuest.Init;
using HatQuest.Effects;

/* Iain Davis
 * The Enemy class that each enemy will inherit from
 * There are no known issues */

namespace HatQuest
{
    /// <summary>
    /// Elijah
    /// </summary>
    class Enemy : Entity
    {
        /// <summary>
        /// A reference to the player for attacks
        /// </summary>
        private Player player;
        private string name;
        Random random;

        //Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }



        /// <summary>
        /// A modified ctor from Entity that takes the player in as a parameter
        /// </summary>
        /// <param name="texture">The Enemy's Texture</param>
        /// <param name="position">The top left corner of the enemy's sprite</param>
        /// <param name="width">The width of the enemy</param>
        /// <param name="height">The height of the enemy</param>
        /// <param name="player">The player</param>
        public Enemy(EnemyType enemyType, double level, Point position, int width, int height, Player player) : base(enemyType.Sprite, position, width, height)
        {
            level = Math.Pow(1.125, level-1);
            name = enemyType.Name;
            this.player = player;
            atk = (int)(enemyType.Attack * level);
            def = (int)(enemyType.Defense * level);
            maxHealth = currentHealth = (int)(enemyType.Health * level);
            foreach(Ability a in enemyType.Abilities)
            {
                abilities.Add(a.Clone(this));
            }
            random = EnemiesDirectory.random;
        }

        /// <summary>
        /// This allows the monster to attack the player
        /// </summary>
        /// <param name="ability">The ability to be used to attack the player</param>
        /// <returns>The ability used</returns>
        public Ability AttackPlayer()
        {
            int abilityIndex = random.Next(abilities.Count);
            abilities[abilityIndex].Activate(player);
            animation.ResetAnimation(player.Position, abilities[abilityIndex].Color);
            return abilities[abilityIndex];
        }

        public override string[] GetStats()
        {
            stats.Clear();
            stats.Add(name);
            stats.Add(string.Format("HP: {0}/{1}", Health, MaxHealth));
            foreach (StatusEffect e in effects)
            {
                stats.Add(e.ToString());
            }
            return stats.ToArray();
        }
    }
}
