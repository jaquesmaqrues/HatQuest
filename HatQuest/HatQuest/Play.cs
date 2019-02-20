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
        private void GenerateRoom()
        {

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
