using RefillApi.Models;
using RefillApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefillApi.Provider
{
    public class RefillOrderProvider : IRefillOrderProvider
    {
        IRefillOrderRepository _refillOrderRepository;

        public RefillOrderProvider(IRefillOrderRepository repository1)
        {
            _refillOrderRepository = repository1;
        }

        public RefillOrder AdhocRefill(int PolicyId, int MemberId, int SubscriptionId)
        {
            return _refillOrderRepository.AdhocRefill(PolicyId, MemberId, SubscriptionId);
        }

            public int RefillDues(int id)
        {
            return (_refillOrderRepository.RefillDues(id));
        }

        public RefillOrder RefillStatus(int id)
        {
            return _refillOrderRepository.RefillStatus(id);
        }
    }
}
