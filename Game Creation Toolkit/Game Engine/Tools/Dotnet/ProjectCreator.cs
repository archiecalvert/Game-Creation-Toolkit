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
            //https://www.youtube.com/watch?v=HeHR9q-IWF8
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
            SystemHandlers.CurrentProjectDirectory = Directory + "\\" + Name + "\\" + Name; //stores the current directory that the project is in
            StructureProject(Name, Directory,process); //creates all the necessary files used by my program
        }

        static void StructureProject(string Name, string Directory, Process process)
        {
            List<string> commands = new List<string>();
            commands.Add("cd \"" + Name + "\"");
            commands.Add("Mkdir editor");
            commands.Add("cd editor");
            commands.Add("Mkdir data classes"); //creates the files that the program will use
            foreach(string command in commands)
            {
                process.StandardInput.WriteLine(command);
            }
            process.Close();
            while (!System.IO.Directory.Exists(Directory + "\\" + Name + "\\" + Name + "\\editor\\data")){ } //line makes the program wait until the whole monogame file has been created
            //File.Create(Directory + "\\" + Name + "\\" + Name + "\\editor\\data\\window.dat").Close();
            //ProjectFileManager.AddFileToProject(SystemHandlers.CurrentProjectDirectory +"\\"+ Name + ".csproj", "editor\\data\\window.dat");
            MakeFile(Directory, Name, "editor\\data\\window.dat");
            using(StreamWriter sw = new StreamWriter(Directory+"\\"+Name+"\\"+Name+"\\editor\\data\\window.dat"))
            {
                sw.WriteLine("Window Color: 255,255,255");
                sw.Close();
            }
            MakeFile(Directory, Name, "editor\\data\\objects.dat");
        }
        static void MakeFile(string Directory, string Name, string Target)
        {
            File.Create(Directory + "\\" + Name + "\\" + Name + "\\"+Target).Close();
            ProjectFileManager.AddFileToProject(SystemHandlers.CurrentProjectDirectory + "\\" + Name + ".csproj", Target);
        }
    }
}





/*
             commands.Add("cd\\"); //Goes back to the root directory
            commands.Add("cd " + Directory); //Goes to the entered directory
            commands.Add("Mkdir \"" + Name + "\""); //Creates a file in the directory under the entered name
            commands.Add("cd \"" + Name + "\""); //Changes the directory to the file that just got created
            commands.Add("dotnet new sln"); //makes a Visual Studio solution in the directory
            commands.Add("dotnet new install Monogame.Templates.CSharp"); //Installs the monogame templates if they aren't found
            commands.Add("dotnet new mgdesktopgl -o \"" + Name + "\""); //creates a new monogame project
            commands.Add("dotnet sln add \""+Name+"/" + Name + ".csproj\"");
*/
