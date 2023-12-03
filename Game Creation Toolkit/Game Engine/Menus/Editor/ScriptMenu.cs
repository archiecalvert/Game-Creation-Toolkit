using Game_Creation_Toolkit.Classes;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Game_Creation_Toolkit.Game_Engine.UI;
using Game_Creation_Toolkit.Game_Engine.Scripts;
using Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes.ScriptMenu;

namespace Game_Creation_Toolkit.Game_Engine.Menus.Editor
{
    public class ScriptMenu : ContentWindow
    {
        SpriteFont TextFont = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont");
        static int width = 410;
        Rectangle MenuBounds = new Rectangle(2460- width - 10,60,width,1430);
        Texture2D BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1);
        Button AddNewBtn; //Used to add new objects to the project
        public string CurrentItemDirectory = "g6";
        public ScriptMenu()
        {
            BlankTexture.SetData(new[] {Color.White});
            Texture2D AddNewTexture = Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Project Tree/AddNew");
            AddNewBtn = new Button(AddNewTexture,
                Position: new Vector2((int)MenuBounds.X + MenuBounds.Width - (1.5f*AddNewTexture.Width) - 5, MenuBounds.Y + 5),
                Scale: new Vector2(1.5f));
        }
        public override void Update()
        {
            if (CurrentItemDirectory == "")
            {
                AddNewBtn.isClicked = false;
            }
            if (AddNewBtn.isClicked)
            {
                AddNewBtn.isClicked = false;
                int width = 1500;
                int height = 750;
                AddScript AddScript = new AddScript((2460 - width) / 2, (1500 - height) / 2, 1500, 750);
            }
        }
        public override void Draw()
        {
            
            Core._spriteBatch.Draw(BlankTexture, MenuBounds, new Color(96, 96, 96));
            Core._spriteBatch.DrawString(spriteFont: TextFont,
                text: "Scripts",
                position: new Vector2((MenuBounds.X + 10), MenuBounds.Y + 3),
                color: Color.White,
                rotation: 0f,
                origin: Vector2.Zero,
                scale: 0.35f,
                effects: SpriteEffects.None,
                layerDepth: Core.TextDepth
                );
            
        }
        public override void UnloadWindow()
        {
            
        }
        public void LoadScripts(string DataDirectory)
        {

        }
    }
}
