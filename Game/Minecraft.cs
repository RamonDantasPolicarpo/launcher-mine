using System.Diagnostics;
using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.ProcessBuilder;


internal class Minecraft
{
    private readonly MinecraftLauncher launcher = new();
    public async Task OpenMinecraft(string version, string playerName, bool willDownlaod)
    {
        Process? game;
        MLaunchOption options = new()
        {
            Session = MSession.CreateOfflineSession(playerName),
            MaximumRamMb = 8192
        };

        if (willDownlaod)
        {
            game = await launcher.InstallAndBuildProcessAsync(version, options);
        } else
        {
            game = await launcher.BuildProcessAsync(version, options);
        }

        game.Start();
    }
}