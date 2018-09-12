using System.Data;

namespace NsnApp
{
    public class SimpleColumnBuilder : IColumnBuilder
    {
        public void BuildInputColumns(DataTable source,string headerLine, string[] testValue)
        {
            source.Columns.Add("advertiser",typeof(string));
            source.Columns.Add("spot_date",typeof(string));
            source.Columns.Add("ad_spend",typeof(double));
            source.Columns.Add("publisher",typeof(string));            
        }

        public void BuildOutputColumns(DataTable source, string columnsToGroup)
        {
            source.Columns.Add("advertiser",typeof(string)); 
            source.Columns.Add("ad_spend",typeof(double));  
            source.Columns.Add("publisher",typeof(string));    
        }
    }
}