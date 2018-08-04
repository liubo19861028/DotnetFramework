using System;
using System.Diagnostics;

namespace Dotnet.Elasticsearch.Tracing
{
	public interface ITraceProvider
	{
		void Trace(TraceEventType level, string message, params object[] args);
		void Trace(TraceEventType level, Exception ex, string message, params object[] args);
	}
}
