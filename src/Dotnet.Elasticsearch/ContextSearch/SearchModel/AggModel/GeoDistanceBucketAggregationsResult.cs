using System.Collections.Generic;
using Dotnet.Elasticsearch.ContextSearch.SearchModel.AggModel.Buckets;
using Newtonsoft.Json.Linq;

namespace Dotnet.Elasticsearch.ContextSearch.SearchModel.AggModel
{
	public class GeoDistanceBucketAggregationsResult : AggregationResult<GeoDistanceBucketAggregationsResult>
	{
		public List<GeoDistanceRangeBucket> Buckets { get; set; }

		public override GeoDistanceBucketAggregationsResult GetValueFromJToken(JToken result)
		{
			return result.ToObject<GeoDistanceBucketAggregationsResult>();
		}
	}
}