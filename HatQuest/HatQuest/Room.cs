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

namespace HatQuest
{
    /// <summary>
    /// Elijah, Kat
    /// </summary>
    class Room
    {
        //Fields
        private Enemy[] enemies;
        private int currentAttacker;
        private float timer;

        //Properties
        public Enemy this[int i]
        {
            get { return enemies[i]; }
        }

        public Room(EnemyType[] enemyTypes, double level, Player player)
        {
            //Fill the room with enemies
            enemies = new Enemy[5];
            for(int k = 0; k < 5; k++)
            {
                if(enemyTypes[k] != null)
                {
                    if (k % 2 == 0)
                    {
                        enemies[k] = new Enemy(enemyTypes[k], level, new Point(650, (k * 50) + 25), 75, 150, player);
                        enemies[k].Name += (" " + (k + 1));
                    }
                    else
                    {
                        enemies[k] = new Enemy(enemyTypes[k], level, new Point(550, (k * 50) + 25), 75, 150, player);
                        enemies[k].Name += (" " + (k + 1));
                    }
                }
                else
                {
                    enemies[k] = null;
                }
            }

            //Give the enemies hats based on the current level
            int hats = (int)level;
            for(int k = 0; k < level; k++)
            {
                //get the index of a random non-null enemy
                int randomEnemy = EnemiesDirectory.random.Next(5);
                while(enemies[randomEnemy] == null)
                {
                    randomEnemy = EnemiesDirectory.random.Next(5);
                }
                //Give a random hat to the random enemy
                HatsDirectory.GetRandomHat(level).Equip(enemies[randomEnemy]);
            }

            //A currentAttacker of 5 indicates that all enemies have attacked
            currentAttacker = 0;
            timer = 0;
        }

        /// <summary>
        /// Used as an Update method for the Room class while the Play class is in the EnemyTurn state
        /// </summary>
        /// <param name="player">The current player for the enemies to target</param>
        public PlayState TakeEnemyTurn(Player player)
        {
            //Let the current attacker take their turn
            switch(currentAttacker)
            {
                #region Enemy turn
                case 0:
                    if (enemies[0] != null && enemies[0].IsActive)
                    {
                        enemies[0].TurnStart();
                        enemies[0].AttackPre(player);
                        enemies[0].AttackPlayer();
                        enemies[0].AttackPost(player);
                        enemies[0].TurnEnd();
                        currentAttacker++;
                    }
                    else
                    {
                        currentAttacker++;
                    }
                    break;
                case 1:
                    if (enemies[1] != null && enemies[1].IsActive)
                    {
                        enemies[1].TurnStart();
                        enemies[1].AttackPre(player);
                        enemies[1].AttackPlayer();
                        enemies[1].AttackPost(player);
                        enemies[1].TurnEnd();
                        currentAttacker++;
                    }
                    else
                    {
                        currentAttacker++;
                    }
                    break;
                case 2:
                    if (enemies[2] != null && enemies[2].IsActive)
                    {
                        enemies[2].TurnStart();
                        enemies[2].AttackPre(player);
                        enemies[2].AttackPlayer();
                        enemies[2].AttackPost(player);
                        enemies[2].TurnEnd();
                        currentAttacker++;
                    }
                    else
                    {
                        currentAttacker++;
                    }
                    break;
                case 3:
                    if (enemies[3] != null && enemies[3].IsActive)
                    {
                        enemies[3].TurnStart();
                        enemies[3].AttackPre(player);
                        enemies[3].AttackPlayer();
                        enemies[3].AttackPost(player);
                        enemies[3].TurnEnd();
                        currentAttacker++;
                    }
                    else
                    {
                        currentAttacker++;
                    }
                    break;
                case 4:
                    if (enemies[4] != null && enemies[4].IsActive)
                    {
                        enemies[4].TurnStart();
                        enemies[4].AttackPre(player);
                        enemies[4].AttackPlayer();
                        enemies[4].AttackPost(player);
                        enemies[4].TurnEnd();
                        currentAttacker++;
                    }
                    else
                    {
                        currentAttacker++;
                    }
                    break;
                //Ends the enemy turn 
                default:
                    //Returns the player to their turn if there are still live e
                    foreach (Enemy enemy in enemies)
                    {
                        if(enemy != null && enemy.IsActive)
                        {
                            currentAttacker = 0;
                            return PlayState.PlayerInput;
                        }
                    }
                    //Sends the player to the safe room if all enemies are dead
                    currentAttacker = 0;
                    return PlayState.CombatEnd;
                    #endregion
                //Checks if all enemies have been defeated after they have all had a chance to take their turn
            }

            //Continues the enemies' turn if they haven't all finished and the player is still alive
            if (player.IsActive)
            {
                return PlayState.EnemyTurn;
            }
            //Ends combat if the player has died
            else
            {
                return PlayState.CombatEnd;
            }
            
        }

        public void Draw(SpriteBatch batch)
        {
            foreach(Enemy en in enemies)
            {
                //The if statement may be redundant
                //IDK if the foreach loop will iterate through null values
                if(en != null)
                    en.Draw(batch);
            }
        }

        /// <summary>
        /// Returns the enemy currently being hovered over
        /// </summary>
        /// <param name="ms">Current mouse state</param>
        /// <returns></returns>
        public Enemy SelectedEnemy(MouseState ms)
        {
            foreach(Enemy e in enemies)
            {
                if(e.Selected(ms) != true)
                {
                    return e;
                }
            }
            return null;
        }

        /// <summary>
        /// Get all of the hats worn by all enemies
        /// </summary>
        /// <returns>An array of all enemy hats</returns>
        public Hat[] GetDroppedHats()
        {
            List<Hat> droppedHats = new List<Hat>();
            foreach(Enemy e in enemies)
            {
                if(e != null)
                {
                    droppedHats.AddRange(e.Hats);
                }
            }
            return droppedHats.ToArray();
        }
    }
}
