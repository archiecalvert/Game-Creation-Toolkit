using Game_Creation_Toolkit.Game_Engine.Base_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Scripts
{
    public class Scene
    {
        public static Dictionary<KeyValuePair<int, Entity>, List<Script>> GameObjects = new Dictionary<KeyValuePair<int, Entity>, List<Script>>();
        public Scene() { }
        public void Update()
        {

        }
        public static void LoadScripts()
        {
            //loads all of the scripts into the gameobjects dictionary
            GameObjects.Clear();
            foreach(KeyValuePair<int, string> entity in EntityIDTable)
            {
                GameObjects.Add(new KeyValuePair<int, Entity>(entity.Key, new Entity(entity.Key)), new List<Script>());
                var scripts = new List<dynamic>();
                //Loads all of the scripts stored in the objects data file
                foreach (string line in File.ReadLines(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes\\" + entity.Value + "\\object.dat"))
                {
                    scripts.Add(JsonConvert.DeserializeObject(line));
                }
                //decides which script each item is and creates a menu item for each
                foreach (var item in scripts)
                {
                    GameObjects.ElementAt(GameObjects.Count-1).Value.Add(DetermineScriptType(entity.Key, item));
                }
            }
        }
        public void Draw() { }
    }
}
