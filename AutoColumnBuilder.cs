using System.Data;
using NsnApp.ColumnTypeReolver;

namespace NsnApp
{
    public class AutoColumnBuilder : IColumnBuilder
    {
        private readonly ColumnTypeResolverBase _resolver;
        private readonly string _columnsToGroup;
        public AutoColumnBuilder(string columnsToGroup)
        {            
            _resolver = new NumericColumnTypeResolver(new StringColumnTypeResolver(null));
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

        public void BuildOutputColumns(DataTable source)
        {
            string[] columnNames = _columnsToGroup.Split("|");
            foreach(string columnName in columnNames)
                source.Columns.Add(columnName,typeof(string));              
        }
    }
}