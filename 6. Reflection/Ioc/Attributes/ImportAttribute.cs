using System;

namespace Ioc.Attributes
{
    /// <summary>
    /// Attribute for import dependency for properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ImportAttribute : Attribute
    {
    }
}
