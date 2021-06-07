using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Data
{
    public class Seed
    {

        public static void SeedData(DataContext context)
        {
            //foreach (var item in context.Users.ToList())
            //{
            //    password = Utility.SHA256Hash(string.Concat(item.Salt, "@Password1"));
            //    item.Password = password;
            //    context.Users.Update(item);
            //}
            //context.SaveChanges();
        }
    }
}
