using Spectre.Console;

class Program
{

    public static async Task Main()
    {
        var gui = new LauncherInterface();

        AnsiConsole.MarkupLine("[bold]Bem vindo ao Launcher de [/][green bold]Minecraft[/]\n");
        // Criar usuário
        gui.createUser();
        // Inicia o processo
        await gui.selectVersions();
    }
}