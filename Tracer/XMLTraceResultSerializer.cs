using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace Tracer
{
    public sealed class XMLTraceResultSerializer: ITraceResultSerializer
    {
         public MemoryStream Serialize(MemoryStream outStream, TraceResult traceResult)
         {
            var xmlTree = new XDocument();
            var root = new XElement("root");

            foreach (KeyValuePair<int, TracedThread> tracedThread in traceResult.TracedThreads)
            {
                var thread = new XElement("thread");

                thread.Add(new XAttribute("id", tracedThread.Key));
                thread.Add(new XAttribute("time", tracedThread.Value.ExecutionTime));

                foreach (TracedMethod tracedMethod in tracedThread.Value.TracedMethods)
                {
                    thread.Add(CreateMethodElement(tracedMethod));
                }

                root.Add(thread);
            }

            xmlTree.Add(root);
            xmlTree.Save(outStream);

            return outStream;
        }

        private XElement CreateMethodElement(TracedMethod tracedMethod)
        {
            var method = new XElement("method");

            method.Add(new XAttribute("name", tracedMethod.Name));
            method.Add(new XAttribute("class", tracedMethod.ClassName));
            method.Add(new XAttribute("time", tracedMethod.ExecutionTime));

            foreach (TracedMethod tracedNestedMethod in tracedMethod.NestedMethods)
            {
                method.Add(CreateMethodElement(tracedNestedMethod));
            }

            return method;
        }
    }
}
