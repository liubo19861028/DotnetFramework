using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Dotnet.MongoDb.Configuration;
using MongoDB.Driver;

namespace Dotnet.MongoDb
{
    public class DefaultMongoDatabaseProvider : IMongoDatabaseProvider
    {
        private const string ConnectionStringKey = "MongoDb.ConnectionString";

        private const string DatabaseIdSettingKey = "MongoDb.DatatabaseName";

        internal MongoDbModuleConfiguration MongoDbModuleConfiguration
        {
           get {
                return new MongoDbModuleConfiguration
                {
                     ConnectionString = ConfigurationManager.AppSettings[ConnectionStringKey],
                      DatatabaseName = ConfigurationManager.AppSettings[DatabaseIdSettingKey]
                };
            }
        }

        public MongoDatabase Database {
            get {
                string connectionStr = MongoDbModuleConfiguration.ConnectionString;
                MongoClient client = new MongoClient(connectionStr);
                MongoServer server = client.GetServer();
                MongoDatabase db = server.GetDatabase(MongoDbModuleConfiguration.DatatabaseName);

                return db;
            }

        }
    }
}
