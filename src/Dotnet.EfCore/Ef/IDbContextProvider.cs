using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Dotnet.Ef.Ef
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IDbContextProvider<out TDbContext>
        where TDbContext : DbContext
    {
        TDbContext GetDbContext();
    }
}
