using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;

namespace Tracer
{
    public class TracedThread
    {
        private readonly List<TracedMethod> tracedMethods;
        private readonly Stack<TracedMethod> stack;
        
        internal long ExecutionTime => tracedMethods.Select(tracedMethod => tracedMethod.ExecutionTime).Sum();

        private void AddMethodToTrace(MethodBase method)
        {
            var tracedMethod = new TracedMethod(method);

            if (stack.Count == 0)
            {
                tracedMethods.Add(tracedMethod);
            }
            else
            {
                stack.Peek().AddNestedMethod(tracedMethod);
            }

            stack.Push(tracedMethod);        
        }

        internal TracedThread()
        {
            tracedMethods = new List<TracedMethod>();
            stack = new Stack<TracedMethod>();
        }

        internal void StopTrace()
        {
            if (stack.Count == 0)
            {
                throw new InvalidOperationException("There is not method which is executing at the moment.");
            }

            stack.Peek().StopTrace();
            stack.Pop();
        }

        public static TracedThread operator+(TracedThread lhs, MethodBase rhs)
        {
            lhs.AddMethodToTrace(rhs);
            return lhs;
        }

        public IEnumerable<TracedMethod> TracedMethods => tracedMethods;
    }
}
