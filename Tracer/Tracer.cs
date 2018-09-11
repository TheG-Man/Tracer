using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;

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
            var stackTrace = new StackTrace(1);
            StackFrame stackFrame = stackTrace.GetFrame(0);
            MethodBase method = stackFrame.GetMethod();

            traceResult.AddMethodToTrace(method);
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
