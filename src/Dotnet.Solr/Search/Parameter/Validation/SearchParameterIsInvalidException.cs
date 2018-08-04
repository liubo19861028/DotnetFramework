using System;

namespace Dotnet.Solr.Search.Parameter.Validation
{
    public class SearchParameterIsInvalidException : Exception
    {
        public SearchParameterIsInvalidException(string parameterType, string errorMessage) :
            base(string.Format(Resource.SearchParameterIsInvalidException, parameterType, errorMessage))
        {
        }
    }
}
