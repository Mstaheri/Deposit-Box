using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.ISmsSevice
{
    public interface ISmsServiceRepositorieQuery
    {
        Task<OperationResult> SendAsync(string apiUrl, string Receptor, string token, string token2, string token3, string template);
    }
}
