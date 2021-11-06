using ParkyWeb.Models;
using ParkyWeb.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParkyWeb.Repositories
{
    public class AccountRepository : Repository<UserModel>, IAccountRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public AccountRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public Task<UserModel> LoginAsync(string url, UserModel objToCreate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterAsync(string url, UserModel objToCreate)
        {
            throw new NotImplementedException();
        }
    }
}
