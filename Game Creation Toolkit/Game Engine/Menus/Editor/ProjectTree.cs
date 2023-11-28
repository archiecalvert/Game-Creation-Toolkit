using Game_Creation_Toolkit.Classes;
using Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Menus.Editor
{
    public class ProjectTree : ContentWindow
    {
        Texture2D BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1);
        static int width = 410;
        Rectangle MenuBounds = new Rectangle(10, 60, width, 1430);
        SpriteFont TextFont = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont");
        Button AddNewBtn;
        public ProjectTree()
        {
            BlankTexture.SetData(new[] { Color.White });
            Texture2D AddNewTexture = Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Project Tree/AddNew");
            AddNewBtn = new Button(AddNewTexture,
                Position: new Vector2((int)MenuBounds.X + MenuBounds.Width - (1.5f*AddNewTexture.Width) - 5, MenuBounds.Y + 5),
                Scale: new Vector2(1.5f));
        }
        public override void Update()
        {
            if(AddNewBtn.isClicked)
            {
                AddNewBtn.isClicked = false;
                int width = 1500;
                int height = 750;
                AddObjectMenu AddNewMessage = new AddObjectMenu((2460-width)/2,(1500-height)/2,1500,750);
            }
        }
        public override void Draw()
        {
            Core._spriteBatch.Draw(BlankTexture, MenuBounds, new Color(96, 96, 96));
            Core._spriteBatch.DrawString(spriteFont: TextFont,
                text: "Project",
                position: new Vector2((MenuBounds.X + 10), MenuBounds.Y + 3),
                color: Color.White,
                rotation: 0f,
                origin: Vector2.Zero,
                scale: 0.35f,
                effects: SpriteEffects.None,
                layerDepth: 0
                );
        }
        public override void UnloadWindow()
        {

        }
    }
}
