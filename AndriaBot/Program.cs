using DatabaseManager.Controllers;

namespace AndriaBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new Bot();
            ContextController.Initialize();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
