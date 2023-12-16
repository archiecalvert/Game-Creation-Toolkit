using Game_Creation_Toolkit.Game_Engine.Base_Classes;
using Game_Creation_Toolkit.Game_Engine.Menus.MessageBoxes.ScriptMenu;
using Game_Creation_Toolkit.Game_Engine.Scripts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Game_Creation_Toolkit.Game_Engine.Handlers
{
    public static class ObjectHandler
    {
        public static List<Scene> SceneData = new List<Scene>();
        //Creates all of the scenes as objects from the players game directory
        public static void LoadSceneData()
        {
            foreach (string SceneName in Directory.GetDirectories(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes"))
            {
                SceneData.Add(new Scene(SceneName.Substring(SystemHandler.CurrentProjectDirectory.Length + new string("\\GameData\\Scenes\\").Length)));
            }
        }
    }
}

