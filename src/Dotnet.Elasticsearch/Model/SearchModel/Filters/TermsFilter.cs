using System.Collections.Generic;
using Dotnet.Elasticsearch.Utils;

namespace Dotnet.Elasticsearch.Model.SearchModel.Filters
{
	public class TermsFilter : IFilter
	{
		private readonly string _term;
		private readonly List<object> _termValues;

		public TermsFilter(string term, List<object> termValues)
		{
			_term = term;
			_termValues = termValues;
		}

		public void WriteJson(ElasticsearchCrudJsonWriter elasticsearchCrudJsonWriter)
		{
			elasticsearchCrudJsonWriter.JsonWriter.WritePropertyName("terms");
			elasticsearchCrudJsonWriter.JsonWriter.WriteStartObject();

			JsonHelper.WriteListValue(_term, _termValues, elasticsearchCrudJsonWriter);

			elasticsearchCrudJsonWriter.JsonWriter.WriteEndObject();
		}
	}
}
