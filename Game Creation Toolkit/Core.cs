﻿using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Menus;
using Game_Creation_Toolkit.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game_Creation_Toolkit
{
    public class Core : Game
    {
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        public static ContentManager _content;
        public static float ElapsedGameTime;
        public static Color WindowColor = new Color(49, 49, 49); //creates a variable for the window colour

        public Core()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = 1080; //sets the window height
            _graphics.PreferredBackBufferWidth = 1920; //sets the window width
            _content = Content; //used to make the content variable public so it can be used in other classes
        }

        protected override void Initialize()
        {
            //InitialMenu InitialMenu = new InitialMenu(); //Initialises the Initial Menu class which features the first controls the user will see
            GUIDEMO GUI = new GUIDEMO();
            Window.IsBorderless = true;
            IsFixedTimeStep = true;
            _graphics.SynchronizeWithVerticalRetrace = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            ElapsedGameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            UIHandler.Update(); //Updates the UI elements
            SystemHandlers.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(WindowColor); //sets the window colour

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            UIHandler.Draw(); //draws the currently loaded UI to the screen
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}