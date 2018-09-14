using System.Data;
using System.Globalization;

namespace FileParser
{
    public class DataGroupingByAdv : IDataGrouping
    {
        public virtual bool ProperStringFound(DataRow source, DataRow dest)
        {
            if (!source.Table.Columns.Contains("advertiser")||!dest.Table.Columns.Contains("advertiser"))
                return false;
            return source["advertiser"].ToString()==dest["advertiser"].ToString();
        }

        public virtual void GroupFields(DataRow source, DataRow dest)
        {
            if (source.Table.Columns.Contains("ad_spend")||dest.Table.Columns.Contains("ad_spend"))
                
            source["ad_spend"] = double.Parse(source["ad_spend"].ToString(),CultureInfo.CurrentCulture)+
                        double.Parse(dest["ad_spend"].ToString(),CultureInfo.CurrentCulture);
        }
    }

    public class DataGroupingByAdvDate : DataGroupingByAdv
    {
        public override bool ProperStringFound(DataRow source, DataRow dest)
        {
            return source["advertiser"].ToString()==dest["advertiser"].ToString()&&
            source["spot_date"].ToString()==dest["spot_date"].ToString();
        }        
    }
}