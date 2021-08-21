using System.Diagnostics;

namespace LaunchCmd {
    interface ILaunch {
        Process launch(LaunchConfig searchPath,Config config);
    }
}
