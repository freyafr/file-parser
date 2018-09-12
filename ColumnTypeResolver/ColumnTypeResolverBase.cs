using System;

namespace NsnApp.ColumnTypeReolver
{
    public abstract class ColumnTypeResolverBase
    {
        private ColumnTypeResolverBase _next;
        public ColumnTypeResolverBase(ColumnTypeResolverBase next)
        {
            _next = next;
        }

        protected Type MoveNext(string valueToCheckType)
        {
            if (_next!=null)
            return _next.GetTypeFromValue(valueToCheckType);
            return typeof(string);
        }

        public abstract Type GetTypeFromValue(string value);
    }
}
