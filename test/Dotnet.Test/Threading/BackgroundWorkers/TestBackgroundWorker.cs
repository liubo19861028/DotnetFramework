using Dotnet.Threading.BackgroundWorkers;
using Dotnet.Threading.Timers;

namespace Dotnet.Test.Threading.BackgroundWorkers
{
    public class TestBackgroundWorker : PeriodicBackgroundWorkerBase
    {
        protected TestBackgroundWorker(DotnetTimer timer) : base(timer)
        {

        }

        protected override void DoWork()
        {
            Logger.Info($"DoWork....");
        }
    }
}
