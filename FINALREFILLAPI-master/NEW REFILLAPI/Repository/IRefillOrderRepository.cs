using Microsoft.AspNetCore.Mvc;
using RefillApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefillApi.Repository
{
    public interface IRefillOrderRepository
    {

        public RefillOrder AdhocRefill(int PolicyId, int MemberId, int SubscriptionId);
        public int RefillDues(int id);
        public RefillOrder RefillStatus(int id);
        


    }
}
