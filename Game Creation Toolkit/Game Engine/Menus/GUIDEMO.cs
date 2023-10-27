using Game_Creation_Toolkit.Classes;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Menus
{
    public class GUIDEMO : ContentWindow
    {
        Button TempButton;
        Button ResetButton;
        string TempText = "Button State: Not Pressed";
        public GUIDEMO()
        {
            TempButton = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/tempBtn"), new (50,50), new(0.1f));
            ResetButton = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/resetBtn"), new(50, 200), new(0.1f));
        }
        public override void Update()
        {
            if(TempButton.isClicked)
            {
                TempButton.isClicked = false;
                TempText = "Button State: PRESSED!";
            }
            if(ResetButton.isClicked)
            {
                ResetButton.isClicked = false;
                TempText = "Button State: Not Pressed";
            }
        }
        public override void Draw()
        {
            Core._spriteBatch.DrawString(Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont"), TempText, new(500, 50), Color.Black);
        }
        public override void Initialize()
        {
            Core.WindowColor = Color.White;
        }
        public override void UnloadWindow()
        {
        }
    }
}
