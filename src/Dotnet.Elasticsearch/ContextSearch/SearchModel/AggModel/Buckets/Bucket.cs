using Newtonsoft.Json;

namespace Dotnet.Elasticsearch.ContextSearch.SearchModel.AggModel.Buckets
{
	public class Bucket : BaseBucket
	{
		[JsonProperty("key")]
		public object Key { get; set; }
	}
}