using Dotnet.Elasticsearch.Model.Units;

namespace Dotnet.Elasticsearch.Model.SearchModel.Queries.FunctionQuery
{
	public class ExpDateTimePointFunction : DateTimeDecayBaseScoreFunction
	{
		public ExpDateTimePointFunction(string field, TimeUnit scale) : base(field, scale, "exp") { }

		public override void WriteJson(ElasticsearchCrudJsonWriter elasticsearchCrudJsonWriter)
		{
			base.WriteJsonBase(elasticsearchCrudJsonWriter, WriteValues);
		}
	}
}