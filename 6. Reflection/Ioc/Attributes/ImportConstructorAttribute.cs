using System;

namespace Ioc.Attributes
{
    /// <summary>
    /// Attribute for import class
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ImportConstructorAttribute : Attribute
    {
    }
}
