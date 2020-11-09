using Moq;
using NUnit.Framework;
using RefillApi.Models;
using RefillApi.Repository;
using System;
using System.Collections.Generic;

namespace RefillServiceTesting
{
    public class Tests
    {
       
        RefillOrder refillorder = new RefillOrder();
        List<RefillOrder> dataObject = new List<RefillOrder>();
        [SetUp]
        public void Setup()
        {
            dataObject = new List<RefillOrder>()
            {
            new RefillOrder{ Id =1, RefillDate = Convert.ToDateTime("2020-11-24 12:12:00 PM"),DrugQuantity = 10, RefillDelivered=true,Payment=true, SubscriptionId=1},
            new RefillOrder{ Id =2, RefillDate = Convert.ToDateTime("2020-11-24 12:12:00 PM"),DrugQuantity = 10, RefillDelivered=true,Payment=true, SubscriptionId=1},
            new RefillOrder{ Id =3, RefillDate = Convert.ToDateTime("2020-11-24 12:12:00 PM"),DrugQuantity = 10, RefillDelivered=false,Payment=false, SubscriptionId=1}

            };

            }

        [Test]
        public void RepositoryGetStatusTest1()
        {
            RefillOrder refillOrder = new RefillOrder();
            Mock<IRefillOrderRepository> refillContextMock = new Mock<IRefillOrderRepository>();
            var refillRepoObject = new RefillOrderRepository();
            refillContextMock.Setup(x => x.RefillStatus(1)).Returns(refillOrder);
            var refillStatus = refillRepoObject.RefillStatus(1);
            Assert.IsNotNull(refillStatus);
            //Assert.AreEqual("Pending", refillStatus);
        }
        
        [Test]
        public void RepositoryGetStatusTest2()
        {
            //RefillOrder refillOrder = new RefillOrder();
            int p = 1;
            Mock<IRefillOrderRepository> refillContextMock = new Mock<IRefillOrderRepository>();
            var refillRepoObject = new RefillOrderRepository();
            refillContextMock.Setup(x => x.RefillDues(1)).Returns(p);
            var refillDues = refillRepoObject.RefillDues(1);
            Assert.IsNotNull(refillDues);
        }


        [Test]
        public void RepositoryGetStatusTest3()
        {
            //RefillOrder refillOrder = new RefillOrder();
            //int p = 1;
            Mock<IRefillOrderRepository> refillContextMock = new Mock<IRefillOrderRepository>();
            var refillRepoObject = new RefillOrderRepository();
            refillContextMock.Setup(x => x.AdhocRefill(1,2,1,"abc")).Returns(refillorder);
            var adhocrefill = refillRepoObject.AdhocRefill(1,2,1,"abc");
            Assert.IsNull(adhocrefill);
        }

 

    }
}