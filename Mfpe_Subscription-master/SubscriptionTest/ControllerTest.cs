using Castle.Components.DictionaryAdapter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using NUnit.Framework;
using SubscriptionService.Controllers;
using SubscriptionService.Models;
using SubscriptionService.Provider;
using SubscriptionService.Repository;
using System.Web.Http;

namespace SubscriptionTest
{
    class ControllerTest
    {
        private Mock<ISubscribeProvider> _prod;
        private SubscribeController sub;

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

         [SetUp]
        public void Setup()
        {
            _prod = new Mock<ISubscribeProvider>();
            sub = new SubscribeController(_prod.Object);

        }
        [Test]
        public void PostSubscribe_WhenCalled_returnobject()
        {
            _prod.Setup(p => p.Subscribe(pre, "hello", 1,"abc")).Returns(new SubscriptionDetails()
            {
                Id = 1,
                Status = true,
                MemberId = 101,
                MemberLocation = "Delhi",
                PrescriptionId = 21,
                RefillOccurrence = "Weekly",
                SubscriptionDate = DateAndTime.Now
            });
            var res = sub.PostSubscribe(pre, "hello", 1,"abc");
            var check = res as OkObjectResult;
            var value = check.Value as SubscriptionDetails;
            Assert.That(value.Status, Is.True);

        }
        [TestCase(null,"abc",1,null)]
        [TestCase(null,null,1,null)]
        [TestCase(null,"",1,null)]
        [TestCase(null,null,-1,null)]
        [TestCase(null, null,0,null)]
        public void PostSubscribe_prescriptionisnull_returnobject(PrescriptionDetails prescription, string policy, int MemberId,string auth)
        {
            var result = (BadRequestResult)sub.PostSubscribe(prescription, policy, MemberId,auth);
            Assert.AreEqual(result.StatusCode,400);
        }
       
        [Test]
        public void PostUnSubscribe_WhenCalled_returnobject()
        {
            _prod.Setup(p => p.UnSubscribe(1, 1,"abc")).Returns(new SubscriptionDetails()
            {
                 Id = 1,
                Status = false,
                MemberId = 101,
                MemberLocation = "Delhi",
                PrescriptionId = 21,
                RefillOccurrence = "Weekly",
                SubscriptionDate = DateAndTime.Now
            });
            var result = sub.PostUnsubscribe(1, 1,"abc");
            var check = result as OkObjectResult;
            var value = check.Value as SubscriptionDetails;
            Assert.That(value.Status, Is.False);
           

        }
       
       
        [TestCase(-1,1,null)]
        [TestCase(-1, -1,"abc")]
        [TestCase(0, 1,null)]
        [TestCase(1, 0,"abc")]
        [TestCase(-1, 0,"abc")]
        [TestCase(0, -1,"abc")]
        [TestCase(1, -1,"abc")]
        public void PostUnSubscribe_whennegativevaluepassed_returnobject(int MemberId,int SubscriptionId,string auth)
        {
            var result = (BadRequestResult)sub.PostUnsubscribe(MemberId,SubscriptionId,auth);
            Assert.AreEqual(result.StatusCode, 400);
        }
        

    }
}
