﻿using Dotnet.Elasticsearch.Utils;

namespace Dotnet.Elasticsearch.Model.SearchModel.Filters
{
	public class MatchAllFilter : IFilter
	{
		private double _boost;
		private bool _boostSet;

		public double Boost
		{
			get { return _boost; }
			set
			{
				_boost = value;
				_boostSet = true;
			}
		}

		//{
		// "query" : {
		//	  "match_all" : { }
		//  }
		//}
		public void WriteJson(ElasticsearchCrudJsonWriter elasticsearchCrudJsonWriter)
		{
			elasticsearchCrudJsonWriter.JsonWriter.WritePropertyName("match_all");
			elasticsearchCrudJsonWriter.JsonWriter.WriteStartObject();

			JsonHelper.WriteValue("boost", _boost, elasticsearchCrudJsonWriter, _boostSet);

			elasticsearchCrudJsonWriter.JsonWriter.WriteEndObject();
		}
	}


}
