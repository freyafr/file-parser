using System;
using System.Globalization;

namespace FileParser.ColumnTypeReolver
{
    public class NumericColumnTypeResolver : ColumnTypeResolverBase
    {
        public NumericColumnTypeResolver(ColumnTypeResolverBase next):base(next){}

        public NumericColumnTypeResolver():base(){}
        public override Type GetTypeFromValue(string valueToCheckType)
        {
            double result;
            if(Double.TryParse(valueToCheckType, NumberStyles.Any,CultureInfo.InvariantCulture, out result))
            {
                return typeof(double);
            }
            return MoveNext(valueToCheckType);
        }
    }
}