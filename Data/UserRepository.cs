using CoreCodeCamp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Data.Entities;

namespace UserApi.Data.Migrations
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(UserContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");
            return (await _context.SaveChangesAsync()) > 0;
        }


        public async Task<User> GetUserAsync(string cpf)
        {
            _logger.LogInformation($"Getting a User for {cpf}");
            return await _context.Users.FirstOrDefaultAsync(usr=>usr.CPF == cpf);
        }

        public User GetUserbyLogin(string login)
        {
            _logger.LogInformation($"Getting a User for {login}");
            return  _context.Users.FirstOrDefault(usr => usr.Login == login);
        }

        public Task<User[]> GetAllUsersAsync()
        {
            _logger.LogInformation($"Getting all Users");
            return _context.Users.OrderByDescending(u => u.Nome).ToArrayAsync();
        }

       
    }
}
