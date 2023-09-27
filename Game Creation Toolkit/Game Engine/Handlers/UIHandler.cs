using Game_Creation_Toolkit.Classes;
using Game_Creation_Toolkit.Game_Engine.UI;
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
        static public List<TextField> TextFields = new List<TextField>();
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
        }
        
    }
}
