using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace NsnApp
{  

    public class SimpleFileWriter : IFileWriter
    {
        private IDataGrouping _grouper;
        protected ICollection<DataRow> _outputRows = new List<DataRow>();

        public SimpleFileWriter(IDataGrouping grouper)
        {
            _grouper = grouper;
        }
        public void PrepareOutputForSaving(ICollection<DataRow> outputRowsNotGrouped)
        {
            foreach(DataRow row in outputRowsNotGrouped)
            {
                var rowFound = _outputRows.Where(r=>_grouper.ProperStringFound(r,row)).FirstOrDefault();
                if (rowFound!=null)
                {
                    _grouper.GroupFields(rowFound,row);
                }
                else
                {
                    _outputRows.Add(row);
                }
            }
        }

        public void WriteOutputFile(string outputFile)
        {
            if(_outputRows.Count>0)
            using(var writer = new StreamWriter(outputFile,false,Encoding.UTF8))
            {
                var headerstring = string.Join(',',_outputRows.First().Table.Columns.Cast<DataColumn>().ToArray().Select(s=>s.ColumnName));
                        writer.WriteLine(headerstring);
                        writer.Flush();

                foreach (DataRow row in _outputRows)
                {
                    var outputRow = string.Join(",", row.ItemArray.Select(item=>string.Format("\"{0}\"",item)));
                    writer.WriteLine(outputRow);
                    writer.Flush();
                }
            }
        }
    }
}