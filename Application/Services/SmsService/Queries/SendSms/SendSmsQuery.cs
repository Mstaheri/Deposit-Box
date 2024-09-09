using Domain.Exceptions;
using Domain.IRepositories.ISmsSevice;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.SmsService.Queries.SendSms
{
    public record SendSmsQuery : IRequest<OperationResult>
    {
        public required string ApiUrl { get; init; }
        public required string Receptor { get; init; }
        public required string Token { get; init; }
        public required string Token2 { get; init; }
        public required string Token3 { get; init; }
        public required string Template { get; init; }
    }
}
