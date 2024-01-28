using Game_Creation_Toolkit.Game_Engine.Handlers;
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
        public static Color NavColour = new Color(192,192,192);
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        public static ContentManager _content;
        public static float ElapsedGameTime;
        public static Color WindowColor = new Color(192, 192, 192); //creates a variable for the window colour
        public static Vector2 WindowSize = new Vector2(1920, 1080);
        public static GameWindow _window;
        static Texture2D BlankTexture;
        public static double scale = 1f;
        //LAYER DEPTHS
        // 1 = top              0 = bottom
        public static float MessageDialogueDepth = 0.4f;
        public static float GameWindowDepth = 0.05f;
        public static float ButtonDepth = 0.51f;
        public static float TextFieldDepth = 0.1f;
        public static float TextDepth = 0.05f;     
        public Core()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            _content = Content; //used to make the content variable public so it can be used in other classes
            _window = Window;
        }

        protected override void Initialize()
        {
            InitialMenu InitialMenu = new InitialMenu(); //Initialises the Initial Menu class which features the first controls the user will see
            Window.IsBorderless = true;
            IsFixedTimeStep = true;
            _graphics.SynchronizeWithVerticalRetrace = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            BlankTexture = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            BlankTexture.SetData(new[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            ElapsedGameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            UIHandler.Update(); //Updates the UI elements
            SystemHandler.Update();

            //Checks to see whether the display resolution has changed whilst the program has been running
            float tempScale = (float)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 1600f;
            if(tempScale != scale)
            {
                //Rescales the window using the new scale
                ChangeWindowSize((int)WindowSize.X, (int)WindowSize.Y);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Vector2 ScaledResolution = new(WindowSize.X * (float)scale, WindowSize.Y * (float)scale);
            RenderTarget2D renderTarget2D = new RenderTarget2D(Core._graphics.GraphicsDevice, (int)WindowSize.X, (int)WindowSize.Y);
            _graphics.GraphicsDevice.SetRenderTarget(renderTarget2D);
            GraphicsDevice.Clear(WindowColor); //sets the window colour
            //RENDER TARGET
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null);
            UIHandler.Draw(); //draws the currently loaded UI to the screen
            _spriteBatch.End();
            _graphics.GraphicsDevice.SetRenderTarget(null);

            //NORMAL VIEWPORT
            SpriteBatch _targetBatch = new SpriteBatch(GraphicsDevice);
            GraphicsDevice.Clear(WindowColor); //sets the window colour
            _targetBatch.Begin(SpriteSortMode.FrontToBack);
            _targetBatch.Draw(renderTarget2D, new Rectangle(0,0,(int)ScaledResolution.X,(int)ScaledResolution.Y), Color.White);
            _targetBatch.End();
            renderTarget2D.Dispose();
            base.Draw(gameTime);
        }
        //Adds in a depth effect to the rectangle passed in as a parameter
        public static void DrawAccent(Rectangle Bounds, int BarWidth, float layerDepth)
        {
            //Draws the top white line for the accent
            Core._spriteBatch.Draw(texture: BlankTexture,
                position: new(Bounds.X, Bounds.Y),
                null,
                color: Color.White,
                rotation: 0,
                origin: Vector2.Zero,
                scale: new Vector2(Bounds.Width, BarWidth),
                SpriteEffects.None,
                layerDepth: layerDepth + 0.01F);
            //Draws the left white line for the accent
            Core._spriteBatch.Draw(texture: BlankTexture,
                position: new(Bounds.X, Bounds.Y),
                null,
                color: Color.White,
                rotation: 0,
                origin: Vector2.Zero,
                scale: new Vector2(BarWidth, Bounds.Height),
                SpriteEffects.None,
                layerDepth: layerDepth + 0.01F);
            //Draws the right black line for the accent
            Core._spriteBatch.Draw(texture: BlankTexture,
                position: new(Bounds.Right - BarWidth, Bounds.Y),
                null,
                color: Color.Black,
                rotation: 0,
                origin: Vector2.Zero,
                scale: new Vector2(BarWidth, Bounds.Height),
                SpriteEffects.None,
                layerDepth: layerDepth + 0.02F);
            //Draws the bottom black line for the accent
            Core._spriteBatch.Draw(texture: BlankTexture,
                position: new(Bounds.X, Bounds.Bottom - BarWidth),
                null,
                color: Color.Black,
                rotation: 0,
                origin: Vector2.Zero,
                scale: new Vector2(Bounds.Width, BarWidth),
                SpriteEffects.None,
                layerDepth: layerDepth + 0.02F);
            //Draws the right grey line for the window
            Core._spriteBatch.Draw(texture: BlankTexture,
                position: new(Bounds.Right - BarWidth * 2, Bounds.Y),
                null,
                color: new Color(131, 131, 131),
                rotation: 0,
                origin: Vector2.Zero,
                scale: new Vector2(BarWidth, Bounds.Height - BarWidth),
                SpriteEffects.None,
                layerDepth: layerDepth + 0.02F);
            //Draws the bottom grey line for the window
            Core._spriteBatch.Draw(texture: BlankTexture,
                position: new(Bounds.X, Bounds.Bottom - BarWidth * 2),
                null,
                color: new Color(131, 131, 131),
                rotation: 0,
                origin: Vector2.Zero,
                scale: new Vector2(Bounds.Width - BarWidth, BarWidth),
                SpriteEffects.None,
                layerDepth: layerDepth + 0.02F);
        }

        public static void ChangeWindowSize(int width, int height)
        {
            WindowSize = new Vector2(width,height);
            //Calculates the scale that should be used for the window dimensions
            scale = (float)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 1600f;
            //Uses the scale to calculate the new window size
            Vector2 ScaledResolution = new(WindowSize.X * (float)scale, WindowSize.Y * (float)scale);
            _graphics.PreferredBackBufferWidth = (int)ScaledResolution.X;
            _graphics.PreferredBackBufferHeight = (int)ScaledResolution.Y;
            _graphics.ApplyChanges();
        }
    }
}