using System;

namespace Dotnet.Elasticsearch.ContextAddDeleteUpdate.CoreTypeAttributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class ElasticsearchByte : ElasticsearchNumber
	{
		public override string JsonString()
		{
			return JsonStringInternal("byte");
		}
	}
}