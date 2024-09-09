using Application.Services.BankSafeDocuments.Command.AddBankSafeDocuments;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories.ISmsSevice;
using Glimpse.Core.Extensibility;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.SmsService.Queries.SendSms
{
    public class SendSmsQueryHandler :
        IRequestHandler<SendSmsQuery, OperationResult>
    {
        private readonly ISmsServiceRepositorieQuery _smsServiceRepositorieQuery;
        public SendSmsQueryHandler(ISmsServiceRepositorieQuery smsServiceRepositorieQuery,
            ILogger<SendSmsQueryHandler> logger )
        {
            _smsServiceRepositorieQuery= smsServiceRepositorieQuery;
        }
        public async Task<OperationResult> Handle(SendSmsQuery request, CancellationToken cancellationToken)
        {
            var response = await _smsServiceRepositorieQuery.SendAsync
                     (request.ApiUrl, request.Receptor, request.Token,
                     request.Token2, request.Token3, request.Template);
            return response;
        }
    }
}
