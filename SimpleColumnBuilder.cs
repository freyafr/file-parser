using System.Data;

namespace NsnApp
{
    public class SimpleColumnBuilder : IColumnBuilder
    {
        public DataTable BuildInputColumns(string headerLine, string[] testValue)
        {
            DataTable source = new DataTable();
            source.Columns.Add("advertiser",typeof(string));
            source.Columns.Add("spot_date",typeof(string));
            source.Columns.Add("ad_spend",typeof(double));
            source.Columns.Add("publisher",typeof(string));
            return source;
        }

        public void BuildOutputColumns(DataTable source, string columnsToGroup)
        {
            source.Columns.Add("advertiser",typeof(string)); 
            source.Columns.Add("ad_spend",typeof(double));  
            source.Columns.Add("publisher",typeof(string));    
        }
    }
}