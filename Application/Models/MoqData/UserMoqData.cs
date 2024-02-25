using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace Application.Models.MoqData
{
    public class UserMoqData
    {
        public Task<List<User>> GetAll()
        {
            List<User> list = new List<User>()
            {
                new User("mohammad", "salman", "09306994906",
                "1250832456", "mstaheri@gmail.com"
                , "mstaheri", "1234", "admin") ,
                new User("erfan", "salman", "09306994906",
                "1250832456", "mstaheri@gmail.com"
                , "Estaheri", "1234", "admin")
            };
            return Task.FromResult(list);
        }
        public Task<User> Get()
        {
            User user = new User("mohammad", "salman", "09306994906",
                "1250832456", "mstaheri@gmail.com"
                , "mstaheri", "1234", "admin");

            return Task.FromResult(user);
        }
    }
}
