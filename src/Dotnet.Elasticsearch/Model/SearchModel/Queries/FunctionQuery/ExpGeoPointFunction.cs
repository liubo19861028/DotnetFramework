using Dotnet.Elasticsearch.Model.GeoModel;
using Dotnet.Elasticsearch.Model.Units;

namespace Dotnet.Elasticsearch.Model.SearchModel.Queries.FunctionQuery
{
	public class ExpGeoPointFunction : GeoDecayBaseScoreFunction
	{
		public ExpGeoPointFunction(string field, GeoPoint origin, DistanceUnit scale) : base(field, origin, scale, "exp"){}

		public override void WriteJson(ElasticsearchCrudJsonWriter elasticsearchCrudJsonWriter)
		{
			base.WriteJsonBase(elasticsearchCrudJsonWriter, WriteValues);
		}
	}
}