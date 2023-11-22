using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;


namespace Game_Creation_Toolkit.Game_Engine.UI
{
    public class Button
    {
        Vector2 pos; //used to set the position of the button when instantiated
        Vector2 scale; //used to set the scale of the button when instantiated
        public Texture2D texture; //used to set the texture of the button when instantiated
        public Rectangle ButtonRect; //rectangle used to check whether the mouse is intersecting the mouse
        public bool isClicked = false;
        public float ClickDelay = 5f;//Duration in between button presses
        Timer ClickTimer; //Makes a timer so the button isn't being pressed multiple times a second as the code is run per frame
        public bool isHover = false;
        public Button(Texture2D ButtonTexture, Vector2 Position, Vector2 Scale) //constructor for when the Button object is initialised elsewhere in the program
        {
            texture = ButtonTexture;
            pos = Position;
            scale = Scale;
            ButtonRect = new Rectangle((int)pos.X, (int)pos.Y, (int)(texture.Width * scale.X), (int)(scale.Y * texture.Height));
            ClickTimer = new Timer(ClickDelay);
            UIHandler.Buttons.Add(this); //adds the button to the list of UI elements currently on screen
        }
        public void Update()
        {
            ClickTimer.Duration = ClickDelay;
            //Checks for intersection between the mouse and the button
            Rectangle MouseRect = new Rectangle((int)Mouse.GetState().X, (int)Mouse.GetState().Y, 1, 1);
            if(MouseRect.Intersects(ButtonRect))
            {
                isHover = true;
            }
            else
            {
                isHover = false;
            }
            if(MouseRect.Intersects(ButtonRect) && Mouse.GetState().LeftButton == ButtonState.Pressed && !ClickTimer.isActive)//Starts the click delay when the button is pressed
            {
                isClicked = true;
                ClickTimer.Begin();
            }
        }
        public void Draw()
        {
            Core._spriteBatch.Draw(texture: texture,
                position: pos,
                null,
                color: Color.White,
                rotation: 0,
                origin: Vector2.Zero,
                scale: scale, 
                SpriteEffects.None, 
                layerDepth: Core.ButtonDepth); //draws the button to the screen
        }
        
    }
}
