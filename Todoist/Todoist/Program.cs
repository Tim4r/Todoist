using Todoist.BL;
using Todoist.Controllers;
using Todoist.Core.Consts;
using Todoist.Views;

namespace Todoist;

internal static class Program
{
    static async Task Main(string[] args)
    {
        Console.CancelKeyPress += new ConsoleCancelEventHandler(ExitHandler);

        var viewConsole = new ViewConsole();
        var controllerConsole = new ControllerConsole(new ViewConsole(), new BusinessLogic());

        while (true)
        {
            await StartApplication(viewConsole, controllerConsole);
        }
    }

    internal static async Task StartApplication(ViewConsole viewConsole, ControllerConsole controllerConsole)
    {
        string choice;
        viewConsole.Display(AppConsts.Common.Menu.Start + AppConsts.Common.Menu.StartItemSelectable);
        choice = controllerConsole.CheckValidate(viewConsole.GetInput(), AppConsts.Common.NumberOf.StartItems);
        await controllerConsole.DisplayStartMenu(choice);
    }

    private static void ExitHandler(object sender, ConsoleCancelEventArgs args)
    {
        Environment.Exit(0);
    }
}