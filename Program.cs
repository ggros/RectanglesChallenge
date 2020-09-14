using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace intersecting_rectangles
{
    class Program
    {
        private static void PrintHelp()
        {
            Console.WriteLine("Usage: intersect.exe <a.json>");
        }
        private static bool CheckArgsAndPrintHelp(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Invalid number of arguments");
                PrintHelp();
                return false;
            }
            var fileName = args[0];
            if (!File.Exists(fileName))
            {
                Console.WriteLine("File not found");
                PrintHelp();
                return false;
            }
            return true;
        }

        static int Main(string[] args)
        {
            if (!CheckArgsAndPrintHelp(args)) return -1;
            var fileName = args[0];

            var jsonString = File.ReadAllText(fileName);
            //var obj = JsonConvert.DeserializeObject(jsonString);

            RectanglesDTO data;

            using (var sr = new StreamReader(fileName))
            {
                var reader = new RectanglesFileReader(sr);
                data = reader.ReadContent();
            }
            var calculator = new RectanglesInsectionsCalculator(data.rects);
            calculator.PrintInput();
            calculator.TestCollision();


            //Console.WriteLine("Hello World!");
            return 0;
        }
    }
}
