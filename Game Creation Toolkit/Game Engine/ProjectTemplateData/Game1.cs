/*
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace REPLACETHISNAMESPACE //This is a placeholder for the users projects name
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        public static Color BackgroundColour = Color.White;
        public static string ProjectDirectory;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            //This is a placeholder for ther users project directory
            ProjectDirectory = REPLACEPROJECTDIRECTORY;
            ObjectHandler.Initialise();
            //Changes the windows size
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            ObjectHandler.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(BackgroundColour);
            _spriteBatch.Begin();
            ObjectHandler.Draw();
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
    internal class ObjectHandler
    {
        //This class is responsible for storing all of the users project data and adds it to the users project when run
        public static List<Scene> SceneList;
        public static void Initialise()
        {
            SceneList = new List<Scene>();
            //Loads each scene into the users project
            foreach (string Scene in Directory.GetDirectories(Game1.ProjectDirectory + "\\GameData\\Scenes\\"))
            {
                //Extracts the users scene name as the variable Scene is a whole file location
                string name = Scene.Substring(Game1.ProjectDirectory.Length + 17);
                SceneList.Add(new Scene(Scene.Substring(Game1.ProjectDirectory.Length + 17)));
            }
        }
        public static void Update()
        {
            //Calls the Update procedure in each scene
            foreach (Scene scene in SceneList)
            {
                scene.Update();
            }
        }
        public static void Draw()
        {
            //Calls the Draw procedure in each scene
            foreach (Scene scene in SceneList)
            {
                scene.Draw();
            }
        }
        public static Script DetermineScriptType(string JSONObject)
        {
            //Looks through the ID of each script in the users object
            if (JSONObject.Contains("\"id\":\"Texture\""))
            {
                return new Texture(JSONObject);
            }
            return new Coordinate();
        }
    }
    internal class Scene
    {
        public List<Entity> GameObjects;
        public string sceneName;
        public Scene(string SceneName)
        {
            sceneName = SceneName;
            ObjectHandler.SceneList.Add(this);
            GameObjects = new List<Entity>();
            //Loads each game object in the users scene
            foreach (string obj in Directory.GetDirectories(Game1.ProjectDirectory + "\\GameData\\Scenes\\" + SceneName + "\\"))
            {
                //Extracts the name of the object as the variable obj is a whole file location
                string name = obj.Substring(Game1.ProjectDirectory.Length + new string("\\GameData\\Scenes\\" + SceneName + "\\").Length);
                GameObjects.Add(new Entity(name, SceneName));
            }
        }
        public void Update()
        {
            //Calls the update procedure for each game object
            foreach (Entity entity in GameObjects)
            {
                entity.Update();
            }
        }
        public void Draw()
        {
            //Calls the draw procedure for each game object
            foreach (Entity entity in GameObjects)
            {
                entity.Draw();
            }
        }
    }
    internal class Entity
    {
        public Vector2 Coordinates;
        public List<Script> Scripts;
        public Entity(string ObjectName, string SceneName)
        {
            Scripts = new List<Script>();
            //Adds all of the scripts to the game object
            foreach (string line in File.ReadAllLines(Game1.ProjectDirectory + "\\GameData\\Scenes\\" + SceneName + "\\" + ObjectName + "\\object.dat"))
            {
                Scripts.Add(ObjectHandler.DetermineScriptType(line));
            }
        }
        public void Update()
        {
            //Updates each script in the game object
            foreach (Script script in Scripts)
            {
                script.Update();
            }
        }
        public void Draw()
        {
            //Draws each of the scripts in the game object
            foreach (Script script in Scripts)
            {
                script.Draw();
            }
        }

    }
    internal class Texture : Script
    {
        string TextureData;
        Texture2D texture;
        public Texture(string ObjectData)
        {
            TextureData = ObjectData;
            int directoryLength = 0;
            string directory;
            for (int i = 0; i < TextureData.Length; i++)
            {
                //Gets the position of the last " character at the end of the directory of the image
                if (TextureData[28 + i] == '"')
                {
                    directoryLength = i;
                    directory = TextureData.Substring(28, directoryLength);
                    texture = Texture2D.FromFile(Game1._graphics.GraphicsDevice, directory);
                    break;
                }
            }
        }
        public override void Update()
        {
            return;
        }
        public override void Draw()
        {
            //Draws the texture to the screen
            Game1._spriteBatch.Draw(texture,
             position: new Vector2(1600/2, 1000/2),
              null,
               color:Color.White,
                0f,
                 origin:new Vector2(texture.Width/2, texture.Height/2),
                  Vector2.One,
                   SpriteEffects.None,
                    1f);
        }
    }
    internal abstract class Script
    {
        public Script()
        {

        }
        public abstract void Update();
        public abstract void Draw();

    }
    internal class Coordinate : Script
    {
        public Coordinate() { }
        public override void Update()
        {

        }
        public override void Draw()
        {
        }
    }
}
*/