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
        protected Entity user;

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

        public Ability(int manaCost, bool isTargeted, string name, string description, Entity user)
        {
            this.manaCost = manaCost;
            this.isTargeted = isTargeted;
            this.name = name;
            this.description = description;
            this.user = user;
        }

        /// <summary>
        /// Return a copy of the ability tied to a new user
        /// </summary>
        /// <param name="user">User of the new ability</param>
        /// <returns>Clone of this ability</returns>
        public Ability Clone(Entity user)
        {
            return (Ability)System.Activator.CreateInstance(GetType(), user);
        }

        /// <summary>
        /// The logic behind activating an ability
        /// </summary>
        /// <param name="defender">The target of the ability</param>
        public abstract void Activate(Entity target);
    }
}
