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
        private int floorLevel;
        private Hat droppedHat;
        private int temp;

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

            abilityButton = new Button[6];


            int count = 0;
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    abilityButton[count] = new Button(
                        player.Abilities[count].Name,
                        new Rectangle(
                            (int)(SpritesDirectory.width * (.08 + .23 * Math.Pow(x, 1.5))),      //x
                            (int)(SpritesDirectory.height * (.75 + (y * .13))),     //y
                            (int)(SpritesDirectory.width * .1875),                  //width
                            (int)(SpritesDirectory.height * .1042)),                //height
                        SpritesDirectory.GetFont("Arial40"));
                    abilityButton[count].IsActive = abilityButton[count].IsVisible = true;
                    count++;
                }
            }

            Rectangle newAbilityRect = new Rectangle((int)(SpritesDirectory.width * .75),
                                                     (int)(SpritesDirectory.height * .755),
                                                     (int)(SpritesDirectory.width * .1875),
                                                     (int)(SpritesDirectory.height * .1042));


            newAbilityButton = new Button("null", newAbilityRect, SpritesDirectory.GetFont("Arial40"));


            //Textbox
            Rectangle textBox = new Rectangle(200, 10, 400, 100);
            description = new TextBox("null", textBox, SpritesDirectory.GetFont("Arial16"));

            //Animation
            fps = 10.0;
            timePerFrame = 1.0 / fps;

            animation = new Animations(fps, timePerFrame);

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
                        animation.SetSprite(SpritesDirectory.GetSprite("StatusEffect"), 
                                            new Rectangle((int)player.Position.X - 60, 
                                                          (int)player.Position.Y - 80,
                                                          (int)(SpritesDirectory.width * .125),
                                                          (int)(SpritesDirectory.height * .4167)),
                                            10, 
                                            116, 
                                            1523, 
                                            826); //Change player x and y location based on Animation Test need math
                    }
                    break;
                case PlayState.PlayerAttack:
                    //Placeholder state for player animations
                    animation.UpdateAnimation(time);
                    //call the event when the PlayState changes
                    if(animation.IsDone)
                    {
                        animation.ResetAnimation();
                        player.TurnEnd();
                        state = PlayState.EnemyTurn;
                    }
                    break;
                case PlayState.EnemyTurn:
                    state = floor.Peek().TakeEnemyTurn(player);
                    if(state == PlayState.PlayerInput)
                    {
                        player.TurnStart();
                        //Reveal buttons
                        foreach (Button ab in abilityButton)
                        {
                            ab.IsActive = ab.IsVisible = true;
                        }

                    }
                    else if(state == PlayState.CombatEnd)
                    {
                        description.IsVisible = true;
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
                    if(keyboardCurrent.IsKeyDown(Keys.Enter) && keyboardLast.IsKeyUp(Keys.Enter) && ((lastClicked!=null && lastClicked.Clicked) || (player.Loot!=null && !player.Loot.HasAbility)))
                    {
                        if (player.Loot != null)
                        {
                            if (player.Loot.HasAbility && temp !=0)
                            {
                                player.Loot.Equip(player, temp);
                                player.Loot = null;
                                abilityButton[temp] = new Button(player.Abilities[temp].Name,
                                                              abilityButton[temp].Rect,
                                                              SpritesDirectory.GetFont("Arial40"));
                            }
                            else
                            {
                                player.Loot.Equip(player);
                                player.Loot = null;
                            }
                        }

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
                    else if(player.Loot != null)
                    {
                        if (player.Loot.HasAbility)
                        {
                            #region Ability selection
                            //Enable ability buttons
                            for (int k = 0; k < 4; k++)
                            {
                                if (abilityButton[k].IsPressed(mouseLast, mouseCurrent))
                                {
                                    if (lastClicked != null)
                                    {
                                        lastClicked.Clicked = false;
                                    }
                                    lastClicked = abilityButton[k];
                                    abilityButton[k].Clicked = true;
                                    temp = k;
                                }
                            }
                            if (newAbilityButton.IsPressed(mouseLast, mouseCurrent))
                            {
                                if (lastClicked != null)
                                {
                                    lastClicked.Clicked = false;
                                }
                                lastClicked = newAbilityButton;
                                newAbilityButton.Clicked = true;
                            }
                            #endregion
                        }
                    }

                    if (state == PlayState.PlayerInput)
                    {
                        //Reveal buttons
                        foreach(Button ab in abilityButton)
                        {
                            ab.IsActive = ab.IsVisible = true;
                        }
                    }
                    break;
                case PlayState.SafeRoom:
                    description.IsVisible = false;
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
                floor.Peek().Draw(batch);
            }

            //---------Draw player Stats---------
            string[] stats = player.GetStats();
            //Background
            batch.Draw(SpritesDirectory.GetSprite("Button"), 
                       new Rectangle((int)(SpritesDirectory.width * .0125), 
                                     (int)(SpritesDirectory.height * (1/64.0)), 
                                     (int)(SpritesDirectory.width * .15), 
                                     (int)(SpritesDirectory.height * ((4 + (5 * stats.Length)) / 128.0))),//.14583 
                       Color.White);

            for(int k = 0; k < stats.Length; k++)
            {
                batch.DrawString(SpritesDirectory.GetFont("Arial12"),
                            stats[k],
                            new Vector2((int)(SpritesDirectory.width * .03125),
                                        (int)(SpritesDirectory.height * ((4 + (5 * k)) / 128.0) + (3 / 128.0))),//.03125
                            Color.White);
            }
            //Draw based on the PlayState
            switch (state)
            {
                case PlayState.PlayerInput:

                    foreach (Button ab in abilityButton)
                    {
                        ab.Draw(batch);
                    }

                    //Draw stats of enemy being hovered over
                    for (int k = 4; k > -1; k--)
                    {
                        if (floor.Peek()[k] != null && floor.Peek()[k].Selected(mouseCurrent))
                        {
                            stats = floor.Peek()[k].GetStats();
                            batch.Draw(SpritesDirectory.GetSprite("Button"),
                                       new Rectangle((int)(SpritesDirectory.width * (67 / 80.0)),
                                                     (int)(SpritesDirectory.height * (1 / 64.0)),
                                                     (int)(SpritesDirectory.width * .15),
                                                     (int)(SpritesDirectory.height * ((4 + (5 * stats.Length)) / 128.0))),
                                       Color.White);

                            for (int j = 0; j < stats.Length; j++)
                            {
                                batch.DrawString(SpritesDirectory.GetFont("Arial12"),
                                            stats[j],
                                            new Vector2((int)(SpritesDirectory.width * (68 / 80.0)),
                                                        (int)(SpritesDirectory.height * ((4 + (5 * j)) / 128.0) + (3 / 128.0))),
                                            Color.White);
                            }
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
                        droppedHat.Draw(batch, null, 0);
                        description.Text = string.Format("You defeated the enemy and got: {0}!", droppedHat.Name);
                        if(droppedHat.HasAbility)
                        {
                            description.Text = string.Format("Please select an ability to replace.");
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
                        description.Text = string.Format("You were defeated :(");
                    }
                    description.Text = description.Text + "Press ENTER to continue";
                    break;
                case PlayState.SafeRoom:
                    safeRoom.Draw(batch);
                    break;
            }


            //Draw textbox
            if (description.IsVisible)
            {
                description.Draw(batch);
            }
            #endregion
        }

        /// <summary>
        /// Generates a new set of rooms for the floor
        /// </summary>
        private void GenerateFloor()
        {
            floor.Clear();
            if(floorLevel == 10)
            {
                floor.Enqueue(new Room(floorLevel, player, true));
            }
            else if(floorLevel % 3 == 0)
            {
                floor.Enqueue(new Room(floorLevel, player));
            }
            else
            {
                for (int k = 0; k < (floorLevel / 3) + 1; k++)
                {
                    floor.Enqueue(new Room(RoomsDirectory.GetRandomLayout(), floorLevel, player));
                }
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

            bool valid = true;
            valid = false;

            for (int x = 0; x < abilityButton.Length; x++)
            {
                if (abilityButton[x].IsHovered())
                {
                    description.Text = player.Abilities[x].Description;
                    valid = true;
                    if (abilityButton[x].IsPressed(mouseLast, mouseCurrent) && player.Abilities[x] != null && player.CurrentMP >= player.Abilities[x].ManaCost)
                    {
                        selectedAbility = x;
                        lastClicked = currentClicked;
                        currentClicked = abilityButton[selectedAbility];
                        break;
                    }
                }
            }
            description.IsVisible = valid;
            #endregion

            //Gets the player's target if the ability is targeted and activates the ability
            #region target selection
            if (selectedAbility != -1 && player.Abilities[selectedAbility].IsTargeted)
            {
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
            #region ability activation
            if (selectedAbility != -1 && player.Abilities[selectedAbility] != null)
            {
                //For targeted abilities
                if (player.Abilities[selectedAbility].IsTargeted)
                {
                    if(selectedTarget != -1 && floor.Peek()[selectedTarget] != null && floor.Peek()[selectedTarget].IsActive)
                    {
                        player.AttackPre(floor.Peek()[selectedTarget]);
                        if (player.UseAbility(floor.Peek()[selectedTarget], selectedAbility))
                        {
                            player.AttackPost(floor.Peek()[selectedTarget]);
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
                    player.Abilities[selectedAbility].Activate(null);
                    selectedAbility = selectedTarget = -1;
                    //Resets selected button for next round of combat
                    currentClicked.Clicked = false;
                    currentClicked = null;
                    lastClicked = null;
                    return PlayState.PlayerAttack;
                }
            }
            #endregion

            //This return will only be reached if an ability could not be activated
            return PlayState.PlayerInput;
        }
    }
}
