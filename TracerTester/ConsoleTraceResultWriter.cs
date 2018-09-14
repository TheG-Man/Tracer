using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Tracer
{
    public sealed class ConsoleTraceResultWriter : ITraceResultWriter
    {
        public void Write(MemoryStream stream)
        {
            stream.Position = 0;
            Console.Write(System.Text.Encoding.Default.GetString(stream.ToArray()));
        }
    }
}
