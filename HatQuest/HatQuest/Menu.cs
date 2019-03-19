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
    /// Elijah
    /// </summary>
    class Menu
    {
        private Texture2D image;
        private Rectangle position;

        //Fields for player input
        private MouseState mouseCurrent;
        private MouseState mouseLast;

        //Buttons
        private Button playButton;

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
            
            position = new Rectangle(SpritesDirectory.width / 2 - 75, 400, 150, 50);
            playButton = new Button("Play", position, SpritesDirectory.GetFont("Arial40"));

            playButton.IsActive = playButton.IsVisible = true;
            
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
                SpritesDirectory.GetFont("Arial40"), 
                "Hat Quest", 
                new Vector2(SpritesDirectory.width / 2 - SpritesDirectory.GetFont("Arial40").MeasureString("Hat Quest").X / 2, 100), 
                Color.White);

            //Draw Button
            playButton.Draw(batch);
        }
    }
}
