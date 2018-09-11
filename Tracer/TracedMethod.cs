using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace Tracer
{
    class TracedMethod
    {
        private readonly Stopwatch stopwatch;
        private readonly List<TracedMethod> nestedMethods;

        internal string Name { get; }
        internal string ClassName { get; }
        internal long ExecutionTime { get; private set; }

        internal TracedMethod(MethodBase method)
        {
            Name = method.Name;
            ClassName = method.DeclaringType.Name;

            nestedMethods = new List<TracedMethod>();

            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        internal void AddNestedMethod(MethodBase method)
        {
            nestedMethods.Add(new TracedMethod(method));
        }

        internal void StopTrace()
        {
            stopwatch.Stop();
            ExecutionTime = stopwatch.ElapsedMilliseconds;
        }

        internal IEnumerable<TracedMethod> NestedMethods => nestedMethods;
    }
}
