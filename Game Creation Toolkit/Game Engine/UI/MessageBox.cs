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
    public class MessageBox
    {
        public string Title = "Error";
        public string Text = "";
        Texture2D BlankTexture;
        public Vector2 Coordinates;
        Rectangle Bounds;
        Vector2 Dimensions;
        Button CloseBtn;
        SpriteFont Font = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont"); //loads in the default font for the application

        Color WindowColor = Color.White;
        public MessageBox(int x, int y, int width, int height)
        {
            BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1,1);
            Dimensions = new Vector2(width, height);
            BlankTexture.SetData(new[] { Color.White }); //sets the textures data to white
            Coordinates = new Vector2(x,y);
            CloseBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MessageBox/Cancel"), new Vector2(Coordinates.X + (Dimensions.X/2) - (0.6f*Core._content.Load<Texture2D>("Toolkit/Assets/InitialMenu/Cancel").Width/2) , Coordinates.Y + Dimensions.Y - 60), new(0.6f));
            UIHandler.MessageBoxes.Add(this);
            
        }
        public void Draw()
        {
            Bounds = new Rectangle((int)Coordinates.X, (int)Coordinates.Y, (int)Dimensions.X,(int)Dimensions.Y);
            Core._spriteBatch.Draw(texture: BlankTexture,
                position: new(Bounds.X-5, Bounds.Y-5),
                null,
                color: new Color(180,180,180),                            //Draws the border of the message box
                rotation: 0,
                origin: Vector2.Zero,
                scale: new Vector2(Bounds.Width+10, Bounds.Height+10),
                SpriteEffects.None,
                layerDepth: Core.MessageDialogueDepth-0.01f); //draws the button to the screen);
            Core._spriteBatch.Draw(texture: BlankTexture,
                position: new(Bounds.X,Bounds.Y),
                null,
                color: WindowColor,
                rotation: 0,
                origin: Vector2.Zero,                                   //Draws the message box background
                scale: new Vector2(Bounds.Width,Bounds.Height),
                SpriteEffects.None,
                layerDepth: Core.MessageDialogueDepth);
            Core._spriteBatch.DrawString(spriteFont: Font,
                text: Title,
                position: Coordinates + new Vector2(25,12),
                color: Color.Black,
                rotation: 0f,                                           //Draws the title of the message box
                origin: Vector2.Zero, 
                scale: 0.5f,
                SpriteEffects.None,
                layerDepth: Core.MessageDialogueDepth + 0.01f);
            Core._spriteBatch.DrawString(spriteFont: Font, 
                text: Text, 
                position: Coordinates + new Vector2(25, 67),
                color:Color.Black,
                rotation: 0f,                                           //Draws the description of the message box
                origin: Vector2.Zero,
                scale: 0.3f, 
                SpriteEffects.None, 
                layerDepth: Core.MessageDialogueDepth + 0.01f);
        }
        public void Update()
        {
            if (CloseBtn.isClicked)
            {
                CloseBtn.isClicked = false;
                Exit();
            }
        }
        public void Exit()
        {
            UIHandler.MessageBoxes.Remove(this);
            UIHandler.Buttons.Remove(CloseBtn);
        }
    }
}
