namespace Dotnet.Elasticsearch.Model.SearchModel
{
	public interface IQuery
	{
		void WriteJson(ElasticsearchCrudJsonWriter elasticsearchCrudJsonWriter);
	}
}