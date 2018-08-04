namespace Dotnet.Elasticsearch.Model.SearchModel
{
	public interface IAggs
	{
		void WriteJson(ElasticsearchCrudJsonWriter elasticsearchCrudJsonWriter);
	}
}