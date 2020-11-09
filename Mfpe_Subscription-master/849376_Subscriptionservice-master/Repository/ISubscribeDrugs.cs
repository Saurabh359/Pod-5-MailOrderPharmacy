using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubscriptionService.Models;
namespace SubscriptionService.Repository
{
   public interface ISubscribeDrugs
    {
        SubscriptionDetails PostSubscription(PrescriptionDetails subscription, string PolicyDetails, int MemberId, string auth);
        SubscriptionDetails PostUnSubscription(int Member_Id, int Subscription_Id, string auth);
    }
}
