using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;
using System.Linq;
using System.Text;


namespace Tracer
{
    public sealed class TraceResult
    {
        private readonly ConcurrentDictionary<int, TracedThread> tracedThreads;

        internal TraceResult()
        {
            tracedThreads = new ConcurrentDictionary<int, TracedThread>();
        }

        internal void AddTracedMethod(MethodBase method)
        {
            TracedThread tracedThread = tracedThreads.GetOrAdd(Thread.CurrentThread.ManagedThreadId, new TracedThread());
            tracedThread += method;
        }

        internal void StopTrace()
        {
            TracedThread tracedThread;
            int threadId = Thread.CurrentThread.ManagedThreadId;

            if (!tracedThreads.TryGetValue(threadId, out tracedThread))
            {
                throw new ArgumentException("There is not a thread " + threadId);
            }

            tracedThread.StopTrace();
        }

        internal IEnumerable<KeyValuePair<int, TracedThread>> TracedThreads => tracedThreads;
    }
}
