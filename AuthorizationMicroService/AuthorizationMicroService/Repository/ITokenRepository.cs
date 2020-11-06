using AuthorizationMicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroService.Repository
{
    public interface ITokenRepository<T>
    {
        UserData GetToken(T entity);
    }
}
