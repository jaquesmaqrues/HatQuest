using HatQuest.Directories;
using HatQuest.Hats;
using HatQuest.Init;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace HatQuest
{
    enum PlayState { PlayerInput, PlayerAttack, EnemyTurn, SafeRoom, CombatEnd }

    /// <summary>
    /// Elijah, Kat
    /// </summary>
    class Play
    {
        //Fields
        private Player player;
        private Queue<Room> floor;
        private SafeRoom safeRoom;
        private PlayState state;
        private float floorLevel;
        private float levelIncrease;
        private float timer;
        private Hat droppedHat;

        //Fields for player input
        private int selectedAbility;
        private int selectedTarget;
        private MouseState mouseCurrent;
        private MouseState mouseLast;
        private KeyboardState keyboardCurrent;
        private KeyboardState keyboardLast;

        //Buttons and textboxes for the player UI
        private Button[] abilityButton;
        private Button newAbilityButton;
        private Button lastClicked;
        private Button currentClicked;
        private TextBox description;

        //Events
        public delegate void CombatEvent(Entity attacker, Entity defender);
        public event CombatEvent PlayerTurnStart;
        public event CombatEvent PlayerAttackPre;
        public event CombatEvent PlayerAttackPost;
        public event CombatEvent PlayerTurnEnd;

        //Animation
        private double fps;
        private double timePerFrame;
        private Animations animation;

        public Play()
        {
            player = new Player(SpritesDirectory.GetSprite("Elion"), 
                                new Point((int)(SpritesDirectory.width * .125), 
                                          (int)(SpritesDirectory.height * .3125)), 
                                          (int)(SpritesDirectory.width * .125), 
                                          (int)(SpritesDirectory.height * .4167));

            floor = new Queue<Room>();
            safeRoom = new SafeRoom();
            state = PlayState.PlayerInput;
            floorLevel = 1;
            //-1 for selectedTarget and selectedAbility indicates no selection
            selectedAbility = -1;
            selectedTarget = -1;


            //Buttons
            Rectangle cryRect = new Rectangle((int)(SpritesDirectory.width * .75), 
                                              (int)(SpritesDirectory.height * .833), 
                                              (int)(SpritesDirectory.width * .1875), 
                                              (int)(SpritesDirectory.height * .1042));

            Rectangle defendRect = new Rectangle((int)(SpritesDirectory.width * .75), 
                                                 (int)(SpritesDirectory.height * .6771), 
                                                 (int)(SpritesDirectory.width * .1875), 
                                                 (int)(SpritesDirectory.height * .1042));

            Rectangle ability1Rect = new Rectangle((int)(SpritesDirectory.width * .0625), 
                                                   (int)(SpritesDirectory.height * .6771), 
                                                   (int)(SpritesDirectory.width * .1875), 
                                                   (int)(SpritesDirectory.height * .1042));

            Rectangle ability2Rect = new Rectangle((int)(SpritesDirectory.width * .375), 
                                                   (int)(SpritesDirectory.height * .6771), 
                                                   (int)(SpritesDirectory.width * .1875), 
                                                   (int)(SpritesDirectory.height * .1042));

            Rectangle ability3Rect = new Rectangle((int)(SpritesDirectory.width * .0625), 
                                                   (int)(SpritesDirectory.height * .833), 
                                                   (int)(SpritesDirectory.width * .1875), 
                                                   (int)(SpritesDirectory.height * .1042));

            Rectangle ability4Rect = new Rectangle((int)(SpritesDirectory.width * .375), 
                                                   (int)(SpritesDirectory.height * .833), 
                                                   (int)(SpritesDirectory.width * .1875), 
                                                   (int)(SpritesDirectory.height * .1042));


            abilityButton = new Button[6];
            Rectangle newAbilityRect = new Rectangle((int)(SpritesDirectory.width * .75),
                                                     (int)(SpritesDirectory.height * .755),
                                                     (int)(SpritesDirectory.width * .1875),
                                                     (int)(SpritesDirectory.height * .1042));


            abilityButton = new Button[6];
            abilityButton[0] = new Button(player.Abilities[0].Name, ability1Rect, SpritesDirectory.GetFont("Arial40"));
            abilityButton[1] = new Button(player.Abilities[1].Name, ability2Rect, SpritesDirectory.GetFont("Arial40"));
            abilityButton[2] = new Button(player.Abilities[2].Name, ability3Rect, SpritesDirectory.GetFont("Arial40"));
            abilityButton[3] = new Button(player.Abilities[3].Name, ability4Rect, SpritesDirectory.GetFont("Arial40"));
            abilityButton[4] = new Button(player.Abilities[4].Name, defendRect, SpritesDirectory.GetFont("Arial40"));
            abilityButton[5] = new Button(player.Abilities[5].Name, cryRect, SpritesDirectory.GetFont("Arial40"));
            newAbilityButton = new Button("null", newAbilityRect, SpritesDirectory.GetFont("Arial40"));

            foreach (Button ab in abilityButton)
            {
                ab.IsActive = ab.IsVisible = true;
            }

            //Textbox
            Rectangle textBox = new Rectangle(200, 10, 600, 70);
            description = new TextBox(null, textBox, SpritesDirectory.GetFont("Arial40"));

            //Animation
            fps = 10.0;
            timePerFrame = 1.0 / fps;

            animation = new Animations(fps, timePerFrame);
        }   

        private void Play_PlayerAttackPre(Player player, Entity target)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets up the play class for a new game
        /// </summary>
        public void SetUp()
        {
            player.Reset();
            GenerateFloor();
            floorLevel = 1;
            state = PlayState.PlayerInput;

            foreach (Button ab in abilityButton)
            {
                ab.IsActive = ab.IsVisible = true;
            }
        }

        public MainState Update(GameTime time)
        {
            #region Update
            //Update the current keyboard and mouse state
            mouseLast = mouseCurrent;
            mouseCurrent = Mouse.GetState();
            keyboardLast = keyboardCurrent;
            keyboardCurrent = Keyboard.GetState();

            //Checking if button or hat is hovered over
            for (int i = 0; i < 7; i++)
            {
                if (abilityButton[i].IsHovered())
                {
                    //description.Text = player.Abilities[i].Description;
                    description.IsVisible = true;
                }
                else
                {
                    description.IsVisible = false;
                }
            }

            

            //Update the gameplay based on the current state and inputs
            switch(state)
            {
                case PlayState.PlayerInput:
                    state = GetPlayerInput();
                    if(state == PlayState.PlayerAttack)
                    {
                        //Hide buttons
                        foreach (Button ab in abilityButton)
                        {
                            ab.IsActive = ab.IsVisible = false;
                        }

                        animation.SetSprite(AnimationsDirectory.getAnimation("Mario"), player.Position, 3, 116, 72, 44);
                    }
                    break;
                case PlayState.PlayerAttack:
                    //Placeholder state for player animations
                    animation.UpdateAnimation(time);
                    //PlayerTurnEnd(player, null);
                    state = PlayState.EnemyTurn;
                    break;
                case PlayState.EnemyTurn:
                    state = floor.Peek().TakeEnemyTurn(player);
                    if(state == PlayState.PlayerInput)
                    {
                        //PlayerTurnStart(player, null);

                        //Reveal buttons
                        foreach (Button ab in abilityButton)
                        {
                            ab.IsActive = ab.IsVisible = true;
                        }

                    }
                    else if(state == PlayState.CombatEnd)
                    {
                        //Get the dropped hat and remove the room
                        droppedHat = HatsDirectory.GetRandomHat(floorLevel, floor.Dequeue().GetDroppedHats());
                        if (player.IsActive)
                        {
                            player.Loot = droppedHat;
                        }

                        //Hide buttons
                        abilityButton[4].IsVisible = abilityButton[4].IsActive = false;
                        abilityButton[5].IsVisible = abilityButton[5].IsActive = false;

                        if (droppedHat.HasAbility)
                        {
                            //Make and enable the ability to select the new ability
                            Rectangle newAbilityRect = new Rectangle((int)(SpritesDirectory.width * .75),
                                                                     (int)(SpritesDirectory.height * .755),
                                                                     (int)(SpritesDirectory.width * .1875),
                                                                     (int)(SpritesDirectory.height * .1042));

                            newAbilityButton = new Button(droppedHat.Ability.Name, newAbilityRect, SpritesDirectory.GetFont("Arial40"));
                            newAbilityButton.IsVisible = newAbilityButton.IsActive = true;
                            abilityButton[0].IsVisible = abilityButton[0].IsActive = true;
                            abilityButton[1].IsVisible = abilityButton[1].IsActive = true;
                            abilityButton[2].IsVisible = abilityButton[2].IsActive = true;
                            abilityButton[3].IsVisible = abilityButton[3].IsActive = true;
                        }
                        //Disable all the ability buttons if there is no new ability to select
                        else
                        {
                            abilityButton[0].IsVisible = abilityButton[0].IsActive = false;
                            abilityButton[1].IsVisible = abilityButton[1].IsActive = false;
                            abilityButton[2].IsVisible = abilityButton[2].IsActive = false;
                            abilityButton[3].IsVisible = abilityButton[3].IsActive = false;
                        }
                    }
                    break;
                case PlayState.CombatEnd:
                    //Make sure the player can equip the hat
                    if (player.Loot != null)
                    {
                        if (player.Loot.HasAbility)
                        {
                            #region Ability selection
                            //Enable ability buttons
                            for (int k = 0; k < 4; k++)
                            {
                                if (abilityButton[k].IsPressed(mouseLast, mouseCurrent))
                                {
                                    player.Loot.Equip(player, k);
                                    player.Loot = null;
                                    abilityButton[k] = new Button(player.Abilities[k].Name,
                                                                  abilityButton[k].Rect,
                                                                  SpritesDirectory.GetFont("Arial40"));
                                    break;
                                }
                            }
                            if (newAbilityButton.IsPressed(mouseLast, mouseCurrent))
                            {
                                player.Loot.Equip(player);
                                player.Loot = null;
                                break;
                            }

                            //When the player has selected an ability to discard
                            if(player.Loot == null)
                            {
                                //Disable ability buttons
                                newAbilityButton.IsVisible = newAbilityButton.IsActive = false;
                                abilityButton[0].IsVisible = abilityButton[0].IsActive = false;
                                abilityButton[1].IsVisible = abilityButton[1].IsActive = false;
                                abilityButton[2].IsVisible = abilityButton[2].IsActive = false;
                                abilityButton[3].IsVisible = abilityButton[3].IsActive = false;
                            }
                            #endregion
                        }
                        else
                        {
                            player.Loot.Equip(player);
                            player.Loot = null;
                        }
                    }
                    else if (keyboardCurrent.IsKeyDown(Keys.Enter) && keyboardLast.IsKeyUp(Keys.Enter))
                    {
                        //Automatically returns the player to the menu if they're dead
                        if (!player.IsActive)
                        {
                            return MainState.Menu;
                        }

                        
                        //The player has already recieved their loot
                        else
                        {

                            //Move to the next combat if the floor still has rooms left
                            if (floor.Count > 0)
                            {
                                state = PlayState.PlayerInput;
                            }
                            //Move to the safe room if the current floor has been completed
                            else
                            {
                                state = PlayState.SafeRoom;
                                safeRoom.SetUp();
                            }
                        }
                    }

                    if(state == PlayState.PlayerInput)
                    {
                        //Reveal buttons
                        foreach(Button ab in abilityButton)
                        {
                            ab.IsActive = ab.IsVisible = true;
                        }
                    }
                    break;
                case PlayState.SafeRoom:
                    state = safeRoom.Update(time);

                    if(state == PlayState.PlayerInput)
                    {
                        player.CurrentMP = player.MaxMP;
                        player.Health = player.MaxHealth;
                        floorLevel++;
                        GenerateFloor();

                        //Reveal buttons
                        foreach (Button ab in abilityButton)
                        {
                            ab.IsActive = ab.IsVisible = true;
                        }
                    }
                    break;
                    
            }
            return MainState.Play;
            #endregion
        }

        public void Draw(SpriteBatch batch)
        {
            #region Draw
            //Draw background
            batch.Draw(SpritesDirectory.GetSprite("CombatBackground"), 
                       new Rectangle(0, 0, (SpritesDirectory.width), (int)(SpritesDirectory.height * 1.25)), 
                       Color.White);
            
            //Draw the player and enemies
            player.Draw(batch);
            if (floor != null && floor.Count > 0)
            {
                for (int k = 0; k < 5; k++)
                {
                    if (floor.Peek()[k] != null)
                        floor.Peek()[k].Draw(batch);
                }
            }

            //---------Draw player Stats---------
            //Background
            batch.Draw(SpritesDirectory.GetSprite("Button"), 
                       new Rectangle((int)(SpritesDirectory.width * .0125), 
                                     (int)(SpritesDirectory.height * .02083), 
                                     (int)(SpritesDirectory.width * .15), 
                                     (int)(SpritesDirectory.height * 0.26041)),//.14583 
                       Color.White);
            //Name
            batch.DrawString(SpritesDirectory.GetFont("Arial"), 
                            "Elion", 
                            new Vector2((int)(SpritesDirectory.width * .03125), 
                                        (int)(SpritesDirectory.height * 2/64)),//.03125
                            Color.White);
            //HP
            batch.DrawString(SpritesDirectory.GetFont("Arial"), 
                            string.Format("HP: {0} / {1}", player.Health, player.MaxHealth), 
                            new Vector2((int)(SpritesDirectory.width * .03125), 
                                        (int)(SpritesDirectory.height * 5/64)), //.07292
                            Color.White);
            //MP
            batch.DrawString(SpritesDirectory.GetFont("Arial"), 
                            string.Format("MP: {0} / {1}", player.CurrentMP, player.MaxMP), 
                            new Vector2((int)(SpritesDirectory.width * .03125), 
                                        (int)(SpritesDirectory.height * 8/64)), //.114583
                            Color.White);
            //Defense
            batch.DrawString(SpritesDirectory.GetFont("Arial"),
                            string.Format("DF: {0}", player.Def),
                            new Vector2((int)(SpritesDirectory.width * .03125),
                                        (int)(SpritesDirectory.height * 11 / 64)),//0.15626
                            Color.White);
            //Attack
            batch.DrawString(SpritesDirectory.GetFont("Arial"),
                            string.Format("AK: {0}", player.Atk),
                            new Vector2((int)(SpritesDirectory.width * .03125),
                                        (int)(SpritesDirectory.height * 14 / 64)),//0.19793
                            Color.White);

            foreach (Button ab in abilityButton)
            {
                ab.Draw(batch);
            }

            //Draw textbox
            


            //Draw based on the PlayState
            switch (state)
            {
                case PlayState.PlayerInput:
                    //Draw stats of enemy being hovered over
                    for (int k = 4; k > -1; k--)
                    {
                        if (floor.Peek()[k] != null && floor.Peek()[k].Selected(mouseCurrent))
                        {
                            //Background
                            batch.Draw(SpritesDirectory.GetSprite("Button"),
                                       new Rectangle(670, 10, 120, 70),
                                       Color.White);
                            //Name
                            batch.DrawString(SpritesDirectory.GetFont("Arial"),
                                             string.Format("{0} {1}", floor.Peek()[k].Name, k + 1),
                                             new Vector2(685, 15),
                                             new Color(1f, 1 / floorLevel, 1 / floorLevel));
                            //Name
                            batch.DrawString(SpritesDirectory.GetFont("Arial"),
                                             string.Format("HP: {0}", floor.Peek()[k].Health),
                                             new Vector2(685, 35), Color.White);
                            break;
                        }
                    }
                    break;
                case PlayState.PlayerAttack:
                    animation.DrawAttack(batch);
                    break;
                case PlayState.EnemyTurn:
                    break;
                case PlayState.CombatEnd:
                    //If the player won the combat
                    if(player.IsActive)
                    {
                        batch.DrawString(SpritesDirectory.GetFont("Arial"), string.Format("You defeated the enemy and got a {0}!", droppedHat.Name), new Vector2(150), Color.Black);
                        if(droppedHat.HasAbility)
                        {
                            batch.DrawString(SpritesDirectory.GetFont("Arial"), string.Format("Please select an ability to replace."), new Vector2(175), Color.Black);
                            //Ability 1 Button
                            abilityButton[0].Draw(batch);
                            //Ability 2 Button
                            abilityButton[1].Draw(batch);
                            //Ability 3 Button
                            abilityButton[2].Draw(batch);
                            //Ability 4 Button
                            abilityButton[3].Draw(batch);
                            //New Ability Button
                            newAbilityButton.Draw(batch);
                        }
                    }
                    //If the player lost the combat
                    else
                    {
                        batch.DrawString(SpritesDirectory.GetFont("Arial"), string.Format("You were defeated :("), new Vector2(150), Color.Black);
                    }
                    batch.DrawString(SpritesDirectory.GetFont("Arial"), string.Format("Press \'ENTER\' to continue", droppedHat.Name), new Vector2(150, 200), Color.Black);
                    break;
                case PlayState.SafeRoom:
                    safeRoom.Draw(batch);
                    break;
            }
            #endregion
        }

        /// <summary>
        /// Generates a new set of rooms for the floor
        /// </summary>
        private void GenerateFloor()
        {
            floor.Clear();
            for(int k = 0; k < (floorLevel/3)+1; k++)
            {
                floor.Enqueue(new Room(RoomsDirectory.GetRandomLayout(), floorLevel, player));
            }
        }

        private PlayState GetPlayerInput()
        {
            //Gets player input for their selected ability
            #region ability selection
            //Changes button background if button was clicked
            if (lastClicked != null)
            {
                lastClicked.Clicked = false;
            }
            if (currentClicked != null)
            {
                currentClicked.Clicked = true;
            }

            for (int x = 0; x < abilityButton.Length; x++)
            {
                if (abilityButton[x].IsPressed(mouseLast, mouseCurrent))
                {
                    if (player.Abilities[x] != null && player.CurrentMP >= player.Abilities[x].ManaCost)
                    {
                        selectedAbility = x;
                        lastClicked = currentClicked;
                        currentClicked = abilityButton[selectedAbility];
                        break;
                    }
                }
            }
            #endregion


            //Gets the player's target if the ability is targeted and activates the ability
            if (selectedAbility != -1 && player.Abilities[selectedAbility].IsTargeted)
            {
                #region target selection
                if (mouseCurrent.LeftButton == ButtonState.Pressed)
                {
                    //Checks for enemies in reverse order since the later enemies are drawn on top of the previous ones
                    for (int k = 4; k > -1; k--)
                    {
                        if (floor.Peek()[k] != null && floor.Peek()[k].Selected(mouseCurrent))
                        {
                            selectedTarget = k;
                            break;
                        }
                    }
                }
            }
            #endregion

            //Attempt to activate the ability
            if (selectedAbility != -1 && player.Abilities[selectedAbility] != null)
            {
                #region ability activation
                //For targeted abilities
                if (player.Abilities[selectedAbility].IsTargeted)
                {
                    if(selectedTarget != -1 && floor.Peek()[selectedTarget] != null && floor.Peek()[selectedTarget].IsActive)
                    {
                        //PlayerAttackPre(player, floor.Peek()[selectedTarget]);
                        if (player.AttackEnemy(floor.Peek()[selectedTarget], player.Abilities[selectedAbility]))
                        {
                            //PlayerAttackPost(player, floor.Peek()[selectedTarget]);
                            //Reset the selectedAbility and selectedTarget fields after a successful attack
                            selectedAbility = selectedTarget = -1;
                            //Resets selected button for next round of combat
                            currentClicked.Clicked = false;
                            currentClicked = null;
                            lastClicked = null;
                            return PlayState.PlayerAttack;
                        }
                    }
                }
                //For untargeted abilities
                else
                {
                    //There are currently no untargeted abilities and the AttackEnemy method isnt set up to handle them
                    player.Abilities[selectedAbility].Activate(player, null);
                    selectedAbility = selectedTarget = -1;
                    //Resets selected button for next round of combat
                    currentClicked.Clicked = false;
                    currentClicked = null;
                    lastClicked = null;
                    return PlayState.PlayerAttack;
                }
                #endregion
            }

            //This return will only be reached if an ability could not be activated
            return PlayState.PlayerInput;
        }
    }
}
