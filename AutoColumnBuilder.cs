using System.Data;
using NsnApp.ColumnTypeReolver;

namespace NsnApp
{
    public class AutoColumnBuilder : IColumnBuilder
    {
        private ColumnTypeResolverBase _resolver;
        public AutoColumnBuilder()
        {            
            _resolver = new NumericColumnTypeResolver(new StringColumnTypeResolver(null));
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

        public void BuildOutputColumns(DataTable source, string columnsToGroup)
        {
            string[] columnNames = columnsToGroup.Split("|");
            foreach(string columnName in columnNames)
                source.Columns.Add(columnName,typeof(string));              
        }
    }
}