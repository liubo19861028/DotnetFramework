using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Data.Providers
{
    public class ActiveTransactionProviderArgs : Dictionary<string, object>
    {
        public static ActiveTransactionProviderArgs Empty { get; } = new ActiveTransactionProviderArgs();
    }
}
