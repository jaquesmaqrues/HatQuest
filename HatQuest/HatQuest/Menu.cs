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
    /// <summary>
    /// Elijah, Kat
    /// </summary>
    class Menu
    {
        private Texture2D image;
        private Rectangle playPosition;
        private Rectangle instructionsPosition;

        //Fields for player input
        private MouseState mouseCurrent;
        private MouseState mouseLast;

        //Buttons
        private Button playButton;
        private Button instructionsButton;

        //---------PROPERTIES---------

        public Texture2D Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
            }
        }

        //---------CONSTRUCTORS---------

        public Menu()
        {
            //Buttons
            
            playPosition = new Rectangle(SpritesDirectory.width / 2 - (int)((SpritesDirectory.width * .1875) / 2), 
                                         (int)(SpritesDirectory.height * .625), //300
                                         (int)(SpritesDirectory.width * .1875), //150 
                                         (int)(SpritesDirectory.height * .0833)); //40
            playButton = new Button("Play", playPosition, SpritesDirectory.GetFont("Arial40"));

            instructionsPosition = new Rectangle(SpritesDirectory.width / 2 - (int)((SpritesDirectory.width * .1875) / 2),
                                                 (int)(SpritesDirectory.height * .79167), //380
                                                 (int)(SpritesDirectory.width * .1875), //150
                                                 (int)(SpritesDirectory.height * .0833)); //40
            instructionsButton = new Button("Instructions", instructionsPosition, SpritesDirectory.GetFont("Arial40"));

            playButton.IsActive = playButton.IsVisible = true;
            instructionsButton.IsActive = instructionsButton.IsVisible = true;
            
        }

        //---------METHODS---------
        public MainState Update()
        {
            //Update the current keyboard and mouse state
            mouseLast = mouseCurrent;
            mouseCurrent = Mouse.GetState();

            if (playButton.IsPressed(mouseLast, mouseCurrent))
            {
                return MainState.Play;
            }
            else if (instructionsButton.IsPressed(mouseLast, mouseCurrent))
            {
                return MainState.Instructions;
            }
            else
            {
                return MainState.Menu;
            }
        }

        public void Draw(SpriteBatch batch)
        {
            //Draw background
            batch.Draw(SpritesDirectory.GetSprite("CombatBackground"), new Rectangle(0, 0, SpritesDirectory.width, SpritesDirectory.height), Color.White);

            
            //Draw Name
            batch.DrawString(
                SpritesDirectory.GetFont("Arial60"), 
                "Hat Quest", 
                new Vector2((SpritesDirectory.width / 2) - (SpritesDirectory.GetFont("Arial60").MeasureString("Hat Quest").X / 2), 100), 
                Color.White);

            //Draw Button
            playButton.Draw(batch);
            instructionsButton.Draw(batch);
        }
    }
}
