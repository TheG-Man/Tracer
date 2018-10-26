using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Tracer
{
    public sealed class FileTraceResultWriter : ITraceResultWriter
    {
        private readonly String fileName;

        public FileTraceResultWriter(String fileName)
        {
            this.fileName = fileName;
        }

        public void Write(MemoryStream stream)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                stream.Position = 0;
                stream.CopyTo(fileStream);
            }
        }
    }
}
