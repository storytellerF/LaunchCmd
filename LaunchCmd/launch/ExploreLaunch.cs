using System.Diagnostics;

namespace LaunchCmd {
    class ExploreLaunch : ILaunch {
        public Process launch(LaunchConfig searchPath,Config config) {
            string path = searchPath.Path;
            Process p = new Process {
                StartInfo =
                {
                    FileName = "Explorer.exe",
                    Arguments = "/select," + path
                }
            };
            return p;
        }
    }
}
