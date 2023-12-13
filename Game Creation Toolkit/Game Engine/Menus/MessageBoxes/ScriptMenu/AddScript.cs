using Game_Creation_Toolkit.Game_Engine.Handlers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Button = Game_Creation_Toolkit.Game_Engine.UI.Button;
using MessageBox = Game_Creation_Toolkit.Game_Engine.UI.MessageBox;
namespace Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes.ScriptMenu
{
    public class AddScript : MessageBox
    {
        Rectangle Bounds;
        SpriteFont Font = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont"); //loads in the default font for the application
        Button CloseBtn;
        Button TextureButton;
        Texture2D BlankTexture;
        List<Button> Buttons = new List<Button>();
        public string directory;

        public AddScript(int x, int y, int width, int height)
        {
            BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1);
            BlankTexture.SetData(new[] { Color.White });
            Bounds = new Rectangle(x, y, width, height);
            CloseBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MessageBox/Cancel"),
                new Vector2(x + (width / 2) - (0.6f * Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/Cancel").Width / 2), y + height - 60),
                new(0.6f));
            Buttons.Add(CloseBtn);
            TextureButton = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MessageBox/AddScriptMenu/Texture"),
                new Vector2(Bounds.X + 55, Bounds.Y+130),
                new(0.6f));
            Buttons.Add(TextureButton);
        }
        public override void Draw()
        {
            DrawBackground(Bounds); //draws the background 2 rectangles that is present in each message box.
            Core._spriteBatch.DrawString(spriteFont: Font,
                text: "Select Script Type",
                position: new Vector2(Bounds.X + 55, Bounds.Y + 50),
                color: Color.Black,
                rotation: 0f,                                           //Draws the description of the message box
                origin: Vector2.Zero,
                scale: 0.5f,
                SpriteEffects.None,
                layerDepth: 0.5f);
        }
        public override void Update()
        {
            //These update calls won't be automatically called in UIHandler as "inFocus = false" so they are skipped directly over
            foreach (Button button in Buttons)
            {
                button.Update();
            }
            if (CloseBtn.isClicked)
            {
                CloseBtn.isClicked = false;
                DisposeMenu();
            }
            if(TextureButton.isClicked)
            {
                TextureButton.isClicked = false;
                DisposeMenu();
                AddTextureMenu addTextureMenu = new AddTextureMenu();
            }
        }
        void DisposeMenu()
        {
            //Removes all UI elements when cancel is pressed.
            Exit();
            foreach (Button btn in Buttons)
            {
                UIHandler.Buttons.Remove(btn);
            }
        }
        
    }
}

