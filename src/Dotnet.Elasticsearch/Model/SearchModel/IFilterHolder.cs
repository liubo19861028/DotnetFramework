namespace Dotnet.Elasticsearch.Model.SearchModel
{
	public interface IFilterHolder
	{
		void WriteJson(ElasticsearchCrudJsonWriter elasticsearchCrudJsonWriter);
	}
}