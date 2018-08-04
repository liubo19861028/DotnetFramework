using System.Collections.Generic;
using Dotnet.Elasticsearch.Utils;

namespace Dotnet.Elasticsearch.Model.GeoModel
{
	public class GeoShapeMultiPoint : IGeoType
	{
		public List<GeoPoint> Coordinates { get; set; }

		public string Type { get; set; }

		public void WriteJson(ElasticsearchCrudJsonWriter elasticsearchCrudJsonWriter)
		{
			elasticsearchCrudJsonWriter.JsonWriter.WriteStartObject();
			JsonHelper.WriteValue("type", DefaultGeoShapes.MultiPoint, elasticsearchCrudJsonWriter);
			elasticsearchCrudJsonWriter.JsonWriter.WritePropertyName("coordinates");
			elasticsearchCrudJsonWriter.JsonWriter.WriteStartArray();
			foreach (var item in Coordinates)
			{
				item.WriteJson(elasticsearchCrudJsonWriter);
			}
			elasticsearchCrudJsonWriter.JsonWriter.WriteEndArray();
			elasticsearchCrudJsonWriter.JsonWriter.WriteEndObject();
		}
	}
}