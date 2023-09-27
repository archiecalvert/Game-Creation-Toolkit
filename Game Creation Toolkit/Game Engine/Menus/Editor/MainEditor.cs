using Game_Creation_Toolkit.Classes;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game_Creation_Toolkit.Game_Engine.Menus.Editor
{
    public class MainEditor : ContentWindow
    {
        Button CloseBtn;
        public MainEditor()
        {
            CloseBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Close/Close2"), new Vector2(2379,0), new Vector2(1f));
        }
        public override void Update()
        {
            if (CloseBtn.isHover)
            {
                CloseBtn.texture = Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Close/Close1");
            }
            else
            {
                CloseBtn.texture = Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Close/Close2");
            }
            if(CloseBtn.isClicked)
            {
                CloseBtn.isClicked = false;
                Environment.Exit(0);
            }
        }
        public override void Draw()
        {
            
        }
        public override void UnloadWindow()
        {
            
        }
        public override void Initialize()
        {
            Core._graphics.PreferredBackBufferHeight = 1500;
            Core._graphics.PreferredBackBufferWidth = 2460;
            Core._graphics.ApplyChanges();
            
        }
    }
}
