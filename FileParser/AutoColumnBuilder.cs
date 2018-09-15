using System.Data;
using FileParser.ColumnTypeReolver;

namespace FileParser
{
    public class AutoColumnBuilder : IColumnBuilder
    {
        private readonly ColumnTypeResolverBase _resolver;
        private readonly string _columnsToGroup;
        public AutoColumnBuilder(string columnsToGroup)
        {            
            _resolver = new NumericColumnTypeResolver(new StringColumnTypeResolver());
            _columnsToGroup = columnsToGroup;
        }

        public void BuildInputColumns(DataTable source,string headerLine, string[] testValues)
        {            
            var headers = headerLine.Split(',');
            for (int i = 0; i < headers.Length; i++)            
            {
                if(i>=testValues.Length||string.IsNullOrEmpty(testValues[i]))
                    source.Columns.Add(headers[i],typeof(string));
                else
                    source.Columns.Add(headers[i],_resolver.GetTypeFromValue(testValues[i]));
            }
        }

        public void BuildOutputColumns(DataTable source,DataTable inputTable)
        {
            string[] columnNames = _columnsToGroup.Split('|');
            foreach(string columnName in columnNames)
            {
                if (inputTable.Columns.Contains(columnName))
                    source.Columns.Add(columnName,inputTable.Columns[columnName].DataType);   
                else
                    source.Columns.Add(columnName,typeof(string));      
            }        
        }
    }
}