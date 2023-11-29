using Game_Creation_Toolkit.Classes;
using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
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
        Dictionary<string, Button> ScenesDict = new Dictionary<string, Button>();
        Dictionary<Button, List<Button>> SubFiles = new Dictionary<Button, List<Button>>();
        int SceneListLength = 0;
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
            if (AddNewBtn.isClicked)
            {
                AddNewBtn.isClicked = false;
                int width = 1500;
                int height = 750;
                AddObjectMenu AddNewMessage = new AddObjectMenu((2460-width)/2,(1500-height)/2,1500,750);
            }
            if (SceneListLength != Directory.GetDirectories(SystemHandlers.CurrentProjectDirectory + "\\GameData\\Scenes").Count())
            {
                UpdateList();
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
            int index = 0;
            foreach(var s in ScenesDict)
            {
                s.Value.Draw();
                Core._spriteBatch.DrawString(spriteFont: TextFont,
                    text: s.Key,
                    position: new Vector2(s.Value.ButtonRect.X + 10, s.Value.ButtonRect.Y),
                    color: Color.White,
                    rotation: 0f,
                    origin: Vector2.Zero,
                    scale: 0.35f,
                    effects: SpriteEffects.None,
                    layerDepth: 0.52f
                    );
                int subindex = 0;
                int charCount = new string(SystemHandlers.CurrentProjectDirectory + "\\GameData\\Scenes\\"+s.Key+"\\").Length;
                foreach (Button btn in SubFiles.Values.ElementAt(index))
                {
                    string[] subFiles = Directory.GetDirectories(SystemHandlers.CurrentProjectDirectory + "\\GameData\\Scenes\\"+s.Key);
                    btn.Draw();
                    Core._spriteBatch.DrawString(spriteFont: TextFont,
                        text: subFiles[subindex].Substring(charCount),
                        position: new Vector2(s.Value.ButtonRect.X + 30, s.Value.ButtonRect.Y + (35*(1+subindex))),
                        color: Color.White,
                        rotation: 0f,
                        origin: Vector2.Zero,
                        scale: 0.35f,
                        effects: SpriteEffects.None,
                        layerDepth: 0.52f
                        );
                    subindex++;
                }
                index++;
            }
        }
        public override void UnloadWindow()
        {

        }
        public void UpdateList()
        {
            ScenesDict.Clear();
            int index = 0;
            int charCount = new string(SystemHandlers.CurrentProjectDirectory + "\\GameData\\Scenes\\").Length;
            SceneListLength = Directory.GetDirectories(SystemHandlers.CurrentProjectDirectory + "\\GameData\\Scenes").Count();
            foreach (string s in Directory.GetDirectories(SystemHandlers.CurrentProjectDirectory + "\\GameData\\Scenes"))
            {
                ScenesDict.Add(s.Substring(charCount), new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Project Tree/Back1"),
                    new Vector2(MenuBounds.X, MenuBounds.Y + 50 + (index * 35)),
                    Vector2.One));
                index++;
                SubFiles.Add(ScenesDict.Values.ElementAt(ScenesDict.Count -1), new List<Button>());
                foreach (string j in Directory.GetDirectories(SystemHandlers.CurrentProjectDirectory + "\\GameData\\Scenes\\" + s.Substring(charCount)))
                {
                    SubFiles.Values.ElementAt(ScenesDict.Count-1).Add(new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Project Tree/Back2"),
                    new Vector2(MenuBounds.X, MenuBounds.Y + 50 + (index * 35)),
                    Vector2.One));
                    index++;
                }

            }
        }
    }
}
