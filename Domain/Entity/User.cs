using Domain.Attributes;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    [AudiTable]
    public class User
    {
        public Name FirstName { get; private set; }
        public Name LastName { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public NationalIDNumber NationalIDNumber { get; private set; }
        public UserName UserName { get; private set; }
        public Password Password { get; private set; }
        public ICollection<BankAccount>? BankAccounts { get; private set; }
        public ICollection<UserAndNumberOfShare>? UserAndNumberOfShares { get; private set; }
        public User(Name firstName, Name lastName, PhoneNumber phoneNumber,
            NationalIDNumber nationalIDNumber, UserName userName,
            Password password)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            NationalIDNumber = nationalIDNumber;
            UserName = userName;
            Password = password;
        }
        public void Update(Name firstName, Name lastName, PhoneNumber phoneNumber,
            NationalIDNumber nationalIDNumber,
            Password password)
        {

            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            NationalIDNumber = nationalIDNumber;
            Password = password;
        }
    }
}
