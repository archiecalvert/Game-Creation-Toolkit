using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Tools.Dotnet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Tools.NewProject
{
    public class ProjectCreator
    {
        public ProjectCreator(string Name, string Directory)
        {
            //Code below is .bat Commands
            List<string> commands = new List<string>();
            commands.Add("cd\\"); //Goes back to the root directory
            commands.Add("cd \"" + Directory + "\""); //Goes to the entered directory
            commands.Add("Mkdir \"" + Name + "\""); //Creates a file in the directory under the entered name
            commands.Add("cd \"" + Name + "\""); //Changes the directory to the file that just got created
            commands.Add("dotnet new sln"); //makes a Visual Studio solution in the directory
            commands.Add("dotnet new install Monogame.Templates.CSharp"); //Installs the monogame templates if they aren't found
            commands.Add("dotnet new mgdesktopgl -o \"" + Name + "\""); //creates a new monogame project
            commands.Add("dotnet sln add \"" + Name + "/" + Name + ".csproj\"");
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = false;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            foreach(string command in commands)
            {
                process.StandardInput.WriteLine(command); //runs each command
            }
            SystemHandler.CurrentProjectDirectory = Directory + "\\" + Name + "\\" + Name; //stores the current directory that the project is in
            SystemHandler.ProjectName = Name;
            CreateEditorFiles(process); //creates all the necessary files used by my program
            GenerateClasses();
            AddPackages();
        }
        static void CreateEditorFiles(Process process)
        {
            string Name = SystemHandler.ProjectName;
            string Directory = SystemHandler.CurrentProjectDirectory;
            List<string> commands = new List<string>();
            process.StandardInput.WriteLine("cd \"" + Name + "\"");
            process.StandardInput.WriteLine("Mkdir GameData"); //makes the GameData folder
            process.StandardInput.WriteLine("cd GameData");
            process.StandardInput.WriteLine("Mkdir Scenes"); //makes the Scenes folder
            process.Close();
            
            while(!System.IO.Directory.Exists(SystemHandler.CurrentProjectDirectory + "\\GameData\\Scenes")) { }
        }
        static void AddPackages()
        {
            Process process = new Process();
            process.StartInfo.FileName = "powershell.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.EnableRaisingEvents = true;
            process.Start();     
            process.StandardInput.WriteLine("cd\\");
            process.StandardInput.WriteLine("cd \"" + SystemHandler.CurrentProjectDirectory + "\"");
            process.StandardInput.WriteLine("dotnet add package Newtonsoft.Json --version 13.0.3");
            process.StandardInput.WriteLine("EXIT");
            process.WaitForExit();
            process.Close();
            
        }
        static void GenerateClasses()
        {
            //this procedure creates all of the code for the users "Game1" class in their project so that textures and funtionality can be added
            string directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Game Engine\\ProjectTemplateData\\Game1.txt";            
            using (StreamWriter sw = new StreamWriter(SystemHandler.CurrentProjectDirectory + "\\Game1.cs"))
            {
                foreach (string line in File.ReadLines(directory))
                {
                    //REPLACETHISNAMESPACE is a placeholder and will be replaced by the users project name
                    if (line.Contains("REPLACETHISNAMESPACE"))
                    {
                        string newNameSpace = SystemHandler.ProjectName.Replace(" ", "_");
                        string NewLine = line.Replace("REPLACETHISNAMESPACE", newNameSpace);
                        sw.WriteLine(NewLine);
                    }
                    //REPLACEPROJECTDIRECTORY is a placeholder for the users working directory
                    else if (line.Contains("REPLACEPROJECTDIRECTORY"))
                    {
                        string NewLine = "";
                        foreach (char  c in line.Replace("REPLACEPROJECTDIRECTORY", "\"" + SystemHandler.CurrentProjectDirectory + "\""))
                        {
                            //Turns a single \ character into \\
                            if (c == '\\')
                            {
                                NewLine += "\\\\";
                            }
                            else
                            {
                                NewLine += c;
                            }
                        }
                        sw.WriteLine(NewLine);
                    }
                    else sw.WriteLine(line);

                }
                sw.Close();
            }
            
        }
    }
}
