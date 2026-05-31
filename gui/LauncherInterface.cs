using Spectre.Console;

class LauncherInterface
{
    Minecraft minecraft = new();
    string name;
    public void createUser()
    {
        name = AnsiConsole.Ask<string>("Digite seu [green]nome[/] de usuário: ");
        AnsiConsole.MarkupLine($"Usuário: [blue]{name}[/].");
    }
    public async Task selectVersions()
    {
        var choiceType = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Selecione o [green]tipo[/] da versão:")
            .AddChoices("release", "snapshot", "old_beta", "old_alpha", "all")
        );

        var versions = await AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .StartAsync("Buscando versões nos servidores da Mojang...", async ctx =>
            {
                return await minecraft.ListVersions(choiceType);
            });

        var versionsName = versions.Select(v => v.Name);

        var choiceVersion = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title("Selecione a [green]versão[/]:")
        .AddChoices(versionsName));

        bool willDownload;

        if (AnsiConsole.Confirm($"Deseja fazer o download da versão: {choiceVersion}?"))
        {
            willDownload = true;

            await AnsiConsole.Progress()
            .Columns(new ProgressColumn[]
            {
                new TaskDescriptionColumn(),
                new ProgressBarColumn(),
                new PercentageColumn(),
                new SpinnerColumn()
            })
            .StartAsync(async ctx =>
            {
                var downloadTask = ctx.AddTask("[green]Preparando para baixar...[/]");

                minecraft.launcher.FileProgressChanged += (sender, args) =>
                {
                    downloadTask.MaxValue = args.TotalTasks > 0 ? args.TotalTasks : 1;
                    downloadTask.Value = args.ProgressedTasks;

                    downloadTask.Description = $"[blue]Baixando:[/]{args.Name}";
                };

                await minecraft.OpenMinecraft(choiceVersion, name, willDownload);

                downloadTask.Value = downloadTask.MaxValue;
                downloadTask.Description = "[green]Download concluído com sucesso![/]";
            });
        }
        else
        {
            willDownload = false;

            await AnsiConsole.Status()
                .Spinner(Spinner.Known.Star)
                .SpinnerStyle(Style.Parse("green"))
                .StartAsync($"Configurando e iniciando a versão [blue]{choiceVersion}[/]...", async ctx =>
                {
                    await minecraft.OpenMinecraft(choiceVersion, name, willDownload);
                });
        }

        AnsiConsole.MarkupLine("\n[bold green]Minecraft aberto![/] Bom jogo!");

    }
}