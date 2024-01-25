using Game_Creation_Toolkit.Game_Engine.Handlers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Game_Creation_Toolkit.Game_Engine.UI
{
    public abstract class MessageBox
    {
        Texture2D BlankTexture;
        Color WindowColor = new Color(240,240,240);
        public MessageBox()
        {
            BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1);
            BlankTexture.SetData(new[] { Color.White });
            UIHandler.MessageBoxes.Add(this);
            UIHandler.inFocus = false;
        }
        public abstract void Draw();
        public void DrawBackground(Rectangle Bounds)
        {
            //Core.DrawAccent(Bounds, 7, Core.MessageDialogueDepth);
            Core._spriteBatch.Draw(texture: BlankTexture,
                position: new(Bounds.X,Bounds.Y),
                null,
                color: WindowColor,
                rotation: 0,
                origin: Vector2.Zero,                                   //Draws the message box background
                scale: new Vector2(Bounds.Width,Bounds.Height),
                SpriteEffects.None,
                layerDepth: Core.MessageDialogueDepth);
            Core._spriteBatch.Draw(texture: BlankTexture,
                position: new(Bounds.X - 5, Bounds.Y - 5),
                null,
                color: new Color(180, 180, 180),                            //Draws the border of the message box
                rotation: 0,
                origin: Vector2.Zero,
                scale: new Vector2(Bounds.Width + 10, Bounds.Height + 10),
                SpriteEffects.None,
                layerDepth: Core.MessageDialogueDepth - 0.01f);

        }
        public abstract void Update();
        public void Exit()
        {
            UIHandler.inFocus = true;
            UIHandler.MessageBoxes.Remove(this);
        }
    }
}
