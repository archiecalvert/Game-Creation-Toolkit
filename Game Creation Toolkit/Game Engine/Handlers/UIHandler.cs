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
    internal class UIHandler
    {
        static public List<ContentWindow> Windows = new List<ContentWindow>();
        static public List<Button> Buttons = new List<Button>();
        public void Update()
        {
            foreach (var window in Windows)
            {
                window.Update();
            }
            foreach (var button in Buttons)
            {
                //button.Update();
            }
        }

        public void Draw()
        {
            foreach(var window in Windows)
            {
                window.Draw();
            }
            foreach(var  button in Buttons)
            {
                //button.Draw();
            }
        }
    }
}
