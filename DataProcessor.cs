using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace NsnApp
{
    public class DataProcessor{

        private string _inputFileName;
        private string _outputFileName;
        private IFileReader _reader;
        private string _groupCriteria;
        private IColumnBuilder _columnBuilder;
        private IFileWriter _writer;
        public DataProcessor(string inputFileName,string outputFileName,IFileReader reader, string groupCriteria,
        IColumnBuilder columnBuilder, IFileWriter writer)
        {
            _inputFileName = inputFileName;
            _outputFileName = outputFileName;
            _reader = reader;
            _groupCriteria = groupCriteria;
            _columnBuilder = columnBuilder;
            _writer = writer;
            
        }
        public void ProcessFile()
        {            
            DataTable outputTable = new DataTable();
            if (File.Exists(_inputFileName))
                using(var inputFile = new FileStream(_inputFileName, FileMode.OpenOrCreate))
                { 
                    using(var reader = new StreamReader(inputFile))
                    {
                        string headerLine = reader.ReadLine();
                        string[] values = _reader.ParseString(reader.ReadLine());
                        DataTable table = _columnBuilder.BuildInputColumns(headerLine,values); 
                        _columnBuilder.BuildOutputColumns(outputTable,_groupCriteria);    
                        if (values.Length>0&&values.Any(v=>!string.IsNullOrEmpty(v)))
                        {
                            //temporary copy/paste
                            /// TODO: remove later
                            var resultRows = new List<DataRow>();
                            DataRow row = table.NewRow();
                            resultRows.Add(row);
                            for (int i = 0; i < values.Length; i++)
                            {
                                if(row.Table.Columns[i].DataType==typeof(double))
                                {
                                    row[i]=values[i].Replace(".",",");
                                }
                                else
                                    row[i]=values[i];
                            }
                            ICollection<DataRow> rowsToWrite = FormOutputRow(resultRows,outputTable);
                            //Write agregated Data to TempFile or 
                            _writer.PrepareOutputForSaving(rowsToWrite);
                        }                    
                        
                        while(!reader.EndOfStream)
                        {
                            //read line one by one or buffered
                            ICollection<DataRow> rowsRead = _reader.ReadDataBuffered(table,reader);
                            //group by critea to reduce number of records
                            ICollection<DataRow> rowsToWrite = FormOutputRow(rowsRead,outputTable);
                            //Write agregated Data to TempFile or 
                            _writer.PrepareOutputForSaving(rowsToWrite);
                            
                        }  
                    } 
                    inputFile.Close();
                    _writer.WriteOutputFile(_outputFileName);               
                }
            else
            {
                Debug.Fail("input file not found. Please check your setup");
            }
        }

        private ICollection<DataRow> FormOutputRow(ICollection<DataRow> inputRows,DataTable outputTable)
        {
            var resultRows = new List<DataRow>();
            foreach (DataRow row in inputRows)
            {
                DataRow outputRow = outputTable.NewRow();
                resultRows.Add(outputRow);
                for (int i = 0; i < outputTable.Columns.Count; i++)
                {
                    outputRow[i]= row[outputTable.Columns[i].ColumnName];
                }
            }
            return resultRows;
        }
    }
}