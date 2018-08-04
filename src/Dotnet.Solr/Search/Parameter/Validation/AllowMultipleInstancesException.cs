using System;

namespace Dotnet.Solr.Search.Parameter.Validation
{
    public class AllowMultipleInstancesException : Exception
    {
        public AllowMultipleInstancesException(string parameterType) :
            base(string.Format(Resource.AllowMultipleInstancesException, parameterType))
        {
        }
    }
}
