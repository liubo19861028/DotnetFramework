using Newtonsoft.Json.Linq;
using Dotnet.Solr.Search;
using Dotnet.Solr.Search.Parameter;
using Dotnet.Solr.Search.Query;

namespace Dotnet.Solr.Solr5.Search.Parameter
{
    public sealed class QueryParameter<TDocument> : IQueryParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        public SearchQuery<TDocument> Value { get; set; }

        public void AddResultInContainer(JObject container)
        {
            container.Add(this._result);
        }

        public void Execute()
        {
            this._result = new JProperty("query", this.Value.Execute());
        }
    }
}
