using Game_Creation_Toolkit.Classes;
using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Menus
{
    public class InitialMenu : ContentWindow
    {
        private static Button NewFileBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/NewFile"), new(50, 150), new(1,1)); //creates a button for making a new file
        private static Button OpenFileBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/OpenFile"), new(50, 325), new(1,1));
        private static Button CloseBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/Close"), new(50, 500), new(1, 1));
        private static SpriteFont DefaultFont = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont"); //loads in the default font for the application
        public override void Update() //overrides the Update method in the ContentWindow Class
        {
            if(NewFileBtn.isClicked)
            {
                NewFileBtn.isClicked = false;
            }
            if(OpenFileBtn.isClicked)
            {
                OpenFileBtn.isClicked= false;
            }
            if(CloseBtn.isClicked)
            {
                CloseBtn.isClicked = false;
                Environment.Exit(0);
            }
        }
        public override void Draw() //overrides the Draw method in the ContentWindow class
        {
            Core._spriteBatch.DrawString(DefaultFont, new string("Select an Option"), new Vector2(50,50),Color.White);
        }
        public override void UnloadWindow()
        {
            return;
        }
    }
}
