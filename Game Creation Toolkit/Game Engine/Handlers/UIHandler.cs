using Game_Creation_Toolkit.Classes;
using Game_Creation_Toolkit.Game_Engine.Menus;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
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
        public static List<MessageBox> MessageBoxes = new List<MessageBox>();
        public static bool inFocus = true;
        public static void Update()
        {
            if (inFocus)
            {
                //this method iterates through all the currently loaded UI and updates them all
                for (int i = Windows.Count; i > 0; i--)
                {
                    Windows[i - 1].Update();
                }
                for (int i = Buttons.Count; i > 0; i--)
                {
                    Buttons[i - 1].Update();
                }
                for (int i = TextFields.Count; i > 0; i--)
                {
                    TextFields[i - 1].Update();
                }
            }
            for (int i = MessageBoxes.Count; i > 0; i--)
            {
                MessageBoxes[i - 1].Update();
            }
        }
        public static void Draw()
        {
            //iterates and draws all of the UI to the screen
            for (int i = Windows.Count; i > 0; i--)
            {
                Windows[i - 1].Draw();
            }
            for (int i = Buttons.Count; i > 0; i--)
            {
                Buttons[i - 1].Draw();
            }
            for (int i = TextFields.Count; i > 0; i--)
            {
                TextFields[i - 1].Draw();
            }
            for (int i = MessageBoxes.Count; i > 0; i--)
            {
                MessageBoxes[i - 1].Draw();
            }
        }

    }
}
