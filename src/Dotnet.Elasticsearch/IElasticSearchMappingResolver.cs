using System;

namespace Dotnet.Elasticsearch
{
	public interface IElasticsearchMappingResolver
	{
		ElasticsearchMapping GetElasticSearchMapping(Type type);
		void AddElasticSearchMappingForEntityType(Type type, ElasticsearchMapping mapping);
	}
}