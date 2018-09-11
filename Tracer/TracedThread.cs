using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Tracer
{
    class TracedThread
    {
        private readonly List<TracedMethod> tracedMethods;
        private readonly Stack<TracedMethod> stack;

        private void AddTracedMethod(MethodBase method)
        {
            if (stack.Count == 0)
            {
                tracedMethods.Add(new TracedMethod(method));
            }
            else
            {
                stack.Peek().AddNestedMethod(method);
            }

            stack.Push(new TracedMethod(method));        
        }

        internal TracedThread()
        {
            tracedMethods = new List<TracedMethod>();
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
            lhs.AddTracedMethod(rhs);
            return lhs;
        }

    }
}
