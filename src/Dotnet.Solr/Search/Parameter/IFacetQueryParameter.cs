using Dotnet.Solr.Search.Query;

namespace Dotnet.Solr.Search.Parameter
{
    /// <summary>
    /// Facet query parameter
    /// </summary>
    public interface IFacetQueryParameter<TDocument> : IFacetParameter<TDocument>
        where TDocument : Document
    {
        /// <summary>
        /// Name of alias added in query
        /// </summary>
        string AliasName { get; set; }

        /// <summary>
        /// Query used to make facet
        /// </summary>
        SearchQuery<TDocument> Query { get; set; }
    }
}
