using Game_Creation_Toolkit.Classes;
using Game_Creation_Toolkit.Game_Engine.Handlers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Creation_Toolkit.Game_Engine.Menus.Editor
{
    public class GameView : ContentWindow
    {
        Texture2D BlankTexture;
        public Rectangle WindowBounds;
        Color WindowColour = new Color(int.Parse(SystemHandler.WindowColour), int.Parse(SystemHandler.WindowColour), int.Parse(SystemHandler.WindowColour));
        public GameView()
        {
            BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1);
            BlankTexture.SetData(new[] { Color.White });
            WindowBounds = new Rectangle(430, 100, 1600, 1000);
        }
        public override void Update()
        {

        }
        public override void Draw()
        {
            //Draws the background of the window
            Core._spriteBatch.Draw(texture: BlankTexture, position: new(WindowBounds.X, WindowBounds.Y),
                null,
                WindowColour,
                rotation:0f,
                origin:Vector2.Zero,
                scale:new Vector2(WindowBounds.Width,WindowBounds.Height),
                SpriteEffects.None,
                layerDepth: Core.GameWindowDepth);
        }
        public override void UnloadWindow()
        {

        }
    }
}
