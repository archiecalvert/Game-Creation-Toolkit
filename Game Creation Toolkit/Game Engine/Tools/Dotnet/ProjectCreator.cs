using Game_Creation_Toolkit.Game_Engine.Handlers;
using Game_Creation_Toolkit.Game_Engine.Tools.Dotnet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            foreach(string command in commands)
            {
                process.StandardInput.WriteLine(command); //runs each command
            }
            SystemHandler.CurrentProjectDirectory = Directory + "\\" + Name + "\\" + Name; //stores the current directory that the project is in
            SystemHandler.ProjectName = Name;
            CreateEditorFiles(process); //creates all the necessary files used by my program
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
    }
}
