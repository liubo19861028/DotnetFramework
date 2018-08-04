using Newtonsoft.Json.Linq;
using Dotnet.Solr.Search;
using Dotnet.Solr.Search.Parameter;
using Dotnet.Solr.Search.Query;
using Dotnet.Solr.Utility;
using System;

namespace Dotnet.Solr.Solr5.Search.Parameter
{
    public class LocalParameter<TDocument> : ILocalParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JProperty _result;

        public string Name { get; set; }
        public SearchQuery<TDocument> Query { get; set; }
        public string Value { get; set; }

        public void AddResultInContainer(JObject container)
        {
            var jObj = (JObject)container["params"] ?? new JObject();
            jObj.Add(this._result);
            container["params"] = jObj;
        }

        public void Execute()
        {
            Checker.IsTrue<ArgumentException>(this.Query == null && string.IsNullOrWhiteSpace(this.Value));

            var value = this.Query?.Execute() ?? this.Value;

            this._result = new JProperty(this.Name, value);
        }
    }
}
