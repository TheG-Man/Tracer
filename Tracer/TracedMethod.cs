using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Tracer
{
    class TracedMethod
    {
        private readonly List<TracedMethod> nestedMethods;
        private readonly Stopwatch stopwatch;

        internal string Name { get; }
        internal string ClassName { get; }
        internal long ExecutionTime => stopwatch.ElapsedMilliseconds;

        internal TracedMethod(MethodBase method)
        {
            Name = method.Name;
            ClassName = method.DeclaringType.Name;

            nestedMethods = new List<TracedMethod>();

            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        internal void AddNestedMethod(TracedMethod tracedMethod)
        {
            nestedMethods.Add(tracedMethod);
        }

        internal void StopTrace()
        {
            stopwatch.Stop();
        }

        internal IEnumerable<TracedMethod> NestedMethods => nestedMethods;
    }
}
