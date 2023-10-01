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
        Vector2 pos; //position of the text field
        SpriteFont font; //font for the text
        bool isActive = false; //determines whether typing is active or not
        Rectangle MouseRect; //Rectangle storing the mouse position
        Rectangle FieldBounds; //Stores the location of the background
        Color TextCol; //Stores the colour of the text
        Color BackgroundCol; //Background colour of the text field
        Texture2D BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1); //Texture used for the background so that it can be coloured
        Timer KeyDelay;//Used for timing in between key presses
        public string Text;
        public TextField(int width, int height, Vector2 Coordinates, string FieldText, SpriteFont FieldFont, Color FontColour, Color FieldColour, float FontScale)
        {
            FieldBounds = new Rectangle((int)Coordinates.X, (int)Coordinates.Y, width, height); //creates a rectangle for the background
            TextCol = FontColour; 
            BackgroundCol = FieldColour;
            Text = FieldText;
            font = FieldFont;
            BlankTexture.SetData(new[] {Color.White}); //Sets all the pixels in the blank texture to white so the colour can be changed//https://stackoverflow.com/questions/5751732/draw-rectangle-in-xna-using-spritebatch
            UIHandler.TextFields.Add(this); //Adds the text field to the list so that it can be automatically updated and drawn
            KeyDelay = new Timer(0.1f);
        }
        public void Update()
        {
            MouseRect = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1); //Updates the location of the mouse rectangle
            if(MouseRect.Intersects(FieldBounds) && Mouse.GetState().LeftButton == ButtonState.Pressed) //Checks to see if the box is being pressed
            {
                isActive = true;
            }
            if (isActive && !KeyDelay.isActive)
            {
                KeyDelay.Begin();
                if (Keyboard.GetState().IsKeyDown(Keys.Back))//checks if the backspace key has been clicked
                {
                    Backspace();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Enter)) //checks to see if the enter key has been clicked
                {
                    isActive = false;
                }
                else if (Mouse.GetState().LeftButton == ButtonState.Pressed && !MouseRect.Intersects(FieldBounds)) //Checks to see if an area outside of the text field has been clicked
                {
                    isActive = false;
                }
                else if (Keyboard.GetState().GetPressedKeys().Length != 0) 
                {
                    for (int i = 0; i < Keyboard.GetState().GetPressedKeys().Length; i++)
                    {
                        if (Keyboard.GetState().GetPressedKeys()[i].ToString().Length == 1) //checks to see if a character has been clicked rather than a command key
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift) || Keyboard.GetState().CapsLock)//Checks the capitalisation
                            {
                                Text += Keyboard.GetState().GetPressedKeys()[i];
                            }
                            else
                            {
                                Text += Keyboard.GetState().GetPressedKeys()[i].ToString().ToLower();
                            }
                        }
                        //Code below allows for pasting from the clipboard
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
            //Removes the last character from the text
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
