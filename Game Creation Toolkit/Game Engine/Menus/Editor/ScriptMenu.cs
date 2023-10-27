using Game_Creation_Toolkit.Classes;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace Game_Creation_Toolkit.Game_Engine.Menus.Editor
{
    public class ScriptMenu : ContentWindow
    {
        SpriteFont TextFont = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont");
        static int width = 650;
        Rectangle MenuBounds = new Rectangle(2460- width - 10,60,width,1430);
        Texture2D BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1);
        public ScriptMenu()
        {
            BlankTexture.SetData(new[] {Color.White});
        }
        public override void Update()
        {
        }
        public override void Draw()
        {
            Core._spriteBatch.Draw(BlankTexture, MenuBounds, new Color(96, 96, 96));
            Core._spriteBatch.DrawString(spriteFont:TextFont,
                text: "Scripts",
                position: new Vector2((MenuBounds.X + 10), MenuBounds.Y + 3),
                color: Color.White,
                rotation: 0f,
                origin: Vector2.Zero,
                scale:0.35f,
                effects:SpriteEffects.None,
                layerDepth:0
                );
            
        }
        public override void UnloadWindow()
        {
            
        }
    }
}
