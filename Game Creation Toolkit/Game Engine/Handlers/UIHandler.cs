using Game_Creation_Toolkit.Classes;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Handlers
{
    public class UIHandler
    {
        //These are the main window lists
        static public List<ContentWindow> Windows = new List<ContentWindow>(); //holds a list of all the current windows currently being displayed on screen
        static public List<Button> Buttons = new List<Button>(); //holds a list of all the current buttons currently being drawn to the screen
        static public List<TextField> TextFields = new List<TextField>();//holds a list of all the current text fields
        static Texture2D HoverTexture = Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/HoverTexture"); //Makes a hover texture so that the button becomes lighter when highlighted
        public static void Update()
        {
            //this method iterates through all the currently loaded UI and updates them all
            for(int i = Windows.Count; i > 0; i--)
            {
                Windows[i-1].Update();
            }
            foreach (var button in Buttons)
            {
                button.Update();
            }
            foreach (var textField in TextFields)
            {
                textField.Update();
            }
        }
        public static void Draw()
        {
            //iterates and draws all of the UI to the screen
            foreach(var window in Windows)
            {
                window.Draw();
            }
            foreach(var  button in Buttons)
            {
                button.Draw();
            }
            foreach(var textField in TextFields)
            {
                textField.Draw();
            }
            foreach (Button button in Buttons)
            {
                if (button.isHover)
                {
                    Core._spriteBatch.Draw(HoverTexture, new Rectangle((int)button.ButtonRect.X, (int)button.ButtonRect.Y, (int)button.ButtonRect.Width, (int)button.ButtonRect.Height), Color.White);
                }
            }
        }
        
    }
}
