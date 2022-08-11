using System.Diagnostics;

Process compiler = new Process();

compiler.StartInfo.FileName = @"C:\Users\Hadi Cheaito\Downloads\OnnxModelsExecutor\OnnxModelsExecutor\OnnxModelsExecutor.exe";
compiler.StartInfo.Arguments = "\"C:\\Users\\Hadi Cheaito\\OneDrive\\Pictures\\Saved Pictures\\redcar.jpg\"";
compiler.StartInfo.UseShellExecute = false;
compiler.StartInfo.RedirectStandardOutput = true;
compiler.Start();

var read = compiler.StandardOutput.ReadToEnd();

compiler.WaitForExit();
