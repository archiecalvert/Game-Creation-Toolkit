using Assimp;
using Game_Creation_Toolkit.Classes;
using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Menus.Editor;
using Game_Creation_Toolkit.Game_Engine.Menus.Home_Menu;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
namespace Game_Creation_Toolkit.Menus
{
    public class InitialMenu : ContentWindow
    {
        Button NewFileBtn;
        Button OpenFileBtn;
        Button CloseBtn;
        SpriteFont DefaultFont = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont"); //loads in the default font for the application
        Texture2D BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1); //Creates a blank texture

        public InitialMenu()
        {
            NewFileBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/NewFile"), new(50, 170), new(1, 1)); //creates a button for making a new file
            OpenFileBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/OpenFile"), new(50, 345), new(1, 1));//Creates a button for opening an existing project
            CloseBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/Close"), new(50, 520), new(1, 1));//creates a button for closing the application
            BlankTexture.SetData(new[] { Color.White });
        }
        public override void Update() //overrides the Update method in the ContentWindow Class
        {
            if(NewFileBtn.isClicked)
            {
                NewFileBtn.isClicked = false;
                UnloadWindow();
                NewProjectMenu NewMenu = new NewProjectMenu(); //Opens the window for entering new project info
            }
            if(OpenFileBtn.isClicked)
            {
                OpenFileBtn.isClicked= false;
                OpenProject.OpenGameProject();
                if(OpenProject.directoryFound == true)
                {
                    UnloadWindow();
                    MainEditor mainEditor = new MainEditor();
                }
                OpenProject.directoryFound = null;
                
            }
            if(CloseBtn.isClicked)
            {
                CloseBtn.isClicked = false;
                Environment.Exit(0); //closes the application
            }
        }
        public override void Draw() //overrides the Draw method in the ContentWindow class
        {
            int barWidth = 7;
            Core._spriteBatch.DrawString(DefaultFont, new string("Select an Option"), new Vector2(50,85),Color.Black);//title
            Core._spriteBatch.DrawString(DefaultFont, new string("Game Creation Toolkit"), new(20,17), Color.White, 0f, Vector2.Zero, 0.45f, SpriteEffects.None, 1f);//title
            Core._spriteBatch.Draw(BlankTexture, new Rectangle(barWidth, barWidth, 1920 - barWidth*3, 45), Core.NavColour);//new Color(128,128,128)); //Blue Navigation Bar
            Core._spriteBatch.Draw(BlankTexture, new Rectangle(0, 0, barWidth, 1080 - barWidth), Color.White); //Left White Accent
            Core._spriteBatch.Draw(BlankTexture, new Rectangle(0, 0, 1920 - barWidth, barWidth), Color.White); //Top White Accent
            Core._spriteBatch.Draw(BlankTexture, new Rectangle(1920 - barWidth * 2, 0, barWidth, 1080 - barWidth), new Color(131,131,131));       
            Core._spriteBatch.Draw(BlankTexture, new Rectangle(0, 1080 - barWidth*2, 1920, barWidth), new Color(131, 131, 131));
            Core._spriteBatch.Draw(BlankTexture, new Rectangle(0, 1080 - barWidth, 1920, barWidth), new Color(0, 0, 0));
            Core._spriteBatch.Draw(BlankTexture, new Rectangle(1920 - barWidth, 0, barWidth, 1080), new Color(0, 0, 0));



        }
        public override void UnloadWindow()//this method clears all of the objects associated with this page
        {
            UIHandler.Windows.Remove(this);
            UIHandler.Buttons.Remove(NewFileBtn);
            UIHandler.Buttons.Remove(OpenFileBtn);
            UIHandler.Buttons.Remove(CloseBtn);

        }
        public override void Initialize()//Changes the windows size
        {
            Core._graphics.PreferredBackBufferHeight = 1080;
            Core._graphics.PreferredBackBufferWidth = 1920;
            Core._graphics.ApplyChanges();
        }
    }
}
