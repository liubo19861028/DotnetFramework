namespace Dotnet.Elasticsearch.Model.SearchModel
{
	public interface IQueryHolder
	{
		void WriteJson(ElasticsearchCrudJsonWriter elasticsearchCrudJsonWriter);
	}
}