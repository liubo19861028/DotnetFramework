using System;

namespace Dotnet.Elasticsearch.ContextAddDeleteUpdate.CoreTypeAttributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class ElasticsearchInteger : ElasticsearchNumber
	{
		public override string JsonString()
		{
			return JsonStringInternal("integer");
		}
	}
}