using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Tracer;

namespace UnitTests
{
    [TestFixture]
    public class TracerUnitTest
    {
        private readonly ITracer tracer = Tracer.Tracer.GetInstance();

        [SetUp]
        public void Setup()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        [Test]
        public void MethodNameTest()
        {
            tracer.GetTraceResult().TracedThreads.First();
            foreach (KeyValuePair<int, TracedThread> thread in tracer.GetTraceResult().TracedThreads)
            {
                foreach (TracedMethod method in thread.Value.TracedMethods)
                {
                    Assert.AreEqual(method.Name, "Setup");
                }
            }
        }

        [Test]
        public void MethodClassNameTest()
        {
            foreach (KeyValuePair<int, TracedThread> thread in tracer.GetTraceResult().TracedThreads)
            {
                foreach (TracedMethod method in thread.Value.TracedMethods)
                {
                    Assert.AreEqual(method.ClassName, "TracerUnitTest");
                }
            }
        }

        [Test]
        public void MethodExecutionTimeTest()
        {
            foreach (KeyValuePair<int, TracedThread> thread in tracer.GetTraceResult().TracedThreads)
            {
                foreach (TracedMethod method in thread.Value.TracedMethods)
                {
                    Assert.GreaterOrEqual(method.ExecutionTime, 100);
                }
            }
        }
    }
}
