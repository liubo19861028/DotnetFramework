using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dotnet.Elasticsearch.Model
{
	public class GetResult
	{
		[JsonExtensionData]
		public Dictionary<string, JToken> Fields { get; set; }
	}
}