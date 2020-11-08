using SubscriptionService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubscriptionService.Repository;
//using SubscriptionService.Models;
using SubscriptionService.Controllers;
using log4net.Core;

namespace SubscriptionService.Provider
{
    public class SubscribeProvider : ISubscribeProvider
    {
        ISubscribeDrugs subs;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SubscribeController));
        public SubscribeProvider(ISubscribeDrugs subscribeDrugs)
        {
            subs = subscribeDrugs;
        }

         public SubscriptionDetails Subscribe(PrescriptionDetails subscription, string PolicyDetails, int MemberId)
        {
            try
            {
                return subs.PostSubscription(subscription, PolicyDetails, MemberId);
            }
            catch(Exception ex)
            {
                _log4net.Error("Error occured in provider layer" + ex.Message);
                return null;
            }
        }

        public SubscriptionDetails UnSubscribe(int Member_Id, int Subscription_Id)
        {
            try
            {
                return subs.PostUnSubscription(Member_Id, Subscription_Id);
            }
            catch(Exception ex)
            {
                _log4net.Error("Error occured in provider layer" + ex.Message);
                return null;
            }
        }
    }
}
