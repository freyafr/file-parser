using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace FileParser
{
    public class DataProcessor{

        private readonly string _inputFileName;  
        private readonly IColumnBuilder _columnBuilder;
        private readonly IFileWriter _writer;
        private readonly ITextParser _textParser;
        public DataProcessor(string inputFileName,
        IColumnBuilder columnBuilder, IFileWriter writer,ITextParser textParser)
        {
            _inputFileName = inputFileName; 
            _columnBuilder = columnBuilder;
            _writer = writer;
            _textParser = textParser;            
        }
        public void ProcessFile()
        {   
            if (File.Exists(_inputFileName))
            {     
                using(var reader = new StreamReader(_inputFileName, Encoding.UTF8))
                {
                    string headerLine = reader.ReadLine();
                    bool firstValueFound = false;
                    DataTable table = new DataTable();
                    DataTable outputTable = new DataTable();                 
                        
                    while(!reader.EndOfStream)
                    {                            
                        string[] values = _textParser.ParseFileLine(reader.ReadLine());
                        if (values.Any(v=>!string.IsNullOrEmpty(v)))
                        {
                            if (!firstValueFound)
                            {
                                _columnBuilder.BuildInputColumns(table,headerLine,values); 

                                _columnBuilder.BuildOutputColumns(outputTable); 
                                firstValueFound =true; 
                            }
                            DataRow resultRow = _textParser.FillDataFromString(table.NewRow(),values);

                            ICollection<DataRow> rowsToWrite = FormOutputRow(resultRow,outputTable);
                            
                            _writer.PrepareOutputForSaving(rowsToWrite);
                        }                           
                    }  
                } 
                    
                _writer.WriteOutputFile();  
            }   
            else
            {
                Debug.Fail("input file not found. Please check your setup");
            }
        }

        private ICollection<DataRow> FormOutputRow(DataRow inputRow,DataTable outputTable)
        {
            var resultRows = new List<DataRow>();
            DataRow outputRow = outputTable.NewRow();
                resultRows.Add(outputRow);
                for (int i = 0; i < outputTable.Columns.Count; i++)
                {
                    if (inputRow.Table.Columns.Contains(outputTable.Columns[i].ColumnName))
                        outputRow[i]= inputRow[outputTable.Columns[i].ColumnName];
                }
            
            return resultRows;
        }
    }
}