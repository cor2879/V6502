using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emulators.Nes.Binary;

namespace RomParseConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            NesRomParser rom = new NesRomParser(args[0]);

            Console.Clear();

            for (int i = 0; i < rom.Count;)
            {
                Console.Write("0x{0:X8}", i);

                int limit = i + 16;
                for (; i < limit; i++)
                {
                    Console.Write(" {0:X2}", rom[i]);
                }

                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
