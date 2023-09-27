using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game_Creation_Toolkit.Game_Engine.Handlers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using Game_Creation_Toolkit.Game_Engine.Tools;
using TextCopy;

namespace Game_Creation_Toolkit.Game_Engine.UI
{
    public class TextField
    {
        Vector2 pos;
        SpriteFont font;
        bool isActive = false;
        Rectangle MouseRect;
        Rectangle FieldBounds;
        Color TextCol;
        Color BackgroundCol;
        Texture2D BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1);
        Timer KeyDelay;
        public string Text;
        public TextField(int width, int height, Vector2 Coordinates, string FieldText, SpriteFont FieldFont, Color FontColour, Color FieldColour, float FontScale)
        {
            FieldBounds = new Rectangle((int)Coordinates.X, (int)Coordinates.Y, width, height);
            TextCol = FontColour;
            BackgroundCol = FieldColour;
            Text = FieldText;
            font = FieldFont;
            BlankTexture.SetData(new[] {Color.White}); //https://stackoverflow.com/questions/5751732/draw-rectangle-in-xna-using-spritebatch
            UIHandler.TextFields.Add(this);
            KeyDelay = new Timer(0.1f);
        }
        public void Update()
        {
            MouseRect = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
            if(MouseRect.Intersects(FieldBounds) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                isActive = true;
            }
            if (isActive && !KeyDelay.isActive)
            {
                KeyDelay.Begin();
                if (Keyboard.GetState().IsKeyDown(Keys.Back))
                {
                    Backspace();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    isActive = false;
                }
                else if (Mouse.GetState().LeftButton == ButtonState.Pressed && !MouseRect.Intersects(FieldBounds))
                {
                    isActive = false;
                }
                else if (Keyboard.GetState().GetPressedKeys().Length != 0)
                {
                    for (int i = 0; i < Keyboard.GetState().GetPressedKeys().Length; i++)
                    {
                        if (Keyboard.GetState().GetPressedKeys()[i].ToString().Length == 1)
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift) || Keyboard.GetState().CapsLock)
                            {
                                Text += Keyboard.GetState().GetPressedKeys()[i];
                            }
                            else
                            {
                                Text += Keyboard.GetState().GetPressedKeys()[i].ToString().ToLower();
                            }
                        }
                        List<string> keys = new List<string>();
                        foreach(var key in Keyboard.GetState().GetPressedKeys())
                        {
                            keys.Add(key.ToString());
                        }
                        if (keys.Contains("LeftControl") && keys.Contains("V"))
                        {
                            Backspace();
                            Text += ClipboardService.GetText();
                        }
                    }
                }
                
            }
        }
        public void Draw()
        {
            Core._spriteBatch.Draw(BlankTexture, FieldBounds, BackgroundCol);
            Core._spriteBatch.DrawString(font, Text, new Vector2(FieldBounds.X+10, FieldBounds.Y), TextCol, 0f, Vector2.Zero,
                0.5f, SpriteEffects.None, 0);
        }
        void Backspace()
        {
            int len = Text.Length;
            string temp = "";
            for (int i = 0; i < len - 2; i++)
            {
                temp = "" + (temp + Text[i]);
            }
            Text = temp;
        }
    }
}
