using CmlLib;
using CmlLib.Core;
class Program
{
    public static async Task Main()
    {
        var launcher = new MinecraftLauncher();

        //Lista todas as versões do mine
        var versions = await launcher.GetAllVersionsAsync();
        foreach(var version in versions)
        {
            Console.WriteLine(version);
        }
    }
}