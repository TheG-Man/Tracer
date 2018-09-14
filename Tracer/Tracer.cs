using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public sealed class Tracer : ITracer
    {
        private readonly TraceResult traceResult;

        private static volatile Tracer instance = null;
        private static readonly object syncRoot = new object();

        private Tracer()
        {
            traceResult = new TraceResult();
        }

        public void StartTrace()
        {
            traceResult.StartTrace();
        }

        public void StopTrace()
        {
            traceResult.StopTrace();
        }

        public TraceResult GetTraceResult()
        {
            return traceResult;
        }

        public static Tracer GetInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new Tracer();
                    }
                }
            }

            return instance;
        }
    }
}
