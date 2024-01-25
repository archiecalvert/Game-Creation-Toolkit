using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TextCopy;

namespace Game_Creation_Toolkit.Game_Engine.UI
{
    public class TextField
    {
        SpriteFont font; //font for the text
        bool isActive = false; //determines whether typing is active or not
        Rectangle MouseRect; //Rectangle storing the mouse position
        public readonly Rectangle FieldBounds; //Stores the location of the background
        Color TextCol; //Stores the colour of the text
        Color BackgroundCol; //Background colour of the text field
        public string Text;
        Texture2D BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1); //Texture used for the background so that it can be coloured
        Timer KeyDelay;//Used for timing in between key presses
        public float layerDepth = Core.TextFieldDepth;
        float TextScale = 1f;
        public TextField(int width, int height, Vector2 Coordinates, string FieldText, SpriteFont FieldFont, Color FontColour, Color FieldColour, float FontScale)
        {
            FieldBounds = new Rectangle((int)Coordinates.X, (int)Coordinates.Y, width, height); //creates a rectangle for the background
            TextCol = FontColour;
            BackgroundCol = FieldColour;
            Text = FieldText;
            font = FieldFont;
            TextScale = FontScale;
            BlankTexture.SetData(new[] { Color.White }); //Sets all the pixels in the blank texture to white so the colour can be changed
            UIHandler.TextFields.Add(this); //Adds the text field to the list so that it can be automatically updated and drawn
            KeyDelay = new Timer(0.12f); //sets the time inbetween key presses
        }
        List<char> forbiddenCharacters = new List<char> { '“', '”' };
        public void Update()
        {
            MouseRect = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1); //Updates the location of the mouse rectangle
            if (MouseRect.Intersects(FieldBounds) && Mouse.GetState().LeftButton == ButtonState.Pressed) //Checks to see if the box is being pressed
            {
                isActive = true;
            }
            if (isActive && !KeyDelay.isActive)
            {

                if (Keyboard.GetState().IsKeyDown(Keys.Back))//checks if the backspace key has been clicked
                {
                    Backspace();
                    KeyDelay.Begin();
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
                    KeyDelay.Begin();
                    List<string> keys = new List<string>();
                    foreach (var key in Keyboard.GetState().GetPressedKeys())
                    {
                        keys.Add(key.ToString()); //Saves a list of the current pressed keys so this can be accessed later on
                    }
                    if (keys.Contains("LeftControl") && keys.Contains("V")) //checks to see whether the paste command is being executed
                    {
                        Console.WriteLine("Attempting to Paste...");
                        Console.WriteLine("Clipboard Content: " +ClipboardService.GetText());
                        foreach (char c in forbiddenCharacters)
                        {
                            if (ClipboardService.GetText().Contains(c))
                            {
                                Console.WriteLine("Error! Invalid Character Present in Clipboard.");
                                return; //this will break out of the update loop when an invalid character has attempted to be pasted
                            }
                        }
                        Text += ClipboardService.GetText(); //Adds the text in the clipboard to text
                        return;
                    }
                    for (int i = 0; i < Keyboard.GetState().GetPressedKeys().Length; i++)
                    {
                        if (Keyboard.GetState().GetPressedKeys()[i].ToString().Length == 1) //checks to see if a character has been clicked rather than a command key
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift) || Keyboard.GetState().CapsLock)//Checks the capitalisation
                            {
                                Text += Keyboard.GetState().GetPressedKeys()[i]; //adds the uppercase letter to the text variable
                            }
                            else
                            {
                                Text += Keyboard.GetState().GetPressedKeys()[i].ToString().ToLower(); //adds the lowercase letter to the text variable
                            }
                        }
                        else if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        {
                            Text += " "; //adds a space to the text variable when the space bar is pressed
                        }
                        else if (Keyboard.GetState().GetPressedKeys()[i].ToString().Contains("D") && Keyboard.GetState().GetPressedKeys()[i].ToString().Length > 1)
                        {
                            Text += Keyboard.GetState().GetPressedKeys()[i].ToString()[1]; //This section of code is used to allow numbers to be entered into the field as theyre stored as D"n"
                        }
                        else if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
                        {
                            Text += "-";
                        }
                        else if(Keyboard.GetState().IsKeyDown(Keys.OemPeriod))
                        {
                            Text += ".";
                        }
                    }
                    
                }
            }
        }
        public void Draw()
        {
            Core._spriteBatch.Draw(texture: BlankTexture,
                position: new(FieldBounds.X, FieldBounds.Y),
                null,
                color: BackgroundCol,
                rotation: 0,
                origin: Vector2.Zero,
                scale: new Vector2(FieldBounds.Width, FieldBounds.Height),
                SpriteEffects.None,
                layerDepth: layerDepth); //draws the background of the text field
            DrawAccent(4);
            Core._spriteBatch.DrawString(font, Text, new Vector2(FieldBounds.X + (13), FieldBounds.Y + (25*(TextScale-0.2f))), TextCol, 0f, Vector2.Zero, TextScale, SpriteEffects.None, layerDepth + 0.01f); //draws the text stored to the text field
        }
        void Backspace()
        {
            //Removes the last character from the text
            int len = Text.Length;
            string temp = "";
            for (int i = 0; i < len - 1; i++)
            {
                temp = "" + (temp + Text[i]);
            }
            Text = temp;
        }
        void DrawAccent(int barWidth)
        {
            Core._spriteBatch.Draw(texture: BlankTexture,
                position: new(FieldBounds.X, FieldBounds.Y),
                null,
                color: Color.White,
                rotation: 0,
                origin: Vector2.Zero,
                scale: new Vector2(FieldBounds.Width, barWidth),
                SpriteEffects.None,
                layerDepth: layerDepth+0.01F); //draws the background of the text field
            Core._spriteBatch.Draw(texture: BlankTexture,
                position: new(FieldBounds.X, FieldBounds.Y),
                null,
                color: Color.White,
                rotation: 0,
                origin: Vector2.Zero,
                scale: new Vector2(barWidth, FieldBounds.Height),
                SpriteEffects.None,
                layerDepth: layerDepth + 0.01F); //draws the background of the text field
            Core._spriteBatch.Draw(texture: BlankTexture,
                position: new(FieldBounds.Right - barWidth, FieldBounds.Y),
                null,
                color: Color.Black,
                rotation: 0,
                origin: Vector2.Zero,
                scale: new Vector2(barWidth, FieldBounds.Height),
                SpriteEffects.None,
                layerDepth: layerDepth + 0.02F); //draws the background of the text field
            Core._spriteBatch.Draw(texture: BlankTexture,
                position: new(FieldBounds.X, FieldBounds.Bottom - barWidth),
                null,
                color: Color.Black,
                rotation: 0,
                origin: Vector2.Zero,
                scale: new Vector2(FieldBounds.Width, barWidth),
                SpriteEffects.None,
                layerDepth: layerDepth + 0.02F); //draws the background of the text field
            Core._spriteBatch.Draw(texture: BlankTexture,
                position: new(FieldBounds.Right - barWidth * 2, FieldBounds.Y),
                null,
                color: new Color(131, 131, 131),
                rotation: 0,
                origin: Vector2.Zero,
                scale: new Vector2(barWidth, FieldBounds.Height - barWidth),
                SpriteEffects.None,
                layerDepth: layerDepth + 0.02F); //draws the background of the text field
            Core._spriteBatch.Draw(texture: BlankTexture,
                position: new(FieldBounds.X, FieldBounds.Bottom - barWidth * 2),
                null,
                color: new Color(131,131,131),
                rotation: 0,
                origin: Vector2.Zero,
                scale: new Vector2(FieldBounds.Width - barWidth, barWidth),
                SpriteEffects.None,
                layerDepth: layerDepth + 0.02F); //draws the background of the text field
        }
    }
}
