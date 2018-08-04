using Dotnet.Solr.Search.Query;

namespace Dotnet.Solr.Search.Parameter
{
    /// <summary>
    /// Query parameter
    /// </summary>
    public interface IQueryParameter<TDocument> : ISearchParameter
        where TDocument : Document
    {
        /// <summary>
        /// Parameter to include in query
        /// </summary>
        SearchQuery<TDocument> Value { get; set; }
    }
}
