using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace EventTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        delegate void Updates();
        Updates updateHandler;
        event Updates UpdateEvent;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Things> thingList = new List<Things>();
        public static Texture2D tex;
        int i = 0;
        Song canon;
        Texture2D background;

        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.IsFullScreen = true;

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
            tex = Content.Load<Texture2D>("Flakes1");
            canon = Content.Load<Song>("Canon");
            MediaPlayer.Play(canon);
            MediaPlayer.IsRepeating = true;
            background = Content.Load<Texture2D>("Winter");
            // TODO: use this.Content to load your game content here
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
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if(updateHandler != null)
            updateHandler.Invoke();

           
            // TODO: Add your update logic here
            for (int i = 0; i < thingList.Count; i++)
            {
                if(thingList[i].position.Y >graphics.PreferredBackBufferHeight)
                {
                    updateHandler -= thingList[i].Update;
                    thingList.RemoveAt(i);
                    i--;
                }

            }
            CreateSnow();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.Additive);
            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.White);
            foreach (var item in thingList)
            {
                item.Draw(spriteBatch);
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        void CreateTexture()
        {
            Color[] data = new Color[100];

            for (int i = 0; i < 100; i++)
            {
                data[i] = Color.White;
            }
            tex = new Texture2D(GraphicsDevice, 10, 10);

            tex.SetData(data);
        }

        void CreateSnow()
        {
            i++;
            if (i % 1 == 0)
            {
                Things t = new Things();
                thingList.Add(t);
                updateHandler += t.Update;
            }
        }


    }
}
