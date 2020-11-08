using RefillApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefillApi.Provider
{
    public interface IRefillOrderProvider
    {
        RefillOrder AdhocRefill(int PolicyId, int MemberId, int SubscriptionId);
        int RefillDues(int id);
       RefillOrder RefillStatus(int id);
    }
}
