using Game_Creation_Toolkit.Classes;
using Game_Creation_Toolkit.Game_Engine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Handlers
{
    public class UIHandler
    {
        static public List<ContentWindow> Windows = new List<ContentWindow>(); //holds a list of all the current windows currently being displayed on screen
        static public List<Button> Buttons = new List<Button>(); //holds a list of all the current buttons currently being drawn to the screen
        public static void Update()
        {
            //this method iterates through all the currently loaded UI and updates them all
            foreach (var window in Windows)
            {
                window.Update();
            }
            foreach (var button in Buttons)
            {
                button.Update();
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
        }
    }
}
