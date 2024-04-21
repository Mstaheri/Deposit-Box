using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Exceptions
{
    public class OperationResult
    {
        public OperationResult(bool success, string? message)
        {
            Success = success;
            Message = message;
        }
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
    public class OperationResult<T> : OperationResult
    {
        public OperationResult(bool success, string? message, T? data) :
            base(success, message)
        {
            Data = data;
        }
        public T? Data { get; set; }
    }
}
