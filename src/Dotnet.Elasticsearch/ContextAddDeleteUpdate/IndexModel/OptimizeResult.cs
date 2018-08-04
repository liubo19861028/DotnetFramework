using Dotnet.Elasticsearch.ContextSearch.SearchModel;
using Dotnet.Elasticsearch.Model;
using Newtonsoft.Json;

namespace Dotnet.Elasticsearch.ContextAddDeleteUpdate.IndexModel
{
	public class OptimizeResult
	{
		[JsonProperty(PropertyName = "_shards")]
		public Shards Shards { get; set; }
	}
}