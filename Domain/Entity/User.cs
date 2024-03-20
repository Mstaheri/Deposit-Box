using Domain.Attributes;
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
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string NationalIDNumber { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public ICollection<BankAccount>? BankAccounts { get; private set; }
        public ICollection<UserAndNumberOfShare>? UserAndNumberOfShares { get; private set; }
        public User(string firstName, string lastName, string phoneNumber,
            string nationalIDNumber, string userName,
            string password)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            NationalIDNumber = nationalIDNumber;
            UserName = userName;
            Password = password;
        }
        public void Update(string firstName, string lastName, string phoneNumber,
            string nationalIDNumber,
            string password)
        {

            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            NationalIDNumber = nationalIDNumber;
            Password = password;
        }
    }
}
