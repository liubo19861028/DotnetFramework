using System;

namespace Dotnet.Elasticsearch
{
	public class ElasticsearchCrudException : Exception
	{
		public ElasticsearchCrudException(string message) : base(message)
		{
		}
	}
}
