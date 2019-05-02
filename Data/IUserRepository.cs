using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Data.Entities;

namespace UserApi.Data
{
    public interface IUserRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<User[]> GetAllUsersAsync();
        Task<User> GetUserAsync(string cpf);
        User GetUserbyLogin(string login);
      



    }
}
