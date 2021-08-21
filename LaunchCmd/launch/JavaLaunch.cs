using System;
using System.Diagnostics;

namespace LaunchCmd {
    class JavaLaunch : ILaunch {
        public Process launch(LaunchConfig searchPath, Config config) {

            string path = searchPath.Path;
            string arguments = " -jar \"" + path + "\"";
            Console.WriteLine(arguments);
            Process p = new Process {
                StartInfo =
                {
                            FileName = config.javaHome,
                            Arguments = arguments,
                            UseShellExecute = false,
                            RedirectStandardInput = true,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        }
            };
            return p;
        }
    }
}
