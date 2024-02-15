using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class User
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string NationalIDNumber { get; private set; }
        public string Email { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Roll { get; private set; }
        public ICollection<BankAccount> BankAccounts { get; private set; }
        public ICollection<UserSharePrice> UserSharePrices { get; private set; }
        public User(string firstName, string lastName, string phoneNumber,
            string nationalIDNumber, string email, string userName,
            string password, string roll)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            NationalIDNumber = nationalIDNumber;
            Email = email;
            UserName = userName;
            Password = password;
            Roll = roll;
        }
        public void Edit(string firstName, string lastName, string phoneNumber,
            string nationalIDNumber, string email,
            string password, string roll)
        {

            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            NationalIDNumber = nationalIDNumber;
            Email = email;
            Password = password;
            Roll = roll;
        }
    }
}
