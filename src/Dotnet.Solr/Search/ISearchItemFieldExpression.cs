using Dotnet.Solr.Builder;
using System;
using System.Linq.Expressions;

namespace Dotnet.Solr.Search
{
    /// <summary>
    /// Property FieldExpression
    /// </summary>
    public interface ISearchItemFieldExpression<TDocument>
        where TDocument : Document
    {
        /// <summary>
        /// Build expressions engine
        /// </summary>
        ExpressionBuilder<TDocument> ExpressionBuilder { get; }

        /// <summary>
        /// Expression used to find field name
        /// </summary>
        Expression<Func<TDocument, object>> FieldExpression { get; set; }
    }
}
