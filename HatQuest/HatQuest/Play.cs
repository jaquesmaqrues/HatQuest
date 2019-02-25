using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HatQuest.Init;

namespace HatQuest
{
    enum PlayState { PlayerInput, PlayerAttack, EnemyTurn, SafeRoom }

    class Play
    {
        //Fields
        private Player player;
        private Queue<Room> floor;
        private PlayState state;
        private int floorLevel;
        private float timer;
        private MouseState mouseCurrent;
        private MouseState mouseLast;
        private KeyboardState keyboardCurrent;
        private KeyboardState keyboardLast;

        //Buttons for the player UI
        private Button cryButton;
        private Button defendButton;
        private Button[] abilityButton;

        //Hats
        private Hats.Hat hat;

        public Play()
        {
            player = new Player(SpritesDirectory.GetSprite("Elion"), new Point(100, 150), 100, 200);
            floor = new Queue<Room>();
            GenerateFloor();
            state = PlayState.PlayerInput;
            floorLevel = 1;

            //Hats
            hat = new Hats.Hat(SpritesDirectory.GetSprite("Hat"), 0, 0, 0, 5);
            hat.Equip(player);  //Error: Maxhealth in Entity is never set(?)  System.StackOverflowException from Public int MaxHealth get{}

            //Buttons
            Rectangle cryRect = new Rectangle(600, 400, 150, 50);
            Rectangle defendRect = new Rectangle(600, 325, 150, 50);
            Rectangle ability1Rect = new Rectangle(50, 325, 150, 50);
            Rectangle ability2Rect = new Rectangle(300, 325, 150, 50);
            Rectangle ability3Rect = new Rectangle(50, 400, 150, 50);
            Rectangle ability4Rect = new Rectangle(300, 400, 150, 50);
            cryButton = new Button("Cry", cryRect, SpritesDirectory.GetFont("Arial"), SpritesDirectory.GetSprite("Button"));
            defendButton = new Button("Defend", defendRect, SpritesDirectory.GetFont("Arial"), SpritesDirectory.GetSprite("Button"));
            abilityButton = new Button[4];
            abilityButton[0] = new Button("Ability 1", ability1Rect, SpritesDirectory.GetFont("Arial"), SpritesDirectory.GetSprite("Button"));
            abilityButton[1] = new Button("Ability 2", ability2Rect, SpritesDirectory.GetFont("Arial"), SpritesDirectory.GetSprite("Button"));
            abilityButton[2] = new Button("Ability 3", ability3Rect, SpritesDirectory.GetFont("Arial"), SpritesDirectory.GetSprite("Button"));
            abilityButton[3] = new Button("Ability 4", ability4Rect, SpritesDirectory.GetFont("Arial"), SpritesDirectory.GetSprite("Button"));

            cryButton.IsActive = cryButton.IsVisible = false;
            defendButton.IsActive = defendButton.IsVisible = false;
            abilityButton[0].IsActive = abilityButton[0].IsVisible = false;
            abilityButton[1].IsActive = abilityButton[1].IsVisible = false;
            abilityButton[2].IsActive = abilityButton[2].IsVisible = false;
            abilityButton[3].IsActive = abilityButton[3].IsVisible = false;
        }   

        public MainState Update()
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
                    break;
                case PlayState.PlayerAttack:
                    break;
                case PlayState.EnemyTurn:
                    state = floor.Peek().TakeEnemyTurn(player);
                    break;
                case PlayState.SafeRoom:
                    break;
            }
            return MainState.Play;
        }

        public void Draw(SpriteBatch batch)
        {
            //Draw background
            batch.Draw(SpritesDirectory.GetSprite("CombatBackground"), new Rectangle(0, 0, 800, 600), Color.White);
            
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

            //batch.Draw(SpritesDirectory.GetSprite("Hat"), new Rectangle(100, 100, 100, 100), Color.White);

            //Draw player Stats
            batch.Draw(SpritesDirectory.GetSprite("Button"), new Rectangle(10, 10, 120, 70), Color.White);      //Box
            batch.DrawString(SpritesDirectory.GetFont("Arial"), "Elion", new Vector2(25, 15), Color.White);     //Name
            batch.DrawString(SpritesDirectory.GetFont("Arial"), string.Format("HP: {0}", player.Health), new Vector2(25, 35), Color.White);     //Health
            batch.DrawString(SpritesDirectory.GetFont("Arial"), string.Format("MP: (0)", player.CurrentMP), new Vector2(25, 55), Color.White);        //MP

            //Draw enemy stats (Hardcoded for now)
            batch.Draw(SpritesDirectory.GetSprite("Button"), new Rectangle(670, 10, 120, 70), Color.White);     //Box
            batch.DrawString(SpritesDirectory.GetFont("Arial"), "Goblin", new Vector2(685, 15), Color.White);     //Name
            batch.DrawString(SpritesDirectory.GetFont("Arial"), string.Format("HP: {0}", floor.Peek()[0].Health), new Vector2(685, 35), Color.White);     //Health

            //Draw 
            switch (state)
            {
                case PlayState.PlayerInput:
                    //Cry Button
                    cryButton.IsVisible = true;
                    cryButton.Draw(batch);
                    //Defend Button
                    defendButton.IsVisible = true;
                    defendButton.Draw(batch);
                    //Ability 1 Button
                    abilityButton[0].IsVisible = true;
                    abilityButton[0].Draw(batch);
                    //Ability 2 Button
                    abilityButton[1].IsVisible = true;
                    abilityButton[1].Draw(batch);
                    //Ability 3 Button
                    abilityButton[2].IsVisible = true;
                    abilityButton[2].Draw(batch);
                    //Ability 4 Button
                    abilityButton[3].IsVisible = true;
                    abilityButton[3].Draw(batch);
                    break;
                case PlayState.PlayerAttack:
                    //Hide buttons
                    cryButton.IsVisible = false;
                    defendButton.IsVisible = false;
                    abilityButton[0].IsVisible = false;
                    abilityButton[1].IsVisible = false;
                    abilityButton[2].IsVisible = false;
                    abilityButton[3].IsVisible = false;
                    break;
                case PlayState.EnemyTurn:
                    break;
                case PlayState.SafeRoom:
                    break;
            }
        }

        /// <summary>
        /// Generates a new set of rooms for the floor
        /// </summary>
        private void GenerateFloor()
        {
            floor.Enqueue(new Room(new Enemy(EnemiesDirectory.GOBLIN, 1, new Point(600,175), 97, 150, player)));
        }

        private PlayState GetPlayerInput()
        {
            if(cryButton.IsPressed())
            {
                return PlayState.PlayerAttack;
            }
            else if (defendButton.IsPressed())
            {
                return PlayState.PlayerAttack;
            }
            else if (abilityButton[0].IsPressed())
            {
                if (player.Abilities[0] != null && floor.Peek()[0] != null)
                {
                    player.AttackEnemy(floor.Peek()[0], player.Abilities[0]);
                    return PlayState.EnemyTurn;
                }
                else
                {
                    return PlayState.PlayerAttack;
                }     
            }
            else if (abilityButton[1].IsPressed())
            {
                return PlayState.PlayerAttack;
            }
            else if (abilityButton[2].IsPressed())
            {
                return PlayState.PlayerAttack;
            }
            else if (abilityButton[3].IsPressed())
            {
                return PlayState.PlayerAttack;
            }
            else
            {
                return PlayState.PlayerInput;
            } 
        }
    }
}
