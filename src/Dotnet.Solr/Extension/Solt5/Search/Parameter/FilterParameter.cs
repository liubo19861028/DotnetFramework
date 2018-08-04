using Newtonsoft.Json.Linq;
using Dotnet.Solr.Search;
using Dotnet.Solr.Search.Parameter;
using Dotnet.Solr.Search.Parameter.Validation;
using Dotnet.Solr.Search.Query;
using Dotnet.Solr.Utility;

namespace Dotnet.Solr.Solr5.Search.Parameter
{
    [AllowMultipleInstances]
    public sealed class FilterParameter<TDocument> : IFilterParameter<TDocument>, ISearchItemExecution<JObject>
        where TDocument : Document
    {
        private JToken _result;

        public SearchQuery<TDocument> Query { get; set; }
        public string TagName { get; set; }

        public void AddResultInContainer(JObject container)
        {
            var jArray = (JArray)container["filter"] ?? new JArray();
            jArray.Add(this._result);
            container["filter"] = jArray;
        }

        public void Execute()
        {
            this._result = ParameterUtil.GetFilterWithTag(this.Query.Execute(), this.TagName);
        }
    }
}
