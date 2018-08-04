using System.Collections.Generic;

namespace Dotnet.Solr.Search.Result
{
    public interface IFacetsResult<TDocument> : ISearchResult<TDocument>
        where TDocument : Document
    {
        /// <summary>
        /// Facet data
        /// </summary>
        IEnumerable<IFacetItem> Data { get; }
    }
}
