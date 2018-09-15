using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace FileParser
{  

    public class SimpleFileWriter : IFileWriter
    {
        private readonly IDataGrouping _grouper;
        

        private readonly string _outputFile;

        public ICollection<DataRow> OutputRows{get;private set;} = new List<DataRow>();

        public SimpleFileWriter(IDataGrouping grouper,string outputFile)
        {
            _grouper = grouper;
            _outputFile = outputFile;
        }
        public void PrepareOutputForSaving(ICollection<DataRow> outputRowsNotGrouped)
        {
            foreach(DataRow row in outputRowsNotGrouped)
            {
                var rowFound = OutputRows.Where(r=>_grouper.ProperStringFound(r,row)).FirstOrDefault();
                if (rowFound!=null)
                {
                    _grouper.GroupFields(rowFound,row);
                }
                else
                {
                    OutputRows.Add(row);
                }
            }
        }

        public void WriteOutputFile()
        {
            if(OutputRows.Count>0)
            using(var writer = new StreamWriter(_outputFile,false,Encoding.UTF8))
            {
                var headerstring = string.Join(",",OutputRows.First().Table.Columns.Cast<DataColumn>().ToArray().Select(s=>s.ColumnName));
                        writer.WriteLine(headerstring);
                        writer.Flush();

                foreach (DataRow row in OutputRows)
                {
                    var outputRow = string.Join(",", row.ItemArray.Select(item=>string.Format("\"{0}\"",
                                                    item is double?((double)item).ToString(CultureInfo.InvariantCulture) :item.ToString())));
                    writer.WriteLine(outputRow);
                    writer.Flush();
                }
            }
        }
    }
}