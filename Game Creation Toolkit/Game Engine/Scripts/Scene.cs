using Game_Creation_Toolkit.Game_Engine.Base_Classes;
using Game_Creation_Toolkit.Game_Engine.Handlers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Scripts
{
    public class Scene
    {
        public List<KeyValuePair<int, string>> EntityIDTable = new List<KeyValuePair<int, string>>();
        public List<GameObject> GameObjects = new List<GameObject>();
        public string id;
        public bool CurrentScene = true;
        public Scene(string Name)
        {
            id = Name;
            while (!File.Exists(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes\\" + Name + "\\id.csv")) { }
            LoadEntityIDTable();
            LoadSavedGameObjects();
        }
        public void Update()
        {
            foreach(GameObject GameObject in GameObjects)
            {
                GameObject.Update();
            }
        }
        public void Draw()
        {
            foreach (GameObject GameObject in GameObjects)
            {
                GameObject.Draw();
            }
        }
        //Loads all of the ids from the .csv file with what game object theyre associated with
        public void LoadEntityIDTable()
        {
            foreach (string line in File.ReadLines(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes\\" + id + "\\id.csv"))
            {
                //splits the data into the id and the game object
                string[] parts = line.Split(',');
                EntityIDTable.Add(new KeyValuePair<int, string>(int.Parse(parts[0]), parts[1]));
            }
        }
        public void AddIDToEntity(string EntityFileLocation)
        {
            //This procedure will assign any game object a unique ID
            List<string> data = new List<string>();
            bool inserted = false;
            int lineCount = 0;
            //Checks to see whether there is an empty space already in the entityid table
            foreach (string line in File.ReadAllLines(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes\\"+id+"\\id.csv"))
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
            
            using (StreamWriter sw = new StreamWriter(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes\\"+id+"\\id.csv"))
            {
                foreach (string line in data)
                {
                    sw.WriteLine(line);
                }
                sw.Close();
            }
            
        }
        //loads all of the game objects for the entities stored in the entity id table
        public void LoadSavedGameObjects()
        {
            GameObjects.Clear();
            foreach(KeyValuePair<int, string> entity in EntityIDTable)
            {
                GameObjects.Add(new GameObject(id, entity.Value.Split("\\")[1], entity.Key));
            }
        }
        public void SaveEntityData()
        {
            //writes all the data in the table back to the .csv file
            using (StreamWriter sw = new StreamWriter(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes\\"+id+"\\id.csv"))
            {
                foreach (KeyValuePair<int, string> entity in EntityIDTable)
                {
                    sw.WriteLine(entity.Key + "," + entity.Value);
                }
                sw.Close();
            }
        }
        
    }
}
