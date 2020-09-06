using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Common
{
    public class Result<T>
    {
        public bool IsSucced { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
