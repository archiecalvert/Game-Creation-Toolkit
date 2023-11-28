using Game_Creation_Toolkit.Game_Engine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Game_Creation_Toolkit.Game_Engine.Handlers;

namespace Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes
{
    public class ErrorMessage : MessageBox
    {
        Rectangle Bounds;
        public string Text = "";
        public string Title = "";
        Button CloseBtn;
        SpriteFont Font = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont"); //loads in the default font for the application

        public ErrorMessage(int x, int y, int width, int height)
        {
            Bounds = new Rectangle(x, y, width, height);
            CloseBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MessageBox/Cancel"),
                new Vector2(x + (width / 2) - (0.6f * Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/Cancel").Width / 2), y + height - 60),
                new(0.6f));
        }
        public override void Draw()
        {
            DrawBackground(Bounds);
            Core._spriteBatch.DrawString(spriteFont: Font,
                text: Title,
                position: new Vector2(Bounds.X, Bounds.Y) + new Vector2(25, 12),
                color: Color.Black,
                rotation: 0f,                                           //Draws the title of the message box
                origin: Vector2.Zero,
                scale: 0.5f,
                SpriteEffects.None,
                layerDepth: Core.MessageDialogueDepth + 0.01f);
            Core._spriteBatch.DrawString(spriteFont: Font,
                text: Text,
                position: new Vector2(Bounds.X, Bounds.Y) + new Vector2(25, 67),
                color: Color.Black,
                rotation: 0f,                                           //Draws the description of the message box
                origin: Vector2.Zero,
                scale: 0.3f,
                SpriteEffects.None,
                layerDepth: Core.MessageDialogueDepth + 0.01f);
        }
        public override void Update()
        {
            CloseBtn.Update();
            if(CloseBtn.isClicked)
            {
                CloseBtn.isClicked = false;
                UIHandler.Buttons.Remove(CloseBtn);
                Exit();
            }
        }
    }
}
