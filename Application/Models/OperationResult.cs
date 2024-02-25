using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class OperationResult<T> : OperationResult
    {
        public T Result { get; set; }
    }
}
