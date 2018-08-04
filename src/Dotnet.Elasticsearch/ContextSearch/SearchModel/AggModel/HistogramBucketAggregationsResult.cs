using System.Collections.Generic;
using Dotnet.Elasticsearch.ContextSearch.SearchModel.AggModel.Buckets;
using Newtonsoft.Json.Linq;

namespace Dotnet.Elasticsearch.ContextSearch.SearchModel.AggModel
{
	public class HistogramBucketAggregationsResult : AggregationResult<HistogramBucketAggregationsResult>
	{
		public List<Bucket> Buckets { get; set; }

		public override HistogramBucketAggregationsResult GetValueFromJToken(JToken result)
		{
			return result.ToObject<HistogramBucketAggregationsResult>();
		}

	}
}