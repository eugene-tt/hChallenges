using NConsoleMenu;
using System;

namespace Challenge2.ViewControllers
{
    public class BaseViewController
    {
        public static void ExecuteSubMenu(CMenu menu)
        {
            menu.Run();
        }

        public static void BeginOutput()
        {
            Console.WriteLine("_______________________________________________________");
        }

        public static void EndOutput()
        {
            Console.WriteLine("_______________________________________________________");
            Console.WriteLine();
        }

        public static void PrintHeader(string header)
        {
            Console.WriteLine($"{header}:");
        }

        public static void PrintLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}
