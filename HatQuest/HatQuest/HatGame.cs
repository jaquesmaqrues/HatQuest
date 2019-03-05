using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HatQuest.Init;

namespace HatQuest
{
    enum MainState { Menu, Play, HatSelect, MenuOptions, PlayOptions}

    /// <summary>
    /// Jack, Iain, Kat, Elijah 
    /// </summary>
    public class HatGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //FSM Objets
        Menu menu;
        Play play;

        //HatGame fields
        private MainState state;

        public HatGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //In case we want broken full screen mode
            //graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            state = MainState.Menu;

            //Makes mouse visible 
            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            SpritesDirectory.Init(this);
            PostInitialize();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Initializes content that requires textures or fonts
        /// </summary>
        private void PostInitialize()
        {
            RoomsDirectory.ReadRooms("temp");
            HatsDirectory.SetUp();
            menu = new Menu();
            play = new Play();
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Main FSM
            switch(state)
            {
                case MainState.Menu:
                    state = menu.Update();
                    if(state == MainState.Play)
                    {
                        play.SetUp();
                    }
                    break;
                case MainState.HatSelect:
                    //Currently unused
                    //May be cut in the future
                    break;
                case MainState.Play:
                    state = play.Update(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();

            //Main FSM
            switch (state)
            {
                case MainState.Menu:
                    menu.Draw(spriteBatch);
                    break;
                case MainState.HatSelect:
                    break;
                case MainState.Play:
                    play.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
