using System.Collections.Generic;
using Dotnet.Elasticsearch.ContextSearch.SearchModel.AggModel.Buckets;
using Newtonsoft.Json.Linq;

namespace Dotnet.Elasticsearch.ContextSearch.SearchModel.AggModel
{
	public class RangesBucketAggregationsResult : AggregationResult<RangesBucketAggregationsResult>
	{
		public List<RangeBucket> Buckets { get; set; }

		public override RangesBucketAggregationsResult GetValueFromJToken(JToken result)
		{
			return result.ToObject<RangesBucketAggregationsResult>();
		}
	}
}