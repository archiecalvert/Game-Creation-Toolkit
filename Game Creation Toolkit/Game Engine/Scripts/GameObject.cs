using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Menus.Editor;
using Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes.ScriptMenu.MenuItems;
using Game_Creation_Toolkit.Game_Engine.Scripts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace Game_Creation_Toolkit.Game_Engine.Base_Classes
{
    public class GameObject
    {
        public string sceneName;
        public string id;
        public readonly int entityID;
        public List<Script> Scripts = new List<Script>();
        public GameObject(string SceneName, string ObjectName, int EntityID)
        {
            sceneName = SceneName;
            id = ObjectName;
            entityID = EntityID;
            ReloadScripts();
        }
        public void Update()
        {
            if(MainEditor.ScriptMenu.ReloadFlag)
            {
                ReloadScripts() ;
            }
            //Updates every scene each frame
            foreach (Script script in Scripts)
            {
                if(script!=null) script.Update();
            }
        }
        public void Draw()
        {
            //Draws every scene to the screen (if applicable)
            foreach(Script script in Scripts)
            {
                if(script!=null) script.Draw();
            }
        }
        public void ReloadScripts()
        {
            Scripts.Clear();
            var scripts = new List<dynamic>();
            //Loads all of the scripts stored in the objects data file
            foreach (string line in File.ReadLines(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes\\" + sceneName + "\\" + id + "\\object.dat"))
            {
                scripts.Add(JsonConvert.DeserializeObject(line));
            }
            //decides which script each item is and creates a menu item for each
            foreach (var item in scripts)
            {
                Scripts.Add(DetermineScriptType(entityID, item));
            }
        }
        //this will take in script data and return the type of script the data is
        //this is used to add in the correct script to the game objects
        public Script DetermineScriptType(int id, dynamic script)
        {
            switch ((string)(script["id"]))
            {
                case "Texture":
                    return new Texture(id, script, sceneName);
                case "Map":
                        return new Map(script);                   
                default:
                    return null;
            }
        }
    }
}
