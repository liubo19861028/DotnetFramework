using NHibernate;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Dotnet.NHibernate
{
    public interface ISessionFactoryHolder 
    {
        ISessionFactory GetSessionFactory();
        Configuration GetConfiguration();
    }
}
