using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Tracer
{
    public interface ITraceResultWriter
    {
        void Write(MemoryStream stream);
    }
}
