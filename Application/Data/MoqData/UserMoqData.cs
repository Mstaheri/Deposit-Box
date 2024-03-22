using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace Application.Data.MoqData
{
    public class UserMoqData
    {
        public Task<List<User>> GetAll()
        {
            List<User> list = new List<User>()
            {
                new User("mohammad", "salman", "09306994906",
                "1250832456"
                , "mstaheri", "1234") ,
                new User("erfan", "salman", "09306994906",
                "1250832456"
                , "Estaheri", "1234")
            };
            return Task.FromResult(list);
        }
        public Task<User> Get()
        {
            User user = new User("mohammad", "salman", "09306994906",
                "1250832456"
                , "mstaheri", "1234");

            return Task.FromResult(user);
        }
    }
}
