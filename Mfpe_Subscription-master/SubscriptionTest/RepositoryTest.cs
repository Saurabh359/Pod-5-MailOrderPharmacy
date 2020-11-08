using Castle.Components.DictionaryAdapter;
using Microsoft.AspNet.SignalR.Client.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Moq;
using NUnit.Framework;
using SubscriptionService.Controllers;
using SubscriptionService.Models;
using SubscriptionService.Provider;
using SubscriptionService.Repository;
namespace SubscriptionTest
{
    class RepositoryTest
    {
        
        private SubscribeDrugs sub;

        PrescriptionDetails pre = new PrescriptionDetails()
        {
            DoctorName = "abc",
            DrugName = "Disprin",
            Id = 1,
            InsurancePolicyNumber = 2,
            InsuranceProvider = "xyz",
            PrescriptionDate = DateAndTime.Now,
            RefillOccurrence = "Weekly"

        };
        PrescriptionDetails pre1 = new PrescriptionDetails()
        {
            DoctorName = "abc",
            DrugName = "vetmal",
            Id = 1,
            InsurancePolicyNumber = 2,
            InsuranceProvider = "xyz",
            PrescriptionDate = DateAndTime.Now,
            RefillOccurrence = "Weekly"

        };
        [SetUp]
        public void Setup()
        {
            
            sub = new SubscribeDrugs();

        }
        [Test]
        public void PostSubscribe_WhenCalled_returnobject()
        {

            var res = sub.PostSubscription(pre, "hello", 1);
            Assert.That(res.Status, Is.True);

        }
        [Test]
        public void PostSubscribe_DrugNotAvailable_returnnullObject()
        {
           
            var res = sub.PostSubscription(pre1, "hello123", 2);
            Assert.That(res.Status, Is.False);
        }
        
        [Test]
        public void PostUnSubscribe_PaymentDone_returnObject()
        {
           
            var result = sub.PostUnSubscription(1, 1);
            Assert.That(result.Status, Is.False) ;
        }
        [Test]
        public void PostUnsubscribe_PaymentDue_returnNull()
        {
           
            var result = sub.PostUnSubscription(1, 3);
            Assert.That(result.Status, Is.True);
        }

        
    }
}
