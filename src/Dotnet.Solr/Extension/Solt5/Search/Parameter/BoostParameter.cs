using Newtonsoft.Json.Linq;
using Dotnet.Solr.Search;
using Dotnet.Solr.Search.Parameter;
using Dotnet.Solr.Search.Query;

namespace Dotnet.Solr.Solr5.Search.Parameter
{
    public sealed class BoostParameter<TDocument> : IBoostParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        public BoostFunctionType BoostFunctionType { get; set; }
        public SearchQuery<TDocument> Query { get; set; }

        public void AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["params"] ?? new JObject();
            jObj.Add(this._result);
            container["params"] = jObj;
        }

        public void Execute()
        {
            var boostFunction = this.BoostFunctionType.ToString().ToLower();

            this._result = new JProperty(boostFunction, this.Query.Execute());
        }
    }
}
