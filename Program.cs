using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace NsnApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var processor = new DataProcessor("input.csv","output.csv",
                "advertiser|ad_spend|publisher",
                new AutoColumnBuilder(),
                new SimpleFileWriter(new DataGroupingByAdv()),
                new TextParser());

            processor.ProcessFile();
            Console.WriteLine("It is done!");            
        }
    }
}
