using Newtonsoft.Json.Linq;
using Dotnet.Solr.Search;
using Dotnet.Solr.Search.Parameter;

namespace Dotnet.Solr.Solr5.Search.Parameter
{
    public sealed class SortRandomlyParameter<TDocument> : ISortRandomlyParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        public void AddResultInContainer(JObject container)
        {
            container.Add(this._result);
        }

        public void Execute()
        {
            this._result = new JProperty("sort", "random");
        }
    }
}
