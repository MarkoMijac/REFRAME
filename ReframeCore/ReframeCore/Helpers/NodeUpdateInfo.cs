using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Helpers
{
    public class NodeUpdateInfo
    {
        public TimeSpan UpdateDuration { get; private set; }
        public DateTime UpdateStartedAt { get; private set; }
        public DateTime UpdateCompletedAt { get; private set; }

        private Stopwatch _sw;

        public void StartMeasuring()
        {
            _sw = new Stopwatch();
            _sw.Start();
            UpdateStartedAt = DateTime.Now;
        }

        public void EndMeasuring()
        {
            UpdateCompletedAt = DateTime.Now;
            _sw.Stop();

            UpdateDuration = _sw.Elapsed;
        }
    }
}
