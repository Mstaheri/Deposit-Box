﻿using Domain.Entity;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BankSafeTransactions.Queries.GetBankSafeTransaction
{
    public record GetBankSafeTransactionCommand
        : IRequest<OperationResult<BankSafeTransaction>>
    {
        public required Guid Code { get; init; }
    }
}
