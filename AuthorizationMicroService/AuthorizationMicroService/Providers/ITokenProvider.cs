using AuthorizationMicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroService.Providers
{
    public interface ITokenProvider<T>
    {
        UserData GetToken(T entity);
    }
}
