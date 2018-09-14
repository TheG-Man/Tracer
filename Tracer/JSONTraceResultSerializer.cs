using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Tracer
{
    public sealed class JSONTraceResultSerializer: ITraceResultSerializer
    {
        public MemoryStream Serialize(MemoryStream outStream, TraceResult traceResult)
        {
            var jsonTree = new JObject();
            var root = new JArray();

            foreach (KeyValuePair<int, TracedThread> tracedThread in traceResult.TracedThreads)
            {
                var thread = new JObject();

                thread.Add("id", new JValue(tracedThread.Key));
                thread.Add("time", new JValue(tracedThread.Value.ExecutionTime));

                foreach (TracedMethod tracedMethod in tracedThread.Value.TracedMethods)
                {
                    thread.Add("methods", CreateMethodsElement(tracedMethod));
                }

                root.Add(thread);
            }

            jsonTree.Add("threads", root);

            String json = jsonTree.ToString();
            outStream.Write(Encoding.ASCII.GetBytes(json), 0, json.Length);

            return outStream;
        }

        private JObject CreateMethodsElement(TracedMethod tracedMethod)
        {
            var method = new JObject();

            method.Add("name", new JValue(tracedMethod.Name));
            method.Add("class", new JValue(tracedMethod.ClassName));
            method.Add("time", new JValue(tracedMethod.ExecutionTime));

            var methods = new JArray();
            foreach (TracedMethod tracedNestedMethod in tracedMethod.NestedMethods)
            {
                methods.Add(CreateMethodsElement(tracedNestedMethod));
            }

            method.Add("methods", methods);

            return method;
        }
    }
}
