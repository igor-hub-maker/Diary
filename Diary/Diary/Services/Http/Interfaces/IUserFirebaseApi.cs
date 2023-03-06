using Diary.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diary.Services
{
    public interface IUserFirebaseApi
    {
        [Get("/users.json")]
        public Task<List<User>> GetUsers();

        [Put("/users/{id}.json")]
        public Task<User> RegisterUser([AliasAs("id")] string id, User user);
    }
}
