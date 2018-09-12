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
                new SingleStringReader(),"advertiser|publisher",
                new AutoColumnBuilder(),
                new SimpleFileWriter());

            processor.ProcessFile();
            Console.WriteLine("It is done!");





            /*Dictionary<string,string> inputRow = new Dictionary<string,string> ();

            using(var inputFile = new FileStream("input.csv", FileMode.Open))
            { 
                DataTable table = new DataTable();  
                
                DataTable otable = new DataTable();  
                otable.Columns.Add("advertiser",typeof(string)); 
                otable.Columns.Add("ad_spend",typeof(double));  
                otable.Columns.Add("publisher",typeof(string));    
                int lineNum=0;                           
                using(var reader = new StreamReader(inputFile))
                {
                    
                    while(!reader.EndOfStream)
                    {
                        string[] dataRowStringArray =  Regex.Matches(reader.ReadLine(),"(\"(.*?)\"(,|$))").Select(s=>s.Groups[2].Value).ToArray();
                        if (lineNum>0)
                        {
                            inputRow["advertiser"]=dataRowStringArray[0];
                           // inputRow["spot_date"]=dataRowStringArray[1];
                            inputRow["ad_spend"]=dataRowStringArray[1];
                            inputRow["publisher"]=dataRowStringArray[2];
                            Console.WriteLine(string.Join("||   ",dataRowStringArray));
                        }
                        lineNum++;

                        
                        /*if (table.Columns.Count == 0)
                        {
                            foreach (string colName in dataRowStringArray)
                            {
                                if (colName=="ad_spend")
                                 table.Columns.Add(colName,typeof(double));
                                else
                                    table.Columns.Add(colName);
                            }
                        }
                        else
                        {
                            for(int i=0;i<dataRowStringArray.Length;i++)
                                dataRowStringArray[i]=dataRowStringArray[i].Replace("\"","").Replace(".",",");
                            
                            
                            var row = table.LoadDataRow(dataRowStringArray,false);
                            Console.WriteLine(string.Join("||   ",row.ItemArray));
                            using(var outputFile = new FileStream("output.csv", FileMode.OpenOrCreate))
                            {
                                bool lineFound = false;
                                using(var outputReader = new StreamReader(outputFile))
                                {
                                    while(!outputReader.EndOfStream)
                                    {
                                        string[] dataRowStringArrayOutput = Regex.Split(outputReader.ReadLine(),",");
                                        otable
                                    }
                                }
                            }

                            if(table.Rows.Count>200)
                                table.Rows.Clear();
                            
                        }
                        
                    }
                    
                }
                
                System.Console.WriteLine("Rows Read:{0}",lineNum);
            }*/
        }
    }
}
