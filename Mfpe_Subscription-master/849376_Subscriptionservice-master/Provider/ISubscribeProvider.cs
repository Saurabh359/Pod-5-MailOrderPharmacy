using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubscriptionService.Models;
namespace SubscriptionService.Provider
{
    public interface ISubscribeProvider
    {
        SubscriptionDetails Subscribe(PrescriptionDetails subscription, string PolicyDetails, int MemberId);
        SubscriptionDetails UnSubscribe(int Member_Id, int Subscription_Id);
    }
}
