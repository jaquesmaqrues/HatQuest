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
        private string description;

        //Events
        public delegate void RoomDelegate();
        public event RoomDelegate RoomCleared;
        private RoomDelegate roomHandler;

        //Properties
        public Enemy this[int i]
        {
            get { return enemies[i]; }
        }
        public string Description
        {
            get { return description; }
        }
        public bool IsVisible { get; set; }

        /// <summary>
        /// Basic room constructor
        /// </summary>
        public Room(RoomLayout layout, double level, Player player)
        {
            //Fill the room with enemies
            enemies = new Enemy[5];
            for(int k = 0; k < 5; k++)
            {
                if(layout[k] != null)
                {
                    if (k % 2 == 0)
                    {
                        enemies[k] = new Enemy(layout[k], level, new Point(650, (k * 50) + 25), 75, 150, player, layout[k].HatPosition);
                        enemies[k].Name += (" " + (k + 1));
                    }
                    else
                    {
                        enemies[k] = new Enemy(layout[k], level, new Point(550, (k * 60) + 60), 75, 150, player, layout[k].HatPosition);
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
            IsVisible = false;
            currentAttacker = 0;
            description = "";
        }

        /// <summary>
        /// Boss room constructor
        /// </summary>
        /// <param name="finalBoss">Pass in true only if this is the final boss room</param>
        public Room(double level, Player player, bool finalBoss = false)
        {
            //Put the boss in the room
            //  indicies 1-4 should be empty
            enemies = new Enemy[5];
            if(finalBoss == false)
            {
                EnemyType temp = EnemiesDirectory.RANDOM();
                enemies[0] = new Enemy(temp, level * 3, new Point(650, 100), 150, 380, player, temp.HatPosition);
            }
            else
            {
                
                enemies[0] = new Enemy(EnemiesDirectory.BOSS, level * 3, new Point(650, 100), 150, 380, player, EnemiesDirectory.BOSS.HatPosition);
            }

            enemies[0].Name += " Boss";
            enemies[1] = null;
            enemies[2] = null;
            enemies[3] = null;
            enemies[4] = null;

            //Give the boss hats
            for (int k = 0; k < level; k++)
            {
                HatsDirectory.GetRandomHat(level * 2).Equip(enemies[0]);
            }

            //A currentAttacker of 5 indicates that all enemies have attacked
            IsVisible = false;
            currentAttacker = 0;
            description = "";
        }

        /// <summary>
        /// Used as an Update method for the Room class while the Play class is in the EnemyTurn state
        /// </summary>
        /// <param name="player">The current player for the enemies to target</param>
        public PlayState TakeEnemyTurn(Player player, GameTime time)
        {
            //Update the current anuimation
            if (currentAttacker < 5 && enemies[currentAttacker] != null && !enemies[currentAttacker].Animation.IsDone)
            {
                enemies[currentAttacker].Animation.UpdateAnimation(time);
                //If the animation just finished increase the currentAttacker
                if(enemies[currentAttacker].Animation.IsDone)
                {
                    currentAttacker++;
                }
                return PlayState.EnemyTurn;
            }
            else
            {
                //Let the current attacker take their turn
                //If enemies[currentAttacker] is non-null and alive then let that enemy take it's turn
                //Otherwise only increase currentAttacker
                Ability usedAbility;
                switch (currentAttacker)
                {
                    #region Enemy turn
                    case 0:
                        if (enemies[0] != null && enemies[0].IsActive)
                        {
                            enemies[0].TurnStart();
                            //End the current enemy's turn if they just died (like to poison)
                            if (!enemies[0].IsActive) { currentAttacker++; break; }
                            enemies[0].AttackPre(player);
                            usedAbility = enemies[0].AttackPlayer();
                            enemies[0].AttackPost(player);
                            enemies[0].TurnEnd();
                            //Update the animation and description
                            description = String.Format("{0} used {1}", enemies[0].Name, usedAbility.Name);
                            enemies[0].Animation.ResetAnimation(player.Position, usedAbility.Color);
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
                            if (!enemies[1].IsActive) { currentAttacker++; break; }
                            enemies[1].AttackPre(player);
                            usedAbility = enemies[1].AttackPlayer();
                            enemies[1].TurnEnd();
                            description = String.Format("{0} used {1}", enemies[0].Name, usedAbility.Name);
                            enemies[1].Animation.ResetAnimation(player.Position, usedAbility.Color);
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
                            if (!enemies[2].IsActive) { currentAttacker++; break; }
                            enemies[2].AttackPre(player);
                            usedAbility = enemies[2].AttackPlayer();
                            enemies[2].AttackPost(player);
                            enemies[2].TurnEnd();
                            description = String.Format("{0} used {1}", enemies[0].Name, usedAbility.Name);
                            enemies[2].Animation.ResetAnimation(player.Position, usedAbility.Color);
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
                            if (!enemies[3].IsActive) { currentAttacker++; break; }
                            enemies[3].AttackPre(player);
                            usedAbility = enemies[3].AttackPlayer();
                            enemies[3].AttackPost(player);
                            enemies[3].TurnEnd();
                            description = String.Format("{0} used {1}", enemies[0].Name, usedAbility.Name);
                            enemies[3].Animation.ResetAnimation(player.Position, usedAbility.Color);
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
                            if (!enemies[4].IsActive) { currentAttacker++; break; }
                            enemies[4].AttackPre(player);
                            usedAbility = enemies[4].AttackPlayer();
                            enemies[4].AttackPost(player);
                            enemies[4].TurnEnd();
                            description = String.Format("{0} used {1}", enemies[0].Name, usedAbility.Name);
                            enemies[4].Animation.ResetAnimation(player.Position, usedAbility.Color);
                        }
                        else
                        {
                            currentAttacker++;
                        }
                        break;
                    //Once all enemies havce taken their turn end the enemy turn
                    default:
                        //Returns the player to their turn if there are still live enemies
                        foreach (Enemy enemy in enemies)
                        {
                            if (enemy != null && enemy.IsActive)
                            {
                                currentAttacker = 0;
                                return PlayState.PlayerInput;
                            }
                        }
                        //Sends the player to the safe room if all enemies are dead
                        currentAttacker = 0;
                        roomHandler = RoomCleared;
                        if (roomHandler != null)
                        {
                            RoomCleared();
                        }
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
        }

        public void Draw(SpriteBatch batch)
        {
            if(IsVisible)
            {
                foreach (Enemy en in enemies)
                {
                    //The if statement may be redundant
                    //IDK if the foreach loop will iterate through null values
                    if (en != null)
                        en.Draw(batch);
                }
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
