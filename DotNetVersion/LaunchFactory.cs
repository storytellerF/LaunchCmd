using LaunchCmd.launch;

namespace LaunchCmd {
    class LaunchFactory {
        public static ILaunch Get(LaunchConfig launchConfig) {
            switch (launchConfig.Type) {
                case "cmd":
                    return new CMDLaunch();
                case "ex":
                    return new ExploreLaunch();
                default:
                    return new RegularLaunch(); ;
            }
        }
    }
}
