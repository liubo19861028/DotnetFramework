using Dotnet.Solr.Builder;
using Dotnet.Solr.Options;
using Dotnet.Solr.Search;
using Dotnet.Solr.Search.Behaviour;
using Dotnet.Solr.Search.Query;
using Dotnet.Solr.Search.Result;
using Dotnet.Solr.Update;

namespace Dotnet.Solr.Utility
{
    /// <summary>
    /// Helper class used to add core services in DI engine
    /// </summary>
    internal static class CoreDependecyInjection
    {
        /// <summary>
        /// Configure core services
        /// </summary>
        /// <param name="serviceProvider">DI engine to be configured</param>
        /// <param name="options">Options to control Dotnet.Solr behavior</param>
        internal static void Configure<TDocument>(ISolrExpressServiceProvider<TDocument> serviceProvider, SolrExpressOptions options)
            where TDocument : Document
        {
            serviceProvider
                .AddSingleton(options)
                .AddTransient(serviceProvider);

            var solrConnection = new SolrConnection(options);
            var expressionBuilder = new ExpressionBuilder<TDocument>(options, solrConnection);
            if (!options.LazyInfraValidation)
            {
                expressionBuilder.LoadDocument();
            }

            serviceProvider
                .AddTransient(expressionBuilder)
                .AddTransient<DocumentSearch<TDocument>>()
                .AddTransient<DocumentUpdate<TDocument>>()
                .AddTransient<SearchResultBuilder<TDocument>>()
                .AddTransient<SearchQuery<TDocument>>()
                .AddTransient<ISolrConnection>(solrConnection)
                .AddTransient<IDocumentResult<TDocument>, DocumentResult<TDocument>>()
                .AddTransient<IChangeDynamicFieldBehaviour<TDocument>, ChangeDynamicFieldBehaviour<TDocument>>();
        }
    }
}
