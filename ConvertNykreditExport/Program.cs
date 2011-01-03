using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConvertNykreditExport
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFilename = args[0];
            var outputFilename = args[1];

            Console.WriteLine("Converting " + inputFilename + " to " + outputFilename);

            using (var sr = new StreamReader(inputFilename, Encoding.Default))
            {
                using (var sw = new StreamWriter(outputFilename, false, Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.StartsWith("Data hentet")) continue; // The last line that displays the data collection datetime

                        var fields = line.Split(';');
                        var date = fields[0].Replace('-', '/');
                        var text = fields[1];
                        var amount = fields[2].Replace(",", "");
                        var controlled = fields[3];
                        var balance = fields[4].Replace(",", "");
                        var bankdate = fields[5];

                        sw.WriteLine(date + "," + amount + "," + "\"" + text + "\"");
                    }
                }
            }
        }
    }
}