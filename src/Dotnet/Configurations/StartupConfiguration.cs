namespace Dotnet.Configurations
{
    public class StartupConfiguration : DictionaryBasedConfig
    {
        public BackgroundWorkerConfiguration BackgroundWorker { get; set; } = new BackgroundWorkerConfiguration();
    }
}
