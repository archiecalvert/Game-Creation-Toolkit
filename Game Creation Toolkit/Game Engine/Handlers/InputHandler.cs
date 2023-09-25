using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Handlers
{
    public class InputHandler
    {
        public static Vector2 MousePosition;
        public static void Update()
        {
            MousePosition = new(Mouse.GetState().X,Mouse.GetState().Y);
        }
    }
}
