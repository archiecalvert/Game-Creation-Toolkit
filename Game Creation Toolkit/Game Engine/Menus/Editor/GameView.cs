using Game_Creation_Toolkit.Classes;
using Game_Creation_Toolkit.Game_Engine.Handlers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Menus.Editor
{
    public class GameView : ContentWindow
    {
        Texture2D BlankTexture;
        Rectangle WindowBounds;
        Color WindowColour = new Color(int.Parse(SystemHandlers.WindowColour), int.Parse(SystemHandlers.WindowColour), int.Parse(SystemHandlers.WindowColour));
        public GameView()
        {
            BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1);
            BlankTexture.SetData(new[] { Color.White });
            WindowBounds = new Rectangle(830, 400, 800, 480);
        }
        public override void Update()
        {
            
        }
        public override void Draw()
        {
            Core._spriteBatch.Draw(BlankTexture, WindowBounds, WindowColour);
        }
        public override void UnloadWindow()
        {
            
        }
    }
}
