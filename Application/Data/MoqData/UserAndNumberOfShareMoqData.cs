using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.MoqData
{
    public class UserAndNumberOfShareMoqData
    {
        public Task<UserAndNumberOfShare> Get()
        {
            UserAndNumberOfShare bankAccount = new UserAndNumberOfShare("Omid","Mstaheri", 123);

            return Task.FromResult(bankAccount);
        }
        public Task<List<UserAndNumberOfShare>> GetAll()
        {
            List<UserAndNumberOfShare> list = new List<UserAndNumberOfShare>()
            {
                new UserAndNumberOfShare("Omid","Mstaheri", 123) ,
                new UserAndNumberOfShare("Omid","Estaheri", 123)
            };
            return Task.FromResult(list);
        }
    }
}
