namespace Dotnet.Elasticsearch.Model.SearchModel
{
	public interface IFilter
	{
		void WriteJson(ElasticsearchCrudJsonWriter elasticsearchCrudJsonWriter);
	}
}