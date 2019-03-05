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
    enum PlayState { PlayerInput, PlayerAttack, EnemyTurn, SafeRoom }

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

        //Fields for player input
        private int selectedAbility;
        private int selectedTarget;
        private MouseState mouseCurrent;
        private MouseState mouseLast;
        private KeyboardState keyboardCurrent;
        private KeyboardState keyboardLast;

        //Buttons for the player UI
        private Button cryButton;
        private Button defendButton;
        private Button[] abilityButton;

        public Play()
        {
            player = new Player(SpritesDirectory.GetSprite("Elion"), new Point((int)(SpritesDirectory.width * .125), (int)(SpritesDirectory.height * .3125)), (int)(SpritesDirectory.width * .125), (int)(SpritesDirectory.height * .4167));
            floor = new Queue<Room>();
            safeRoom = new SafeRoom();
            state = PlayState.PlayerInput;
            floorLevel = 1;
            levelIncrease = 1.125f;
            //-1 for selectedTarget and selectedAbility indicates no selection
            selectedAbility = -1;
            selectedTarget = -1;

            //Buttons
            Rectangle cryRect = new Rectangle((int)(SpritesDirectory.width * .75), (int)(SpritesDirectory.height * .833), (int)(SpritesDirectory.width * .1875), (int)(SpritesDirectory.height * .1042));
            Rectangle defendRect = new Rectangle((int)(SpritesDirectory.width * .75), (int)(SpritesDirectory.height * .6771), (int)(SpritesDirectory.width * .1875), (int)(SpritesDirectory.height * .1042));
            Rectangle ability1Rect = new Rectangle((int)(SpritesDirectory.width * .0625), (int)(SpritesDirectory.height * .6771), (int)(SpritesDirectory.width * .1875), (int)(SpritesDirectory.height * .1042));
            Rectangle ability2Rect = new Rectangle((int)(SpritesDirectory.width * .375), (int)(SpritesDirectory.height * .6771), (int)(SpritesDirectory.width * .1875), (int)(SpritesDirectory.height * .1042));
            Rectangle ability3Rect = new Rectangle((int)(SpritesDirectory.width * .0625), (int)(SpritesDirectory.height * .833), (int)(SpritesDirectory.width * .1875), (int)(SpritesDirectory.height * .1042));
            Rectangle ability4Rect = new Rectangle((int)(SpritesDirectory.width * .375), (int)(SpritesDirectory.height * .833), (int)(SpritesDirectory.width * .1875), (int)(SpritesDirectory.height * .1042));
            cryButton = new Button("Cry", cryRect, SpritesDirectory.GetFont("Arial40"), SpritesDirectory.GetSprite("Button"));
            defendButton = new Button("Defend", defendRect, SpritesDirectory.GetFont("Arial40"), SpritesDirectory.GetSprite("Button"));
            abilityButton = new Button[4];
            abilityButton[0] = new Button(player.Abilities[0].Name, ability1Rect, SpritesDirectory.GetFont("Arial40"), SpritesDirectory.GetSprite("Button"));
            abilityButton[1] = new Button(player.Abilities[1].Name, ability2Rect, SpritesDirectory.GetFont("Arial40"), SpritesDirectory.GetSprite("Button"));
            abilityButton[2] = new Button(player.Abilities[2].Name, ability3Rect, SpritesDirectory.GetFont("Arial40"), SpritesDirectory.GetSprite("Button"));
            abilityButton[3] = new Button(player.Abilities[3].Name, ability4Rect, SpritesDirectory.GetFont("Arial40"), SpritesDirectory.GetSprite("Button"));

            cryButton.IsActive = cryButton.IsVisible = true;
            defendButton.IsActive = defendButton.IsVisible = true;
            abilityButton[0].IsActive = abilityButton[0].IsVisible = true;
            abilityButton[1].IsActive = abilityButton[1].IsVisible = true;
            abilityButton[2].IsActive = abilityButton[2].IsVisible = true;
            abilityButton[3].IsActive = abilityButton[3].IsVisible = true;
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

            cryButton.IsActive = cryButton.IsVisible = true;
            defendButton.IsActive = defendButton.IsVisible = true;
            abilityButton[0].IsActive = abilityButton[0].IsVisible = true;
            abilityButton[1].IsActive = abilityButton[1].IsVisible = true;
            abilityButton[2].IsActive = abilityButton[2].IsVisible = true;
            abilityButton[3].IsActive = abilityButton[3].IsVisible = true;
        }

        public MainState Update(GameTime time)
        {
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
                        cryButton.IsVisible = cryButton.IsActive = false;
                        defendButton.IsVisible = defendButton.IsActive = false;
                        abilityButton[0].IsVisible = abilityButton[0].IsActive = false;
                        abilityButton[1].IsVisible = abilityButton[1].IsActive = false;
                        abilityButton[2].IsVisible = abilityButton[2].IsActive = false;
                        abilityButton[3].IsVisible = abilityButton[3].IsActive = false;
                    }
                    break;
                case PlayState.PlayerAttack:
                    //Placeholder state for player animations
                    state = PlayState.EnemyTurn;
                    break;
                case PlayState.EnemyTurn:
                    state = floor.Peek().TakeEnemyTurn(player);
                    if(state == PlayState.PlayerInput)
                    {
                        //Reveal buttons
                        cryButton.IsVisible = cryButton.IsActive = true;
                        defendButton.IsVisible = defendButton.IsActive = true;
                        abilityButton[0].IsVisible = abilityButton[0].IsActive = true;
                        abilityButton[1].IsVisible = abilityButton[1].IsActive = true;
                        abilityButton[2].IsVisible = abilityButton[2].IsActive = true;
                        abilityButton[3].IsVisible = abilityButton[3].IsActive = true;
                        
                    }
                    else if(state == PlayState.SafeRoom)
                    {
                        //Always call setup before entering the safe room
                        safeRoom.SetUp();

                        //Hide buttons
                        cryButton.IsVisible = cryButton.IsActive = false;
                        defendButton.IsVisible = defendButton.IsActive = false;
                        abilityButton[0].IsVisible = abilityButton[0].IsActive = false;
                        abilityButton[1].IsVisible = abilityButton[1].IsActive = false;
                        abilityButton[2].IsVisible = abilityButton[2].IsActive = false;
                        abilityButton[3].IsVisible = abilityButton[3].IsActive = false;
                    }
                    break;
                case PlayState.SafeRoom:
                    //Automatically returns the player to the menu if they're dead
                    if(!player.IsActive)
                    {
                        return MainState.Menu;
                    }

                    state = safeRoom.Update(time);

                    if(state == PlayState.PlayerInput)
                    {
                        player.CurrentMP = player.MaxMP;
                        player.Health = player.MaxHealth;
                        //Makes evey floor 12.5% harder than the last
                        floorLevel *= levelIncrease;
                        GenerateFloor();

                        //Reveal buttons
                        cryButton.IsVisible = cryButton.IsActive = true;
                        defendButton.IsVisible = defendButton.IsActive = true;
                        abilityButton[0].IsVisible = abilityButton[0].IsActive = true;
                        abilityButton[1].IsVisible = abilityButton[1].IsActive = true;
                        abilityButton[2].IsVisible = abilityButton[2].IsActive = true;
                        abilityButton[3].IsVisible = abilityButton[3].IsActive = true;
                    }
                    break;
                    
            }
            return MainState.Play;
        }

        public void Draw(SpriteBatch batch)
        {
            //Draw background
            batch.Draw(SpritesDirectory.GetSprite("CombatBackground"), new Rectangle(0, 0, (SpritesDirectory.width), (int)(SpritesDirectory.height * 1.25)), Color.White);
            
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

            //Draw player Stats
            batch.Draw(SpritesDirectory.GetSprite("Button"), new Rectangle((int)(SpritesDirectory.width * .0125), (int)(SpritesDirectory.height * .02083), (int)(SpritesDirectory.width * .15), (int)(SpritesDirectory.height * .14583)), Color.White);      //Box
            batch.DrawString(SpritesDirectory.GetFont("Arial"), "Elion", new Vector2((int)(SpritesDirectory.width * .03125), (int)(SpritesDirectory.height * .03125)), Color.White);     //Name
            batch.DrawString(SpritesDirectory.GetFont("Arial"), string.Format("HP: {0}", player.Health), new Vector2((int)(SpritesDirectory.width * .03125), (int)(SpritesDirectory.height * .07292)), Color.White);     //Health
            batch.DrawString(SpritesDirectory.GetFont("Arial"), string.Format("MP: {0}", player.CurrentMP), new Vector2((int)(SpritesDirectory.width * .03125), (int)(SpritesDirectory.height * .114583)), Color.White);        //MP
            //batch.DrawString(SpritesDirectory.GetFont("Arial40"), "Elion", new Vector2(25, 15), Color.White, 0, new Vector2(0, 0), .3f, SpriteEffects.None, 100);     //For scaling font, currently doesn't work

            //Draw stats of enemy being hovered over
            for (int k = 4; k > -1; k--)
            {
                if (floor.Peek()[k] != null && floor.Peek()[k].Selected(mouseCurrent))
                {
                    batch.Draw(SpritesDirectory.GetSprite("Button"), new Rectangle(670, 10, 120, 70), Color.White);     //Box
                    batch.DrawString(SpritesDirectory.GetFont("Arial"), string.Format("{0} {1}", floor.Peek()[k].Name, k + 1), new Vector2(685, 15), Color.White);     //Name
                    batch.DrawString(SpritesDirectory.GetFont("Arial"), string.Format("HP: {0}", floor.Peek()[k].Health), new Vector2(685, 35), Color.White);     //Health
                    break;
                }
            }
            
            //Draw 
            switch (state)
            {
                case PlayState.PlayerInput:
                    //Cry Button
                    cryButton.Draw(batch);
                    //Defend Button
                    defendButton.Draw(batch);
                    //Ability 1 Button
                    abilityButton[0].Draw(batch);
                    //Ability 2 Button
                    abilityButton[1].Draw(batch);
                    //Ability 3 Button
                    abilityButton[2].Draw(batch);
                    //Ability 4 Button
                    abilityButton[3].Draw(batch);
                    break;
                case PlayState.PlayerAttack:
                    //Hiding the buttons should be done in the Update method
                    break;
                case PlayState.EnemyTurn:
                    break;
                case PlayState.SafeRoom:
                    safeRoom.Draw(batch);
                    break;
            }
        }

        /// <summary>
        /// Generates a new set of rooms for the floor
        /// </summary>
        private void GenerateFloor()
        {
            floor.Clear();
            for(int k = 0; k < Math.Round(floorLevel); k++)
            {
                floor.Enqueue(new Room(RoomsDirectory.GetRandomLayout(), floorLevel, player));
            }
        }

        private PlayState GetPlayerInput()
        {
            //Gets player input for their selected ability
            #region ability selection
            if (cryButton.IsPressed(mouseLast, mouseCurrent))
            {
                player.Cry();
                return PlayState.PlayerAttack;
            }
            else if (defendButton.IsPressed(mouseLast, mouseCurrent))
            {
                player.Defend();
                return PlayState.PlayerAttack;
            }
            else if (abilityButton[0].IsPressed(mouseLast, mouseCurrent))
            {
                if (player.Abilities[0] != null && player.CurrentMP >= player.Abilities[0].ManaCost)
                {
                    selectedAbility = 0;
                }
            }
            else if (abilityButton[1].IsPressed(mouseLast, mouseCurrent))
            {
                if (player.Abilities[1] != null && player.CurrentMP >= player.Abilities[1].ManaCost)
                {
                    selectedAbility = 1;
                }
            }
            else if (abilityButton[2].IsPressed(mouseLast, mouseCurrent))
            {
                if (player.Abilities[2] != null && player.CurrentMP >= player.Abilities[2].ManaCost)
                {
                    selectedAbility = 2;
                }
            }
            else if (abilityButton[3].IsPressed(mouseLast, mouseCurrent))
            {
                if (player.Abilities[3] != null && player.CurrentMP >= player.Abilities[3].ManaCost)
                {
                    selectedAbility = 3;
                }
            }
            #endregion

            //Gets the player's target if the ability is targeted and activates the ability
            if(selectedAbility != -1 && player.Abilities[selectedAbility].IsTargeted)
            {
                #region target selection
                if(mouseCurrent.LeftButton == ButtonState.Pressed)
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
                #endregion
            }

            //Attempt to activate the ability
            if(selectedAbility != -1 && player.Abilities[selectedAbility] != null)
            {
                #region ability activation
                //For targeted abilities
                if (player.Abilities[selectedAbility].IsTargeted && selectedTarget != -1)
                {
                    if(floor.Peek()[selectedTarget] != null && floor.Peek()[selectedTarget].IsActive)
                    {
                        if(player.AttackEnemy(floor.Peek()[selectedTarget], player.Abilities[selectedAbility]))
                        {
                            //Reset the selectedAbility and selectedTarget fields after a successful attack
                            selectedAbility = selectedTarget = -1;
                            return PlayState.PlayerAttack;
                        }
                    }
                }
                //For untargeted abilities
                else
                {
                    //There are currently no untargeted abilities and the AttackEnemy method isnt set up to handle them
                }
                #endregion
            }

            //This return will only be reached if an ability could not be activated
            return PlayState.PlayerInput;
        }
    }
}
