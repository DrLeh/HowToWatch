using HowToWatch.Core.Data;
using HowToWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowToWatch.Services
{
    public interface IUserService
    {
        IEnumerable<UserServicePreference> GetUserServicePreferences(long userId);
    }

    public class UserService : IUserService
    {
        public IRepository Repository { get; }

        public UserService(IRepository repository)
        {
            Repository = repository;
        }

        public IEnumerable<UserServicePreference> GetUserServicePreferences(long userId)
        {
            return Repository.UserServices.Where(x => x.UserId == userId).ToList();
        }
    }
}
