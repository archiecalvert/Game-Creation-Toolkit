using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Menus.Editor;
using Game_Creation_Toolkit.Game_Engine.Scripts;
using Game_Creation_Toolkit.Game_Engine.Tools.NewProject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Tools.Dotnet
{
    public class ProjectFileManager
    {
        //this class is used to insert a new itemgroup so that the file passed in as an arguement will be included in the compilation and build process
        public static void AddFileToProject(string FileDirectory)
        {
            string csproj = SystemHandler.CurrentProjectDirectory + "\\" + SystemHandler.ProjectName + ".csproj";
            string newData;
            using (StreamReader sr = new StreamReader(csproj)) //this will read all of the text in the .csproj file
            {
                string csFile = sr.ReadToEnd();
                int location = FindSpace(csFile); //finds the location where the new itemgroup can be inserted into the project file
                if (location != -1)
                {
                    string textBefore = "";
                    for (int i = 0; i < location; i++) //stores all the data before the part where it will be inserted
                    {
                        textBefore += csFile[i];
                    }
                    string textAfter = "";
                    for (int i = location; i < csFile.Length; i++) //stores all the data after where the part will be inserted
                    {
                        textAfter += csFile[i];
                    }
                    string include = "<ItemGroup>\r\n    <None Update=\"" + FileDirectory + "\">\r\n      <CopyToOutputDirectory>Always</CopyToOutputDirectory>\r\n    </None>\r\n  </ItemGroup>\r\n  ";
                    newData = textBefore + include + textAfter; //inserts the new item group
                    sr.Close();
                    using (StreamWriter sw = new StreamWriter(csproj)) //writes this new data to the project file
                    {
                        sw.WriteLine(newData);
                        sw.Close();            
                    }
                }
                sr.Close();
            }
        }
        static int FindSpace(string file) //locates the most appropriate space to include the new itemgroup
        {
            bool keepReading = true;
            int index = 0;
            while (keepReading || index>file.Length)
            {
                if (file.Substring(index, 16) == "</PropertyGroup>") //looks for the last mention of PropertyGroup in the references file
                {
                    if(file.Substring(index + 17, 30).Contains("<ItemGroup>")) //Checks to see if the next thing referenced is an item group
                    {
                        for(int i = 0; i < 30; i++)
                        {
                            if (file[i+index+17] == '<') //looks for the index where this symbol occurs so that a new reference can be added
                            {
                                return i + index + 17;
                            }
                        }
                    }
                }
                index++;
            }
            return -1;
        }
        public static void AddScene(string SceneName)
        {
            MakeFolder("GameData\\Scenes\\" + SceneName);
            MakeFile("GameData\\Scenes\\"+SceneName+"\\scene.dat");
            MakeFile("GameData\\Scenes\\" + SceneName + "\\id.csv");
            ObjectHandler.SceneData.Add(new Scene(SceneName));
            string sceneData = File.ReadAllText(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes\\scenes.dat");
            if(sceneData == "")
            {
                JSONHandler.SceneDataJSON sceneDataJSON = new JSONHandler.SceneDataJSON { MainScene=SceneName};
                using (StreamWriter sw = new StreamWriter(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes\\scenes.dat"))
                {
                    sw.WriteLine(JsonSerializer.Serialize(sceneDataJSON));
                    sw.Close();
                }
                MainEditor.CurrentScene = SceneName;
            }
        }

        public static void AddGameObject(string ObjectName, string SceneName)
        {
            MakeFolder("GameData\\Scenes\\" + SceneName + "\\" + ObjectName);
            MakeFile("GameData\\Scenes\\"+SceneName+"\\"+ObjectName+"\\object.dat");
            //this will create a game object and add it into the scene object. This will allow for it to be displayed in the editor
            foreach(Scene scene in ObjectHandler.SceneData)
            {
                if(scene.id == SceneName)
                {
                    //these lines will assign an id to the object and reload the entityid table
                    scene.AddIDToEntity(SceneName+"\\"+ObjectName);
                    scene.LoadEntityIDTable();
                    foreach(KeyValuePair<int, string> entity in scene.EntityIDTable)
                    {
                        //splits apart the string and extracts the object name (stored in the form "scenename\\objectname")
                        if (entity.Value.Split("\\")[1] == ObjectName)
                        {
                            scene.GameObjects.Add(new Base_Classes.GameObject(SceneName, ObjectName, entity.Key));
                        }
                    }
                }
            }
        }
        public static void MakeFile(string FileName)
        {
            File.Create(SystemHandler.CurrentProjectDirectory + "\\" + FileName).Close();
            AddFileToProject(FileName);
            //Pauses the excecution until this file has been created
            while (!File.Exists(SystemHandler.CurrentProjectDirectory + "\\" + FileName)){ }
        }
        public static void MakeFolder(string Target)
        {
            //Makes a command line process
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            //Makes the Scene folder
            process.StandardInput.WriteLine("Mkdir \"" + SystemHandler.CurrentProjectDirectory +"\\"+ Target + "\"");
            process.StandardInput.WriteLine("EXIT");
            process.WaitForExit();
            process.Close();
        }
    }
}
