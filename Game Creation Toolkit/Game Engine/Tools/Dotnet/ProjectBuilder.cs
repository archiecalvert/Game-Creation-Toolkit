using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Creation_Toolkit.Game_Engine.Tools.Dotnet
{
    public class ProjectBuilder
    {
        static public void Begin(string Directory)
        {
            string[] commands = new string[4];
            commands[0] = "cd\\"; //Changes the directory to the root directory
            commands[1] = "cd \"" + Directory + "\""; //Changes the directory to the parameter entered
            commands[2] = "dotnet publish --sc true -p:PublishReadyToRun=true -p:PublishSingleFile=true -p:PublishTrimmed=true -a x86"; //Command to then generate an exe
            commands[3] = "EXIT";//Exits the command line
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = false; //switch to remove the console window
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            foreach (string command in commands)//loop to run all the commands
            {
                process.StandardInput.WriteLine(command);
                
            }
            process.WaitForExit();
            process.Close();
        }
        
    }
}
