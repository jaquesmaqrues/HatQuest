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
using HatQuest.Abilities;
using HatQuest.Effects;

namespace HatQuest
{
    /// <summary>
    /// Elijah, Iain Davis
    /// </summary>
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
        public Player(Texture2D texture, Point position, int width, int height, int hatPosition) : base(texture, position, width, height, hatPosition)
        {
            maxHealth = currentHealth = 10;
            maxMP = currentMP = 10;
            atk = 10;
            def = 1;
            abilities.Add(new Attack(this));
            abilities.Add(new QuickAttack(this));
            abilities.Add(new LifeSiphon(this));
            //abilities.Add(new Berserk(this));
            abilities.Add(new VenomBite(this));
            abilities.Add(new Defend(this));
            abilities.Add(new Cry(this));
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
        /// <param name="ability">The index of the ability to use</param>
        public bool UseAbility(Entity enemy, int abilityIndex)
        {
            if(currentMP - abilities[abilityIndex].ManaCost >= 0)
            {
                currentMP -= abilities[abilityIndex].ManaCost;
                abilities[abilityIndex].Activate(enemy);
                return true;
            }
            else
            {
                return false;
            }
        }

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
            effects.Clear();
            abilities.Clear();
            abilities.Add(new Attack(this));
            abilities.Add(new QuickAttack(this));
            abilities.Add(new LifeSiphon(this));
            //abilities.Add(new Berserk(this));
            abilities.Add(new VenomBite(this));
            abilities.Add(new Defend(this));
            abilities.Add(new Cry(this));
            hats.Clear();
            HatsDirectory.ATKHAT.Equip(this);
            HatsDirectory.BUCKETHAT.Equip(this);
            HatsDirectory.DEFHAT.Equip(this);
            HatsDirectory.HPHAT.Equip(this);
            HatsDirectory.MANAHAT.Equip(this);
        }

        public override string[] GetStats()
        {
            stats.Clear();
            stats.Add("Elion");
            stats.Add(string.Format("HP: {0}/{1}", Health, MaxHealth));
            stats.Add(string.Format("MP: {0}/{1}", CurrentMP, MaxMP));
            stats.Add(string.Format("DF: {0}", Def));
            stats.Add(string.Format("AK: {0}", Atk));
            foreach(StatusEffect e in effects)
            {
                stats.Add(e.ToString());
            }
            return stats.ToArray();
        }
    }
}
