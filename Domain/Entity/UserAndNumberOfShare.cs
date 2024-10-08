﻿using Domain.Attributes;
using Domain.Common;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    [AudiTable]
    public class UserAndNumberOfShare : IEntity
    {
        public UserAndNumberOfShare(Name nameBankSafe, UserName userName, Number numberOfShares)
        {
            NameBankSafe = nameBankSafe;
            UserName = userName;
            NumberOfShares = numberOfShares;
        }
        public Name NameBankSafe { get; private set; }
        public UserName UserName { get; private set; }
        public Number NumberOfShares { get; private set; }
        public virtual BankSafe BankSafe { get; private set; }
        public virtual User User { get; private set; }
        
        public void Update(Number numberOfShares)
        {
            NumberOfShares = numberOfShares;
        }

    }
}
