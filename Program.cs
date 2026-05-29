using CmlLib;
using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.ProcessBuilder;
class Program
{
    public static async Task Main()
    {
        var launcher = new MinecraftLauncher();

        var game = await launcher.InstallAndBuildProcessAsync(
            "1.8.9", 
            new MLaunchOption
            {
                Session = MSession.CreateOfflineSession("Ramon30"),
                MaximumRamMb = 4096
            }
        );
        game.Start();

        //Lista todas as versões do mine
        var versions = await launcher.GetAllVersionsAsync();
        foreach (var version in versions)
        {
            Console.WriteLine(version);
        }
    }
}