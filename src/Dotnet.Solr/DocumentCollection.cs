using Dotnet.Solr.Search;
using Dotnet.Solr.Update;
using Dotnet.Solr.Utility;

namespace Dotnet.Solr
{
    /// <summary>
    /// SOLR document collection
    /// </summary>
    public class DocumentCollection<TDocument>
        where TDocument : Document
    {
        private readonly ISolrExpressServiceProvider<TDocument> _serviceProvider;

        public DocumentCollection(ISolrExpressServiceProvider<TDocument> serviceProvider)
        {
            Checker.IsNull(serviceProvider);

            this._serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Document search provider
        /// </summary>
        public DocumentSearch<TDocument> Select() => this._serviceProvider.GetService<DocumentSearch<TDocument>>();

        /// <summary>
        /// Document update provider
        /// </summary>
        public DocumentUpdate<TDocument> Update() => this._serviceProvider.GetService<DocumentUpdate<TDocument>>();
    }
}
