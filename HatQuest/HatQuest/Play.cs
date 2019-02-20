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
        private Button abilityButton1;
        private Button abilityButton2;
        private Button abilityButton3;
        private Button abilityButton4;

        public Play()
        {
            player = new Player(SpritesDirectory.GetSprite("Elion"), new Point(100, 100), 50, 50);
            floor = new Queue<Room>();
            GenerateFloor();
            state = PlayState.PlayerInput;
            floorLevel = 1;

            //Buttons
            Rectangle buttRect = new Rectangle(10, 10, 50, 20);
            cryButton = new Button("Cry", buttRect, SpritesDirectory.GetFont("Arial"), SpritesDirectory.GetSprite("Lucario"));
            defendButton = new Button("Cry", buttRect, SpritesDirectory.GetFont("Arial"), SpritesDirectory.GetSprite("Lucario"));
            abilityButton1 = new Button("Cry", buttRect, SpritesDirectory.GetFont("Arial"), SpritesDirectory.GetSprite("Lucario"));
            abilityButton2 = new Button("Cry", buttRect, SpritesDirectory.GetFont("Arial"), SpritesDirectory.GetSprite("Lucario"));
            abilityButton3 = new Button("Cry", buttRect, SpritesDirectory.GetFont("Arial"), SpritesDirectory.GetSprite("Lucario"));
            abilityButton4 = new Button("Cry", buttRect, SpritesDirectory.GetFont("Arial"), SpritesDirectory.GetSprite("Lucario"));

            cryButton.IsActive = cryButton.IsVisible = false;
            defendButton.IsActive = defendButton.IsVisible = false;
            abilityButton1.IsActive = abilityButton1.IsVisible = false;
            abilityButton2.IsActive = abilityButton2.IsVisible = false;
            abilityButton3.IsActive = abilityButton3.IsVisible = false;
            abilityButton4.IsActive = abilityButton4.IsVisible = false;
        }   

        public MainState Update()
        {
            //Update the current keyboard and mouse state
            mouseLast = mouseCurrent;
            mouseCurrent = Mouse.GetState();
            keyboardLast = keyboardCurrent;
            keyboardCurrent = Keyboard.GetState();

            //Update the gameplay based on the currebnt state and inputs
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
            //Draw the player and enemies
            player.Draw(batch);
            for(int k = 0; k < 5; k++)
            {
                if(floor.Peek()[k] != null)
                    floor.Peek()[k].Draw(batch);
            }

            //Draw 
            switch (state)
            {
                case PlayState.PlayerInput:
                    break;
                case PlayState.PlayerAttack:
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
            floor.Enqueue(new Room(new Enemy(SpritesDirectory.GetSprite("Lucario"), new Point(150), 50, 50, player)));
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
            else if (abilityButton1.IsPressed())
            {
                return PlayState.PlayerAttack;
            }
            else if (abilityButton2.IsPressed())
            {
                return PlayState.PlayerAttack;
            }
            else if (abilityButton3.IsPressed())
            {
                return PlayState.PlayerAttack;
            }
            else if (abilityButton4.IsPressed())
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
