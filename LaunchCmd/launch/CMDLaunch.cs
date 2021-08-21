using System.Diagnostics;

namespace LaunchCmd {
    /// <summary>
    /// 通过cmd /c 命令启动
    /// </summary>
    class CMDLaunch : ILaunch {
        public Process launch(LaunchConfig searchPath,Config config) {
            string filePath = searchPath.Path;
            Process process = new Process {
                StartInfo =
                {
                    FileName = "cmd.exe",
                    Arguments = "/c \""+filePath+"\"",
                    UseShellExecute = true,
                    CreateNoWindow = false,
                    WorkingDirectory=searchPath.Working
                }

            };
            return process;
        }
    }
}
