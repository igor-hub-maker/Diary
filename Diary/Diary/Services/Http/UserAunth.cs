using Diary.Models;
using Refit;
using System;
using System.Threading.Tasks;

namespace Diary.Services
{
    public class UserAunth
    {
        private IUserFirebaseApi restService = RestService.For<IUserFirebaseApi>("https://diary-c636d-default-rtdb.europe-west1.firebasedatabase.app");

        public async Task<User> LoginUser(User localUser)
        {
            var users = await restService.GetUsers();
            if (!(users is null))
            {
                foreach (var user in users)
                {
                    if (user.Login == localUser.Login && user.Password == localUser.Password)
                    {
                        return user;
                    }
                }
            }
            throw new Exception("Email or password is incorrect.");
        }

        public async Task RegisterUser(User localUser)
        {
            var users = await restService.GetUsers();
            if (users is null)
            {
                foreach (var user in users)
                {
                    if (localUser.Login == user.Login)
                    {
                        throw new Exception("User with same login exists");
                    }
                }
            }
            localUser.Id = users.Count;
            await restService.RegisterUser(localUser.Id.ToString(), localUser);
        }
    }
}
