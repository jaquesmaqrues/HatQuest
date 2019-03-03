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
    class Menu
    {
        private Texture2D image;
        private Rectangle position;

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
            Rectangle playRect = new Rectangle(600, 400, 150, 50);
            playButton = new Button("Play", playRect, SpritesDirectory.GetFont("Arial"), SpritesDirectory.GetSprite("Button"));

            playButton.IsActive = playButton.IsVisible = true;
        }

        //---------METHODS---------
        public MainState Update()
        {
            return MainState.Play;
        }

        public void Draw(SpriteBatch batch)
        {
            //Draw background
            batch.Draw(SpritesDirectory.GetSprite("CombatBackground"), new Rectangle(0, 0, 800, 600), Color.White);

            //Draw Name
            batch.DrawString(SpritesDirectory.GetFont("Arial"), string.Format("Hi"), new Vector2(685, 15), Color.White);

            //Draw Button
            playButton.Draw(batch);
        }
    }
}
