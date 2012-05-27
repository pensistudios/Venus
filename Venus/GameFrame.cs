using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Venus
{

    public class GameFrame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Engine engine;
        Input input;

        int width = 1280;
        int height = 1024;


        public GameFrame()
        {
            graphics = new GraphicsDeviceManager(this);
            input = new Input();
            Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = width;
            this.graphics.PreferredBackBufferHeight = height;
            //this.graphics.SynchronizeWithVerticalRetrace = false;
            this.IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            //this.IsFixedTimeStep = false;
            // TODO: Add your initialization logic here
            

            base.Initialize();

            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Courier New");
            engine = new Engine(this.Content, new Vector2(width, height));
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            UpdateInput();

#if DEBUG  
            /*if(gameTime.IsRunningSlowly)
            {
                System.Console.WriteLine("(Wtf)System Slow: " + gameTime.TotalRealTime.Seconds);
            }*/
#endif
            /*IAsyncResult result = Guide.BeginShowStorageDeviceSelector(PlayerIndex.One,null,null);
            StorageDevice device = Guide.EndShowStorageDeviceSelector(result);
            wtf
            StorageContainer s = device.OpenContainer("Venus");*/            

            engine.Update(gameTime.ElapsedGameTime.Milliseconds,input);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();
            
            engine.Draw(spriteBatch, gameTime.ElapsedGameTime.Milliseconds);

            int fps = 0;
            if (gameTime.ElapsedGameTime.Milliseconds != 0)
            {
                fps = 1000 / gameTime.ElapsedGameTime.Milliseconds;
            }
            spriteBatch.DrawString(font, fps.ToString(), new Vector2(10.0f, 10.0f), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected void UpdateInput()
        {
            //TODO: Input Mapping here prob

            KeyboardState k = Keyboard.GetState();
            input.playerMoveLeft = k.IsKeyDown(Keys.Left);
            input.playerMoveRight = k.IsKeyDown(Keys.Right);
            input.playerJump = k.IsKeyDown(Keys.Space);

        }
    }
}
