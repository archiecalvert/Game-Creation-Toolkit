using Assimp.Configs;
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
        Rectangle MenuBounds = new Rectangle(10, 60, width, 1430); //Window location and size
        SpriteFont TextFont = Core._content.Load<SpriteFont>("Toolkit/Fonts/defaultfont");
        Button AddNewBtn; //Used to add new objects to the project
        Dictionary<string, Button> ScenesDict = new Dictionary<string, Button>(); //Holds a list of file names and their buttons
        public bool ReadingData = false;
        
        Dictionary<string, Dictionary<string, Button>> SubFiles = new Dictionary<string, Dictionary<string, Button>>(); //Holds a list of the
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
            for (int i = 0; i < ScenesDict.Count; i++)
            {
                int subIndex = 0;
                foreach (Button btn in SubFiles.Values.ElementAt(i).Values)
                {
                    if (btn.isClicked)
                    {
                        //When a directory is clicked, the value of the directory is stored to allow for it to be easily accessed
                        btn.isClicked = false;
                        MainEditor.ScriptMenu.CurrentItemDirectory = SubFiles.Values.ElementAt(i).Keys.ElementAt(subIndex);
                        MainEditor.CurrentScene = ScenesDict.Keys.ElementAt(i);
                        MainEditor.ScriptMenu.ReloadFlag = true;
                    }
                    subIndex++;
                }
            }
            int sceneIndex = 0;
            //iterates through each scene and determines if the scene has been clicked
            foreach(Button btn in ScenesDict.Values)
            {
                if(btn.isClicked)
                {
                    btn.isClicked = false;
                    MainEditor.CurrentScene = ScenesDict.Keys.ElementAt(sceneIndex);
                }
                sceneIndex++;
            }
            UpdateList();
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
            try //Validates the files in the list
            {
                foreach (var s in ScenesDict)
                {
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
                    int subindex = 0; //used to iterate over the sub directories
                                      //used to count the character count of the directory up until the folder name
                    int charCount = new string(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes\\" + s.Key + "\\").Length;
                    //used to store the list of names of sub files in the directory
                    string[] subFiles = Directory.GetDirectories(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes\\" + s.Key);
                    foreach (Button btn in SubFiles.Values.ElementAt(index).Values)
                    {
                        //Draws the names of the files and sub directories
                        Core._spriteBatch.DrawString(spriteFont: TextFont,
                            text: subFiles[subindex].Substring(charCount),
                            position: new Vector2(s.Value.ButtonRect.X + 30, s.Value.ButtonRect.Y + (35 * (1 + subindex))),
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
            } catch { }
        }
        public override void UnloadWindow()
        {

        }
        public void UpdateList()
        {
            try
            {
                //removes them from the UIHandler class and clears the lists
                foreach (Button btn in ScenesDict.Values)
                {
                    UIHandler.Buttons.Remove(btn);
                }
                foreach (var item in SubFiles.Values)
                {
                    foreach (Button btn in item.Values)
                    {
                        UIHandler.Buttons.Remove(btn);
                    }
                }
                ScenesDict.Clear();
                SubFiles.Clear();
                //the item value in the list
                int index = 0;
                int charCount = new string(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes\\").Length;
                SceneListLength = Directory.GetDirectories(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes").Count();
                //adds the directories and buttons to the dictionaries
                foreach (string scene in Directory.GetDirectories(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes"))
                {
                    //Adds the scene name in the first element, and the corresponding button in the second element
                    ScenesDict.Add(scene.Substring(charCount), new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Project Tree/Back1"),
                        new Vector2(MenuBounds.X, MenuBounds.Y + 50 + (index * 35)),
                        Vector2.One));
                    index++;
                    //Adds all the game objects inside each scene to the list
                    //Dictionary where the first element is the scene name, and the second is a dictionary
                    //The second element dictionary stores the game objects names and the buttons associated with each
                    SubFiles.Add(ScenesDict.Keys.ElementAt(ScenesDict.Count - 1), new Dictionary<string, Button>());
                    foreach (string subfile in Directory.GetDirectories(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes\\" + scene.Substring(charCount)))
                    {
                        SubFiles.Values.ElementAt(ScenesDict.Count - 1).Add(subfile, new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Project Tree/Back2"),
                        new Vector2(MenuBounds.X, MenuBounds.Y + 50 + (index * 35)),
                        Vector2.One));
                        index++;
                    }
                }
            }
            catch { }
        }
    }
}
