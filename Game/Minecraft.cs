using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.ProcessBuilder;
using CmlLib.Core.VersionMetadata;


internal class Minecraft
{
    public readonly MinecraftLauncher launcher = new();
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
        }
        else
        {
            game = await launcher.BuildProcessAsync(version, options);
        }

        game.Start();
    }

    public async Task<IVersionMetadata[]> ListVersions(string type)
    {
        var versions = await launcher.GetAllVersionsAsync();
        IVersionMetadata[] finalList;
        if(type == "all")
        {
            return versions
                    .ToArray();
        }
        else
        {
            return versions
                    .Where(v => v.Type == type)
                    .ToArray();
        }
    }
}