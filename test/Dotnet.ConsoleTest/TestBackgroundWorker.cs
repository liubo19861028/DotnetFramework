using Dotnet.Threading.BackgroundWorkers;
using Dotnet.Threading.Timers;
using System;

namespace Dotnet.ConsoleTest
{
    public class TestBackgroundWorker : PeriodicBackgroundWorkerBase
    {
        public TestBackgroundWorker(DotnetTimer timer) : base(timer)
        {
            timer.Period = 1000;
            //  timer.RunOnStart = true;
        }

        protected override void DoWork()
        {
            Logger.Info("111");
            Console.WriteLine("Work");
        }
    }
}
