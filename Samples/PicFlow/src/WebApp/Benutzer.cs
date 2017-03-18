using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FP.Spartakiade2017.PicFlow.Contracts.Dbo;
using Marten;
using Marten.Linq;

namespace FP.Spartakiade2017.PicFlow.WebApp
{
    public class Benutzer
    {
        private string _dbConnectionString = "host=localhost;database=spartakiade;password=sportfrei;username=spartakiade";

        public async Task Test()
        {
            var store = DocumentStore.For(_dbConnectionString);
            using (var session = store.LightweightSession())
            {
                var user1 = new User();
                user1.FirstName = "Kevin";
                user1.LastName = "Lehmann";
                user1.UserName = "klehmann";
                user1.Id = Guid.NewGuid();
                user1.PasswordHash = HashPassword("sport");

                session.Store(user1);
                await session.SaveChangesAsync();

                ////var id = Guid.Parse("1a918565-d903-4367-98de-a55bcdfc0819");
                ////var user = await session.Query<User>().Where(x => x.Id == id).FirstAsync();

                //var user = await session.Query<User>().ToListAsync();

                //foreach (var u in user)
                //{
                //    u.PasswordHash = HashPassword("sport");
                //    session.Store(u);
                //}


               // await session.SaveChangesAsync();

            }

        }

        private static string HashPassword(string password)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var passwordBytes = Encoding.ASCII.GetBytes(password);
            var passwordHash = sha1.ComputeHash(passwordBytes);
            var passwordBase64 = Convert.ToBase64String(passwordHash);
            return passwordBase64;
        }
    }
}
