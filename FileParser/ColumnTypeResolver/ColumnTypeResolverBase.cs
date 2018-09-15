using System;

namespace FileParser.ColumnTypeReolver
{
    public abstract class ColumnTypeResolverBase
    {
        public ColumnTypeResolverBase Next {get;set;}

        public ColumnTypeResolverBase(){}
        public ColumnTypeResolverBase(ColumnTypeResolverBase next)
        {
            Next = next;
        }

        protected Type MoveNext(string valueToCheckType)
        {
            if (Next!=null)
            return Next.GetTypeFromValue(valueToCheckType);
            return typeof(string);
        }

        public abstract Type GetTypeFromValue(string value);
    }
}
