using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HatQuest
{
    /// <summary>
    /// Kat
    /// </summary>
    class Animations
    {
        private Texture2D spriteSheet;
        private Rectangle spriteLocation;

        //Animation
        private int frame;              //Current animation frame
        private double timeCounter;     //The amount of time that has passed
        private double fps;             //The speed of the animation
        private double timePerFrame;    //The amount of time (in fractional seconds) per frame

        //Fields for "source" rectangle (inside the image)
        private int attackFrameCount = 10;      //Number of frames in the animation
        private int spriteRectOffsetY = 1408;  //How far down in the image are the frames
        private int spriteRectHeight = 1406;   //The height of a single frame
        private int spriteRectWidth = 703;     //The width of a single frame

        private bool isDone;
        private Color color;
        private bool isVisible;

        public bool IsDone
        {
            get
            {
                return isDone;
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        public bool IsVisible
        {
            get
            {
                return isVisible;
            }
            set
            {
                isVisible = value;
            }
        }

        //---------CONSTRUCTORS---------

        public Animations(double fps, double timePerFrame, Texture2D texture, int attackFrameCount, int spriteRectOffsetY, int spriteRectHeight, int spriteRectWidth)
        {
            frame = 0;
            timeCounter = 0;
            this.fps = fps;
            this.timePerFrame = timePerFrame;
            isVisible = false;
            isDone = true;
            color = Color.Red;
            spriteSheet = texture;
            this.attackFrameCount = attackFrameCount;
            this.spriteRectOffsetY = spriteRectOffsetY;     //For testing purposes, may not need
            this.spriteRectHeight = spriteRectHeight;       //May be hardcoded
            this.spriteRectWidth = spriteRectWidth;
        }

        //---------METHODS---------

        /// <summary>
        /// Sets fields to correct sprite
        /// </summary>
        /// <param name="texture">Sprite sheet</param>
        /// <param name="spriteLocation">Where to draw sprite</param>
        /// <param name="attackFrameCount">Number of frames in animation</param>
        /// <param name="spriteRectOffsetY">How far down on the image the frame is</param>
        /// <param name="spriteRectHeight">Height of the sprite</param>
        /// <param name="spriteRectWidth">Width of the sprite</param>
        public void SetSprite(Texture2D texture, int attackFrameCount, int spriteRectOffsetY, int spriteRectHeight, int spriteRectWidth)
        {
            spriteSheet = texture;
            //this.spriteLocation = spriteLocation;
            this.attackFrameCount = attackFrameCount;
            this.spriteRectOffsetY = spriteRectOffsetY;     //For testing purposes, may not need
            this.spriteRectHeight = spriteRectHeight;       //May be hardcoded
            this.spriteRectWidth = spriteRectWidth;         //May be hardcoded
            this.color = Color.Red;
        }

        /// <summary>
        /// Upadates the animation based on time elapsed
        /// </summary>
        /// <param name="gameTime">GameTime object</param>
        public void UpdateAnimation(GameTime gameTime)
        {
            timeCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeCounter >= timePerFrame)
            {
                frame += 1;

                if (frame > attackFrameCount)
                {
                    isDone = true;
                    isVisible = false;
                }

                timeCounter -= timePerFrame;
            }
        }

        /// <summary>
        /// Resets the animation
        /// </summary>
        public void ResetAnimation(Rectangle spriteLocation, Color color)
        {
            isVisible = true;
            isDone = false;
            frame = 1;
            timeCounter = 0;
            this.spriteLocation = spriteLocation;
            this.color = color;
        }

        /// <summary>
        /// Draws the sprite attacking
        /// </summary>
        /// <param name="batch">Spritebatch</param>
        public void DrawAttack(SpriteBatch batch)
        {
            if(isVisible)
            {
                batch.Draw(spriteSheet,
                       new Vector2(spriteLocation.X, spriteLocation.Y),
                       new Rectangle(frame * spriteRectWidth,
                                     spriteRectOffsetY,
                                     spriteRectWidth,
                                     spriteRectHeight),
                       color,
                       0,
                       Vector2.Zero,
                       .3f,
                       SpriteEffects.None,
                       0);
            }
        }
    }
}
