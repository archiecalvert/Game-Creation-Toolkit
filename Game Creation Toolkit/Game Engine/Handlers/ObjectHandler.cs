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
        public static Dictionary<KeyValuePair<int, Entity>, List<Script>> GameObjects = new Dictionary<KeyValuePair<int, Entity>, List<Script>>();

        public static List<KeyValuePair<int, string>> EntityIDTable = new List<KeyValuePair<int, string>>();
        public static void LoadEntityTable()
        {
            //This procedure loads all of the data in the id.csv file
            //Clears the EntityIDTable so new data can be added
            EntityIDTable.Clear();
            //Loads each record into the EntityID table
            foreach(string line in File.ReadLines(SystemHandler.CurrentProjectDirectory + "\\GameData\\id.csv"))
            {
                string[] parts = line.Split(',');
                EntityIDTable.Add(new KeyValuePair<int, string>(int.Parse(parts[0]), parts[1]));
            }
        }
        public static void AddIDToEntity(string EntityFileLocation)
        {
            //This procedure will assign any game object a unique ID
            List<string> data = new List<string>();
            bool inserted = false;
            int lineCount = 0;
            //Checks to see whether there is an empty space already in the entityid table
            foreach (string line in File.ReadAllLines(SystemHandler.CurrentProjectDirectory + "\\GameData\\id.csv"))
            {
                string[] parts = line.Split(',');
                if (parts[1] == "null")
                {
                    data.Add(parts[0] + EntityFileLocation);
                    inserted = true;
                }
                else
                {
                    data.Add(line);
                }
                lineCount++;
            }
            //if there wasnt a free space in the table, it will be added at the end
            if (!inserted)
            {
                data.Add(lineCount + "," + EntityFileLocation);
            }
            //Writes each item to the .csv file
            using (StreamWriter sw = new StreamWriter(SystemHandler.CurrentProjectDirectory + "\\GameData\\id.csv"))
            {
                foreach (string line in data)
                {
                    sw.WriteLine(line);
                }
                sw.Close();
            }
        }
        public static void AddScriptToEntity(string EntityFileLocation, ScriptItem Script)
        {
            
        }
        public static void RemoveEntityFromTable(string FileName)
        {
            int index = 0;
            //Goes through each entity and looks for the name in the table
            foreach(KeyValuePair<int, string> entity in EntityIDTable)
            {
                //if the entity is found, it will be nulled
                if (FileName == entity.Value)
                {
                    string[] data = File.ReadAllLines(SystemHandler.CurrentProjectDirectory + "\\GameData\\id.csv");
                    int id = int.Parse(data[index].Split(",")[0]);
                    data[index] = id + ",null";
                    return;
                }
            }
        }
        public static void SaveEntityData()
        {
            //writes all the data in the table back to the .csv file
            using(StreamWriter sw = new StreamWriter(SystemHandler.CurrentProjectDirectory + "\\GameData\\id.csv"))
            {
                foreach(KeyValuePair<int, string> entity in EntityIDTable)
                {
                    sw.WriteLine(entity.Key + "," + entity.Value);
                }
                sw.Close();
            }
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
        //this methodd returns the script type of the json object passed in
        public static Script DetermineScriptType(int id, dynamic script)
        {
            switch ((string)(script["id"]))
            {
                case "Texture":
                    return new Texture(id, script);
                default:
                    return null;
            }   
        }
    }
}
