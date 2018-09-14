using System;

namespace FileParser.ColumnTypeReolver
{
    public class StringColumnTypeResolver : ColumnTypeResolverBase
    {
        public StringColumnTypeResolver(ColumnTypeResolverBase next):base(next){}
        public override Type GetTypeFromValue(string value)
        {
            return typeof(string);
        }
    }
}