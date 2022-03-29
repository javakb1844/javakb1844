using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Consoles
{
    public class ConsoleCSS
    {
        static readonly Random _Random = new Random();
        public static ConsoleColor GetRandColor()
        {
            ConsoleColor cc = ConsoleColor
            .GetValues(typeof(ConsoleColor))
            .Cast<ConsoleColor>()
            .OrderBy(x => _Random.Next())
            .FirstOrDefault();

            if (cc != ConsoleColor.Black && cc != ConsoleColor.DarkBlue && cc != ConsoleColor.DarkGreen && cc != ConsoleColor.DarkCyan && cc != ConsoleColor.Gray)
            { return cc; }
            else
            { return GetRandColor(); }

        }
    }
}
