using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HatQuest
{
    /// <summary>
    /// Elijah, Iain
    /// </summary>
    abstract class Ability
    {
        protected int manaCost;
        protected bool isTargeted;
        protected string name;
        protected string description;

        /// <summary>
        /// The MP cost of the ability
        /// </summary>
        public int ManaCost
        {
            get
            {
                return manaCost;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
        }

        /// <summary>
        /// Whether the ability is targeted or not
        /// </summary>
        public bool IsTargeted
        {
            get { return isTargeted; }
        }

        public Ability(int manaCost, bool isTargeted, string name, string description)
        {
            this.manaCost = manaCost;
            this.isTargeted = isTargeted;
            this.name = name;
            this.description = description;
        }

        /// <summary>
        /// The logic behind activating an ability
        /// </summary>
        /// <param name="attacker">The attacking Entity</param>
        /// <param name="defender">The defending Entity</param>
        public abstract void Activate(Entity attacker, Entity defender);
    }
}
