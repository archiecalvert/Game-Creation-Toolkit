using Game_Creation_Toolkit.Classes;
using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Menus.Home_Menu;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Menus
{
    public class InitialMenu : ContentWindow
    {
        Button NewFileBtn;
        Button OpenFileBtn;
        Button CloseBtn;
        SpriteFont DefaultFont = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont"); //loads in the default font for the application
        public InitialMenu()
        {
            NewFileBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/NewFile"), new(50, 150), new(1, 1)); //creates a button for making a new file
            OpenFileBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/OpenFile"), new(50, 325), new(1, 1));//Creates a button for opening an existing project
            CloseBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/Close"), new(50, 500), new(1, 1));//creates a button for closing the application

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
            }
            if(CloseBtn.isClicked)
            {
                CloseBtn.isClicked = false;
                Environment.Exit(0); //closes the application
            }
        }
        public override void Draw() //overrides the Draw method in the ContentWindow class
        {
            Core._spriteBatch.DrawString(DefaultFont, new string("Select an Option"), new Vector2(50,50),Color.White);//title
            
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
