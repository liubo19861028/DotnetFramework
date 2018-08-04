using System;

namespace Dotnet.Solr.Search.Parameter.Validation
{
    /// <summary>
    /// Indicates possibility to use multiple instance of parameter
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AllowMultipleInstancesAttribute : Attribute
    {
    }
}
