using Todoist.Controllers;
using Todoist.Model;
using Todoist.Views;

namespace Todoist
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(exitHandler);

            var modelConsole = new ModelConsole();
            var viewConsole = new ViewConsole();
            var controllerConsole = new ControllerConsole(modelConsole, viewConsole);
            modelConsole.GetGoalsAndCategories();

            while (true)
            {
                controllerConsole.StartApplication();
            }
        }

        private static void exitHandler(object sender, ConsoleCancelEventArgs args)
        {
            Environment.Exit(0);
        }
    }
}