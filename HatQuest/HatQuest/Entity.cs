using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HatQuest.Hats;
using HatQuest.Effects;
using HatQuest.Init;

namespace HatQuest
{
    /// <summary>
    /// Elijah, Iain, Jack
    /// </summary>
    class Entity
    {
        //Fields
        protected Texture2D texture;
        protected Rectangle position;
        protected bool isVisible;
        protected bool isActive;
        protected List<Ability> abilities;
        protected int currentHealth;
        protected int maxHealth;
        protected int def;
        protected int atk;
        protected List<Hat> hats;
        protected List<StatusEffect> effects;
        protected List<string> stats;
        protected Animations animation;
        protected int hatPosition;

        //Events
        //DamageEvents should return any changes to the damage
        public delegate int EntityDamageDelegate(Entity defender, int damage);
        public event EntityDamageDelegate DamageEvent;
        public EntityDamageDelegate damageHandler;

        public delegate void CombatDelegate(Entity attacker, Entity defender);
        public event CombatDelegate TurnStartEvent;
        public event CombatDelegate AttackPreEvent;
        public event CombatDelegate AttackPostEvent;
        public event CombatDelegate TurnEndEvent;
        public CombatDelegate combatHandler;


        //Properties
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        public Animations Animation
        {
            get { return animation; }
            set { animation = value; }
        }

        /// <summary>
        /// The attack modifier for the entity
        /// </summary>
        public int Atk
        {
            get
            {
                return atk;
            }
            set
            {
                atk = value;
            }
        }

        /// <summary>
        /// The Entity's Current Health
        /// </summary>
        public int Health
        {
            get
            {
                return currentHealth;
            }
            set
            {
                currentHealth = value;
            }
        }

        /// <summary>
        /// The Entity's defense
        /// </summary>
        public int Def
        {
            get
            {
                return def;
            }
            set
            {
                def = value;
            }
        }

        /// <summary>
        /// The Entity's Maximum health
        /// </summary>
        public int MaxHealth
        {
            get
            {
                return maxHealth;
            }
            set
            {
                maxHealth = value;
            }
        }

        /// <summary>
        /// The entities known abilities
        /// </summary>
        public List<Ability> Abilities
        {
            get { return abilities; }
        }

        /// <summary>
        /// The entity's current status effects
        /// </summary>
        public List<StatusEffect> Effects
        {
            get { return effects; }
            set { effects = value; }
        }

        /// <summary>
        /// The Entity's current location
        /// </summary>
        public Rectangle Position
        {
            get
            {
                return position;
            }
        }

        /// <summary>
        /// An indexer for the hats list
        /// </summary>
        /// <param name="i">Whichever index you're trying to look at</param>
        /// <returns>A hat that's equipped to the Entity</returns>
        public Hat this[int i] => hats[i];

        /// <summary>
        /// The list of hats equipped by the Entity
        /// </summary>
        public List<Hat> Hats
        {
            get
            {
                return hats;
            }
            set
            {
                hats = value;
            }
        }
        /// <summary>
        /// Offsets the hat by a set amount
        /// </summary>
        public int HatPosition
        {
            get { return hatPosition; }
            set { hatPosition = value; }
        }

        /// <summary>
        /// The constructor defines the Position and Texture of the Entity
        /// </summary>
        /// <param name="texture">The Entity's Texture</param>
        /// <param name="position">The Top-Left corner of the Entity</param>
        /// <param name="width">How wide you want the Entity</param>
        /// <param name="height">How tall you want the Entity</param>
        public Entity(Texture2D texture, Point position, int width, int height, int hatPosition)
        {
            this.position = new Rectangle(position.X, position.Y, width, height);
            this.texture = texture;
            abilities = new List<Ability>();
            isVisible = isActive = true;
            hats = new List<Hat>();
            effects = new List<StatusEffect>();
            stats = new List<string>();
            this.hatPosition = hatPosition;
            animation = new Animations(10.0, 
                                       1.0 / 10.0, 
                                       SpritesDirectory.GetSprite("StatusEffect"),
                                       10,
                                       116,
                                       1523,
                                       826); 
        }

        /// <summary>
        /// This draws the entity by passing a SpriteBatch
        /// </summary>
        /// <param name="sb">The SpriteBatch that Draws the Entity</param>
        public void Draw(SpriteBatch sb)
        {
            if (isVisible)
            {
                sb.Draw(texture, position, Color.White);
                animation.DrawAttack(sb);
                for(int k = 0; k < hats.Count; k++)
                {
                    hats[k].Draw(sb, this, k);     
                }
            }
        }

        /// <summary>
        /// Returns an array of strings that corespond to the entity's stats
        /// </summary>
        /// <returns></returns>
        public virtual string[] GetStats()
        {
            return stats.ToArray();
        }

        /// <summary>
        /// This allows the players and enemies to take an amount of damage reduced by their defense from abilities
        /// </summary>
        /// <param name="damage">The damage provided by the ability</param>
        public void TakeDamage(int damage)
        {
            //Modify the damage through events
            damageHandler = DamageEvent;
            if (damageHandler != null)
            {
                damage += DamageEvent(this, damage);
            }         

            if (damage - def > 0)
            {
                currentHealth -= (damage - def);
            }

            //Check if the enemy should die
            if (currentHealth < 1)
            {
                isActive = false;
                isVisible = false;
            }
        }

        /// <summary>
        /// Triggers the TurnStartEvent
        /// </summary>
        public void TurnStart()
        {
            combatHandler = TurnStartEvent;
            if(combatHandler != null)
            {
                TurnStartEvent(this, null);
            }
        }

        /// <summary>
        /// Triggers the AttackPreEvent
        /// </summary>
        /// <param name="target">Target of this entity's attack</param>
        public void AttackPre(Entity target)
        {
            combatHandler = AttackPreEvent;
            if (combatHandler != null)
            {
                AttackPreEvent(this, target);
            }
        }

        /// <summary>
        /// Triggers the AttackPostEvent
        /// </summary>
        /// <param name="target">Target of this entity's attack</param>
        public void AttackPost(Entity target)
        {
            combatHandler = AttackPostEvent;
            if (combatHandler != null)
            {
                AttackPostEvent(this, target);
            }
        }

        /// <summary>
        /// Triggers the TurnEndEvent
        /// </summary>
        public void TurnEnd()
        {
            combatHandler = TurnEndEvent;
            if (combatHandler != null)
            {
                TurnEndEvent(this, null);
            }
        }

        public bool Selected(MouseState ms)
        {
            if (position.Contains(ms.Position) && isActive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
