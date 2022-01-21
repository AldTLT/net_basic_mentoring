using System;

namespace Ioc.Attributes
{
    /// <summary>
    /// Attribute for export dependency
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class ExportAttribute : Attribute
    {
        public Type Type { get; }
        
        public ExportAttribute()
        {

        }

        public ExportAttribute(Type type)
        {
            Type = type;
        }
    }
}
