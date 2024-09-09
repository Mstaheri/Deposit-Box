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
        public OperationResult(bool isSuccess, string? message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public OperationResult(string message)
        {
            Message= message;
            IsSuccess= false;
        }

        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public static OperationResult Failure(string message)
        => new(message);
        public static OperationResult<T> CreateValidator<T>(T param)
            => new(param);
        public static OperationResult<T> Success<T>(T data)
        => new(data);
        public static OperationResult<T> Failure<T>(string message)
        => new(message);
    }
    public class OperationResult<T> : OperationResult
    {
        public OperationResult(bool success, string? message, T? data) :
            base(success, message)
        {
            Data = data;
        }
        public OperationResult(T data) : base(true , string.Empty)
        {
            Data = data;
        }
        public OperationResult(string message) : base(false, message)
        {

        }

        public T? Data { get; set; }
        
    }
    public static class OperationResultExtention
    {
        public static OperationResult<T> Validate<T>(this OperationResult<T> operationResult,
                                                      Func<T, bool> predicit,
                                                      string message)
        {
            if (!operationResult.IsSuccess)
            {
                return operationResult;
            }
            var predicitResult = predicit(operationResult.Data);
            if (predicitResult)
            {
                return OperationResult.Failure<T>(message);
            }
            return operationResult;
        }
    }
    
}
