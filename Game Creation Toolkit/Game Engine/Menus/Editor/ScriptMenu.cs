using Game_Creation_Toolkit.Classes;
using Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes.ScriptMenu;
using Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes.ScriptMenu.MenuItems;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Game_Creation_Toolkit.Game_Engine.Menus.Editor
{
    public class ScriptMenu : ContentWindow
    {
        SpriteFont TextFont = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont");
        static int width = 400;
        public Rectangle MenuBounds = new Rectangle(2450 - width - 10,60,width,1420);
        Texture2D BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1);
        Button AddNewBtn; //Used to add new objects to the project
        public string CurrentItemDirectory = "";
        public List<ScriptItem> ScriptItems = new List<ScriptItem>();
        public bool ReloadFlag = false;
        string Title = "Scripts";
        public ScriptMenu()
        {
            BlankTexture.SetData(new[] {Color.White});
            Texture2D AddNewTexture = Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Project Tree/AddNew");
            AddNewBtn = new Button(AddNewTexture,
                Position: new Vector2((int)MenuBounds.X + MenuBounds.Width - (1.5f*AddNewTexture.Width) - 15, MenuBounds.Y + 10),
                Scale: new Vector2(1.35f));
        }

        public override void Update()
        {
            if (ReloadFlag)
            {
                ReloadFlag = false;
                foreach(var item in ScriptItems)
                {
                    item.UnloadItem();
                }
                ScriptItems.Clear();
                LoadCurrentObjectScript();
                string[] directories = CurrentItemDirectory.Split('\\');
                Title = "Scripts | " + directories[directories.Count()-1];
            }
            if (CurrentItemDirectory == "")
            {
                AddNewBtn.isClicked = false;
            }
            if (AddNewBtn.isClicked)
            {
                AddNewBtn.isClicked = false;
                if (CurrentItemDirectory != "")
                {
                    int width = 1500;
                    int height = 750;
                    AddScript AddScriptMenu = new AddScript((2460 - width) / 2, (1500 - height) / 2, 1500, 750);
                }
            }
            for (int i = ScriptItems.Count; i > 0; i--)
            {
                ScriptItems[i - 1].Update();
            }
            

        }
        public override void Draw()
        {
            
            Core._spriteBatch.Draw(BlankTexture, MenuBounds, new Color(192,192,192));
            Core._spriteBatch.Draw(BlankTexture, new Rectangle(MenuBounds.X, MenuBounds.Y, MenuBounds.Width, 45),null , Core.NavColour, 0f, Vector2.Zero, SpriteEffects.None, layerDepth:Core.TextDepth-0.01F);
            Core._spriteBatch.DrawString(spriteFont: TextFont,
                text: Title,
                position: new Vector2((MenuBounds.X + 15), MenuBounds.Y + 15),
                color: Color.Black,
                rotation: 0f,
                origin: Vector2.Zero,
                scale: 0.4f,
                effects: SpriteEffects.None,
                layerDepth: Core.TextDepth
                );
            for (int i = ScriptItems.Count; i > 0; i--)
            {
                ScriptItems[i - 1].Draw();
            }
            Core.DrawAccent(MenuBounds, 7, 0.9f);
        }
        public override void UnloadWindow()
        {
            
        }
        public void LoadCurrentObjectScript()
        {
            var objects = new List<dynamic>();
            //Loads all of the scripts stored in the objects file
            foreach(string line in File.ReadLines(CurrentItemDirectory + "\\object.dat"))
            {
                objects.Add(JsonConvert.DeserializeObject(line));
            }
            //decides which script each item is and creates a menu item for each
            foreach (var item in objects)
            {
                switch ((string)(item["id"]))
                {
                    case "Texture":
                        TextureItem TextureItem = new TextureItem(item);
                        break;
                    case "coordinate":
                        CoordinateItem coordinateItem = new CoordinateItem(item);
                        break;
                    case "EntityMovement":
                        MovementItem movementItem = new MovementItem(item);
                        break;
                    case "Camera":
                        CameraItem cameraItem = new CameraItem(item);
                        break;
                    case "Map":
                        MapItem mapItem = new MapItem(item);
                        break;
                    default:
                        break;
                }
            }

        }
        
    }
}
