using Dotnet.Threading.Timers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace Dotnet.Test.Threading
{
    public class DotnetTimerTest
    {
        private int _testValue = 1;
        [Fact]
        public void DotnetTimer_Test()
        {
            var timer = new DotnetTimer(100, false);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            Thread.Sleep(300);
            timer.Stop();
            Assert.Equal(2, _testValue);
        }

        private void Timer_Elapsed(object sender, EventArgs e)
        {
            _testValue = 2;
        }
    }
}
