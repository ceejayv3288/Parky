using ParkyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Repositories.IRepositories
{
    public interface IAccountRepository : IRepository<UserModel>
    {
        Task<UserModel> LoginAsync(string url, UserModel objToCreate);
        Task<bool> RegisterAsync(string url, UserModel objToCreate);
    }
}
