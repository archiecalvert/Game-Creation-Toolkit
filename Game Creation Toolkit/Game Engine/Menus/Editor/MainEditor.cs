using Game_Creation_Toolkit.Classes;
using Game_Creation_Toolkit.Game_Engine.Base_Classes;
using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Scripts;
using Game_Creation_Toolkit.Game_Engine.Tools.Dotnet;
using Game_Creation_Toolkit.Game_Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game_Creation_Toolkit.Game_Engine.Menus.Editor
{
    public class MainEditor : ContentWindow
    {
        public static Rectangle Bounds;
        public static string CurrentScene = "";
        Button CloseBtn;
        Button RunBtn;
        Button CompileBtn;
        Texture2D BlankTexture = new Texture2D(Core._graphics.GraphicsDevice, 1, 1); //Creates a blank texture
        Vector2 RunCoords = new Vector2(1000, 0); //Coordinates of the run button
        public static GameView GameView;
        public static ProjectTree ProjectTree;
        public static ScriptMenu ScriptMenu;
        public MainEditor()
        {
            Bounds = new Rectangle(0, 0, 2460, 50);
            CloseBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Close/Close2"), new Vector2(2379,0), new Vector2(1f));
            CloseBtn.HasHoverHighlight = false;
            RunBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Run"), RunCoords, new(1f));
            CompileBtn = new Button(Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Compile"), new Vector2(1300,0), new(1f));
            BlankTexture.SetData(new[] { Color.White }); //sets the textures data to white
            GameView = new GameView();
            ScriptMenu = new ScriptMenu();
            ProjectTree = new ProjectTree();
            //Responsible for loading in existing data if it exists
            ObjectHandler.LoadSceneData();
        }
        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                foreach(Scene scene in ObjectHandler.SceneData)
                {
                    Console.WriteLine(scene.id);
                    foreach(GameObject obj in scene.GameObjects)
                    {
                        Console.WriteLine(obj.id);
                        Console.WriteLine(obj.Scripts.Count);
                    }
                    Console.WriteLine("------------------------------");
                    Console.WriteLine();
                }
            }
            if (CloseBtn.isHover) //THE RED "X" in the corner. This swaps between the two textures depending on whether the user is hovering over it
            {
                CloseBtn.texture = Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Close/Close1");
            }
            else
            {
                CloseBtn.texture = Core._content.Load<Texture2D>("Toolkit/Assets/MainEditor/Close/Close2");
            }
            if(CloseBtn.isClicked)
            {
                CloseBtn.isClicked = false;
                Environment.Exit(0); //closes the application
            }
            if (RunBtn.isClicked)
            {
                RunBtn.isClicked = false;
                Console.WriteLine(SystemHandler.CurrentProjectDirectory);
                ProjectRun.Begin(SystemHandler.CurrentProjectDirectory); //runs the commands necessary to get the users project running
                
            }
            if (CompileBtn.isClicked)
            {
                CompileBtn.isClicked = false;
                ProjectCompiler.Begin(SystemHandler.CurrentProjectDirectory);
            }
            foreach(Scene Scene in ObjectHandler.SceneData)
            {
                if(Scene!=null) Scene.Update();
            }
        }
        public override void Draw()
        {
            Core._spriteBatch.Draw(BlankTexture, Bounds, new Color(96,96,96)); //draws the background for the nav bar
           
            foreach(Scene Scene in ObjectHandler.SceneData)
            {
                if(Scene!=null) Scene.Draw();
            }
        }
        public override void UnloadWindow()
        {
            System.Environment.Exit(0);
        }
        public override void Initialize()
        {
            //Changes the windows size
            Core._graphics.PreferredBackBufferHeight = 1500;
            Core._graphics.PreferredBackBufferWidth = 2460;
            Core._window.Position = new Point(50,5);
            Core._graphics.ApplyChanges();
            
        }

    }
}
