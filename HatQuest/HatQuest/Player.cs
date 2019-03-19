using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HatQuest.Init;
using HatQuest.Hats;

/* Iain Davis, Elijah
 * The Player Class
 * There are no known issues */

namespace HatQuest
{
    class Player : Entity
    {
        private int currentMP;
        private int maxMP;
        private Hat loot;

        /// <summary>
        /// The player's current mana
        /// </summary>
        public int CurrentMP
        {
            get
            {
                return currentMP;
            }
            set
            {
                currentMP = value;
            }
        }

        /// <summary>
        /// The player's maximum mana
        /// </summary>
        public int MaxMP
        {
            get
            {
                return maxMP;
            }
            set
            {
                maxMP = value;
            }
        }

        public Hat Loot
        {
            get
            {
                return loot;
            }
            set
            {
                loot = value;
            }
        }

        /// <summary>
        /// The Constructor for the Player Class
        /// </summary>
        /// <param name="texture">The Player's Texture</param>
        /// <param name="position">The top left corner of the player's sprite</param>
        /// <param name="width">How wide the player is</param>
        /// <param name="height">How tall the player is</param>
        public Player(Texture2D texture, Point position, int width, int height) : base(texture, position, width, height)
        {
            maxHealth = currentHealth = 10;
            maxMP = currentMP = 10;
            atk = 10;
            def = 1;
            abilities.Add(AbilitiesDirectory.ATTACK);
            abilities.Add(AbilitiesDirectory.QUICKATTACK);
            abilities.Add(AbilitiesDirectory.LIFESIPHON);
            abilities.Add(AbilitiesDirectory.BERSERK);
            abilities.Add(AbilitiesDirectory.DEFEND);
            abilities.Add(AbilitiesDirectory.CRY);
            HatsDirectory.ATKHAT.Equip(this);
            HatsDirectory.BUCKETHAT.Equip(this);
            HatsDirectory.DEFHAT.Equip(this);
            HatsDirectory.HPHAT.Equip(this);
            HatsDirectory.MANAHAT.Equip(this);
        }

        /// <summary>
        /// The logic behind attacking an Enemy
        /// </summary>
        /// <param name="enemy">The Enemy to be attacked</param>
        /// <param name="ability">The ability being used</param>
        public bool AttackEnemy(Entity enemy, Ability ability)
        {
            if(currentMP - ability.ManaCost >= 0)
            {
                currentMP -= ability.ManaCost;
                ability.Activate(this, enemy);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Defending adds a certain amount of MP back to your pool
        /// </summary>


        /// <summary>
        /// Does nothing unless you have the Bucket Hat equipped
        /// </summary>


        /// <summary>
        /// Reset all the player data
        /// </summary>
        public void Reset()
        {
            maxHealth = currentHealth = 10;
            maxMP = currentMP = 10;
            atk = 10;
            def = 1;
            isActive = isVisible = true;
            abilities.Clear();
            abilities.Add(AbilitiesDirectory.ATTACK);
            abilities.Add(AbilitiesDirectory.QUICKATTACK);
            abilities.Add(AbilitiesDirectory.LIFESIPHON);
            abilities.Add(AbilitiesDirectory.BERSERK);
            abilities.Add(AbilitiesDirectory.DEFEND);
            abilities.Add(AbilitiesDirectory.CRY);
            hats.Clear();
            HatsDirectory.ATKHAT.Equip(this);
            HatsDirectory.BUCKETHAT.Equip(this);
            HatsDirectory.DEFHAT.Equip(this);
            HatsDirectory.HPHAT.Equip(this);
            HatsDirectory.MANAHAT.Equip(this);
        }
    }
}
